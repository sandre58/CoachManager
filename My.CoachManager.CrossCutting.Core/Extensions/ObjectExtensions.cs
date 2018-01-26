using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

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

        public static T Clone2<T>(this T source)
        {
            BinaryFormatter bf = new BinaryFormatter();     //helper to serialize
            MemoryStream memStream = new MemoryStream();
            bf.Serialize(memStream, source);
            memStream.Flush();
            memStream.Position = 0;
            var clone = ((T)bf.Deserialize(memStream));

            return clone;
        }

        public static void CopyIn(this object objSource, ref object newInstance)
        {
            //Get the type of source object and create a new instance of that type
            var typeSource = newInstance.GetType();

            //Get all the properties of source object type
            var propertyInfo = typeSource.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            //Assign all source property to taget object 's properties
            foreach (var property in propertyInfo.Where(property => property.CanWrite))
            {
                //check whether property type is value type, enum or string type
                if (property.PropertyType.IsValueType || property.PropertyType.IsEnum || property.PropertyType == typeof(string))
                {
                    property.SetValue(newInstance, property.GetValue(objSource, null), null);
                }
                //else property type is object/complex types, so need to recursively call this method until the end of the tree is reached
                else
                {
                    var objPropertyValue = property.GetValue(objSource, null);
                    property.SetValue(newInstance, objPropertyValue == null ? null : objPropertyValue.Clone(), null);
                }
            }
        }

        public static void CopyIn<T>(this object objSource, ref T newInstance)
        {
            //Get the type of source object and create a new instance of that type
            var typeSource = typeof(T);

            //Get all the properties of source object type
            var propertyInfo = typeSource.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            //Assign all source property to taget object 's properties
            foreach (var property in propertyInfo.Where(property => property.CanWrite))
            {
                //check whether property type is value type, enum or string type
                if (property.PropertyType.IsValueType || property.PropertyType.IsEnum || property.PropertyType == typeof(string))
                {
                    property.SetValue(newInstance, property.GetValue(objSource, null), null);
                }
                //else property type is object/complex types, so need to recursively call this method until the end of the tree is reached
                else
                {
                    var objPropertyValue = property.GetValue(objSource, null);
                    property.SetValue(newInstance, objPropertyValue == null ? null : objPropertyValue.Clone(), null);
                }
            }
        }

        #endregion Public Methods and Operators
    }
}