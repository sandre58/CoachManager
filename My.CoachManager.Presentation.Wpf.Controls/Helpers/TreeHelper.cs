﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace My.CoachManager.Presentation.Wpf.Controls.Helpers
{
    /// <summary>
    /// Helper methods for UI-related tasks.
    /// This class was obtained from Philip Sumi (a fellow WPF Disciples blog)
    /// http://www.hardcodet.net/uploads/2009/06/UIHelper.cs
    /// </summary>
    public static class TreeHelper
    {
        /// <summary>
        /// Finds the ancestor or self.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="obj">The object.</param>
        /// <returns>
        /// The ancestor or self.
        /// </returns>
        public static UIElement FindAncestorOrSelf(Type type, DependencyObject obj)
        {
            while (obj != null)
            {
                if (obj.GetType() == type)
                {
                    return obj as UIElement;
                }

                obj = GetParentObject(obj);
            }

            return null;
        }


        /// <summary>
        /// Finds a parent of a given item on the visual tree.
        /// </summary>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="child">A direct or indirect child of the
        /// queried item.</param>
        /// <returns>The first parent item that matches the submitted
        /// type parameter. If not matching item can be found, a null
        /// reference is being returned.</returns>
        public static T TryFindParent<T>(this DependencyObject child)
            where T : DependencyObject
        {
            //get parent item
            DependencyObject parentObject = GetParentObject(child);

            //we've reached the end of the tree
            if (parentObject == null) return null;

            //check if the parent matches the type we're looking for
            var parent = parentObject as T;
            return parent ?? TryFindParent<T>(parentObject);
        }

        public static DependencyObject FindTopLevelParent(DependencyObject reference)
        {
            var parent = VisualTreeHelper.GetParent(reference);
            if (parent != null)
            {
                var nextParent = VisualTreeHelper.GetParent(parent);
                return nextParent != null ? FindTopLevelParent(parent) : parent;
            }
            return null;
        }

        /// <summary>
        /// Finds a Child of a given item in the visual tree.
        /// </summary>
        /// <param name="parent">A direct parent of the queried item.</param>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="childName">x:Name or Name of child. </param>
        /// <returns>The first parent item that matches the submitted type parameter.
        /// If not matching item can be found,
        /// a null parent is being returned.</returns>
        public static T FindChild<T>(this DependencyObject parent, string childName)
           where T : DependencyObject
        {
            // Confirm parent and childName are valid.
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                if (!(child is T))
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);
                    // If the child is found, break so we do not overwrite the found child.
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    // If the child's name is set for search
                    if (child is IFrameworkInputElement frameworkInputElement && frameworkInputElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                    else
                    {
                        // recursively drill down the tree
                        foundChild = FindChild<T>(child, childName);
                        // If the child is found, break so we do not overwrite the found child.
                        if (foundChild != null) break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }

        public static DependencyObject FindPartByName(this DependencyObject ele, string name)
        {
            if (ele == null)
            {
                return null;
            }
            if (name.Equals(ele.GetValue(FrameworkElement.NameProperty)))
            {
                return ele;
            }

            var numVisuals = VisualTreeHelper.GetChildrenCount(ele);
            for (var i = 0; i < numVisuals; i++)
            {
                DependencyObject vis = VisualTreeHelper.GetChild(ele, i);
                DependencyObject result;
                if ((result = FindPartByName(vis, name)) != null)
                {
                    return result;
                }
            }
            return null;
        }

        public static T GetVisualChild<T>(this Visual parent) where T : Visual
        {
            T child = default(T);

            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                var v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T ?? GetVisualChild<T>(v);
                if (child != null)
                {
                    break;
                }
            }

            return child;
        }

        /// <summary>
        /// Looks for a child control within a parent by type
        /// </summary>
        /// <typeparam name="T">
        /// The type of the object to find.
        /// </typeparam>
        /// <param name="parent">
        /// The parent.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T FindChild<T>(DependencyObject parent)
            where T : DependencyObject
        {
            // Confirm parent is valid.
            if (parent == null)
            {
                return null;
            }

            T foundChild = null;

            var childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (var i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                // If the child is not of the request child type child
                if (!(child is T))
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child);

                    // If the child is found, break so we do not overwrite the found child.
                    if (foundChild != null)
                    {
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }

        /// <summary>
        /// Finds the visual parent of the specified dependency object.
        /// </summary>
        /// <typeparam name="T">The type of the parent to find.</typeparam>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <returns>
        /// The visual parent of the specified dependency object.
        /// </returns>
        public static T FindVisualParent<T>(this DependencyObject dependencyObject)
            where T : DependencyObject
        {
            return FindVisualParent<T>(dependencyObject, null);
        }

        /// <summary>
        /// Finds the visual parent of the specified dependency object.
        /// </summary>
        /// <typeparam name="T">The type of the parent to find.</typeparam>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="name">The name of the parent. <c>null</c> if none.</param>
        /// <returns>
        /// The visual parent of the specified dependency object.
        /// </returns>
        public static T FindVisualParent<T>(this DependencyObject dependencyObject, string name)
            where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dependencyObject);

            if ((parent == null) && (dependencyObject is FrameworkElement))
            {
                parent = ((FrameworkElement)dependencyObject).Parent;
            }

            if (parent != null)
            {
                if ((parent is T variable) &&
                    (string.IsNullOrEmpty(name) || ((variable is FrameworkElement) && ((FrameworkElement)parent).Name == name)))
                {
                    return variable;
                }

                T grandParent = parent.FindVisualParent<T>(name);
                if (grandParent != null)
                {
                    return grandParent;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the logical parent of the specified dependency object.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <returns>The logical parent of the specified dependency object.</returns>
        public static DependencyObject GetLogicalParent(this DependencyObject dependencyObject)
        {
            return LogicalTreeHelper.GetParent(dependencyObject);
        }

        /// <summary>
        /// This method is an alternative to WPF's
        /// <see cref="VisualTreeHelper.GetParent"/> method, which also
        /// supports content elements. Keep in mind that for content element,
        /// this method falls back to the logical tree of the element!
        /// </summary>
        /// <param name="child">The item to be processed.</param>
        /// <returns>The submitted item's parent, if available. Otherwise
        /// null.</returns>
        public static DependencyObject GetParentObject(this DependencyObject child)
        {
            if (child == null) return null;

            // handle content elements separately
            if (child is ContentElement contentElement)
            {
                DependencyObject parent = ContentOperations.GetParent(contentElement);
                if (parent != null) return parent;

                return contentElement is FrameworkContentElement fce ? fce.Parent : null;
            }

            var childParent = VisualTreeHelper.GetParent(child);
            if (childParent != null)
            {
                return childParent;
            }

            // also try searching for parent in framework elements (such as DockPanel, etc)
            if (!(child is FrameworkElement frameworkElement)) return null;
            {
                DependencyObject parent = frameworkElement.Parent;
                if (parent != null) return parent;
            }

            return null;
        }

        /// <summary>
        /// Analyzes both visual and logical tree in order to find all elements of a given
        /// type that are descendants of the <paramref name="source"/> item.
        /// </summary>
        /// <typeparam name="T">The type of the queried items.</typeparam>
        /// <param name="source">The root element that marks the source of the search. If the
        /// source is already of the requested type, it will not be included in the result.</param>
        /// <param name="forceUsingTheVisualTreeHelper">Sometimes it's better to search in the VisualTree (e.g. in tests)</param>
        /// <returns>All descendants of <paramref name="source"/> that match the requested type.</returns>
        public static IEnumerable<T> FindChildren<T>(this DependencyObject source, bool forceUsingTheVisualTreeHelper = false) where T : DependencyObject
        {
            if (source == null) yield break;
            var childs = GetChildObjects(source, forceUsingTheVisualTreeHelper);
            foreach (DependencyObject child in childs)
            {
                //analyze if children match the requested type
                if (child is T variable)
                {
                    yield return variable;
                }

                //recurse tree
                foreach (var descendant in FindChildren<T>(child, forceUsingTheVisualTreeHelper))
                {
                    yield return descendant;
                }
            }
        }

        /// <summary>
        /// This method is an alternative to WPF's
        /// <see cref="VisualTreeHelper.GetChild"/> method, which also
        /// supports content elements. Keep in mind that for content elements,
        /// this method falls back to the logical tree of the element.
        /// </summary>
        /// <param name="parent">The item to be processed.</param>
        /// <param name="forceUsingTheVisualTreeHelper">Sometimes it's better to search in the VisualTree (e.g. in tests)</param>
        /// <returns>The submitted item's child elements, if available.</returns>
        public static IEnumerable<DependencyObject> GetChildObjects(this DependencyObject parent, bool forceUsingTheVisualTreeHelper = false)
        {
            if (parent == null) yield break;

            if (!forceUsingTheVisualTreeHelper && (parent is ContentElement || parent is FrameworkElement))
            {
                //use the logical tree for content / framework elements
                foreach (var obj in LogicalTreeHelper.GetChildren(parent))
                {
                    if (obj is DependencyObject o) yield return o;
                }
            }
            else if (parent is Visual || parent is Visual3D)
            {
                //use the visual tree per default
                var count = VisualTreeHelper.GetChildrenCount(parent);
                for (var i = 0; i < count; i++)
                {
                    yield return VisualTreeHelper.GetChild(parent, i);
                }
            }
        }

        /// <summary>
        /// Tries to locate a given item within the visual tree,
        /// starting with the dependency object at a given position.
        /// </summary>
        /// <typeparam name="T">The type of the element to be found
        /// on the visual tree of the element at the given location.</typeparam>
        /// <param name="reference">The main element which is used to perform
        /// hit testing.</param>
        /// <param name="point">The position to be evaluated on the origin.</param>
        public static T TryFindFromPoint<T>(UIElement reference, Point point)
            where T : DependencyObject
        {
            if (!(reference.InputHitTest(point) is DependencyObject element))
                return null;
            if (element is T variable)
                return variable;
            return TryFindParent<T>(element);
        }
    }
}