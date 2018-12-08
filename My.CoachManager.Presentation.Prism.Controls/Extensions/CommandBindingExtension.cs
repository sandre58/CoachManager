using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace My.CoachManager.Presentation.Prism.Controls.Extensions
{
    [MarkupExtensionReturnType(typeof(ICommand))]
    public class CommandBindingExtension : MarkupExtension
    {
        public CommandBindingExtension()
        {
        }

        public CommandBindingExtension(string commandName)
        {
            CommandName = commandName;
        }

        [ConstructorArgument("commandName")]
        public string CommandName { get; set; }

        private object _targetObject;
        private object _targetProperty;

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (serviceProvider.GetService(typeof(IProvideValueTarget)) is IProvideValueTarget provideValueTarget)
            {
                _targetObject = provideValueTarget.TargetObject;
                _targetProperty = provideValueTarget.TargetProperty;
            }

            if (!string.IsNullOrEmpty(CommandName))
            {
                // The serviceProvider is actually a ProvideValueServiceProvider, which has a private field "_context" of type ParserContext
                ParserContext parserContext = GetPrivateFieldValue<ParserContext>(serviceProvider, "_context");
                if (parserContext != null)
                {
                    // A ParserContext has a private field "_rootElement", which returns the root element of the XAML file
                    FrameworkElement rootElement =
                        GetPrivateFieldValue<FrameworkElement>(parserContext, "_rootElement");
                    if (rootElement != null)
                    {
                        // Now we can retrieve the DataContext
                        object dataContext = rootElement.DataContext;

                        // The DataContext may not be set yet when the FrameworkElement is first created, and it may change afterwards,
                        // so we handle the DataContextChanged event to update the Command when needed
                        if (!_dataContextChangeHandlerSet)
                        {
                            rootElement.DataContextChanged +=
                                rootElement_DataContextChanged;
                            _dataContextChangeHandlerSet = true;
                        }

                        if (dataContext != null)
                        {
                            ICommand command = GetCommand(dataContext, CommandName);
                            if (command != null)
                                return command;
                        }
                    }
                }
            }

            // The Command property of an InputBinding cannot be null, so we return a dummy extension instead
            return DummyCommand.Instance;
        }

        private ICommand GetCommand(object dataContext, string commandName)
        {
            PropertyInfo prop = dataContext.GetType().GetProperty(commandName);
            if (prop != null)
            {
                if (prop.GetValue(dataContext, null) is ICommand command)
                    return command;
            }
            return null;
        }

        private void AssignCommand(ICommand command)
        {
            if (_targetObject != null && _targetProperty != null)
            {
                if (_targetProperty is DependencyProperty depProp)
                {
                    DependencyObject depObj = _targetObject as DependencyObject;
                    depObj?.SetValue(depProp, command);
                }
                else
                {
                    PropertyInfo prop = _targetProperty as PropertyInfo;
                    if (prop != null) prop.SetValue(_targetObject, command, null);
                }
            }
        }

        private bool _dataContextChangeHandlerSet;

        private void rootElement_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is FrameworkElement rootElement)
            {
                object dataContext = rootElement.DataContext;
                if (dataContext != null)
                {
                    ICommand command = GetCommand(dataContext, CommandName);
                    if (command != null)
                    {
                        AssignCommand(command);
                    }
                }
            }
        }

        private T GetPrivateFieldValue<T>(object target, string fieldName)
        {
            FieldInfo field = target.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
            if (field != null)
            {
                return (T) field.GetValue(target);
            }
            return default(T);
        }

        // A dummy command that does nothing...
        private class DummyCommand : ICommand
        {

            #region Singleton pattern

            private DummyCommand()
            {
            }

            private static DummyCommand _instance;

            public static DummyCommand Instance
            {
                get
                {
                    if (_instance == null)
                    {
                        _instance = new DummyCommand();
                    }
                    return _instance;
                }
            }

            #endregion

            #region ICommand Members

            public bool CanExecute(object parameter)
            {
                return false;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
            }

            #endregion
        }
    }
}