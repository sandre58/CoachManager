using System;
using System.Linq;
using System.Reflection;

namespace My.CoachManager.CrossCutting.Core.Extensions
{
    public static class ObjectExtensions
    {
        #region Public Methods and Operators

        public static object Clone(this object objSource)
        {
            //Get the type of source object and create a new instance of that type
            var typeSource = objSource.GetType();
            var objTarget = Activator.CreateInstance(typeSource);

            //Get all the properties of source object type
            var propertyInfo = typeSource.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            //Assign all source property to taget object 's properties
            foreach (var property in propertyInfo.Where(property => property.CanWrite))
            {
                //check whether property type is value type, enum or string type
                if (property.PropertyType.IsValueType || property.PropertyType.IsEnum || property.PropertyType == typeof(string))
                {
                    property.SetValue(objTarget, property.GetValue(objSource, null), null);
                }
                //else property type is object/complex types, so need to recursively call this method until the end of the tree is reached
                else
                {
                    var objPropertyValue = property.GetValue(objSource, null);
                    property.SetValue(objTarget, objPropertyValue == null ? null : objPropertyValue.Clone(), null);
                }
            }
            return objTarget;
        }

        #endregion Public Methods and Operators
    }
}