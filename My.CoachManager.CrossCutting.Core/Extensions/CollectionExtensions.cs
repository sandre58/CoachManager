using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using My.CoachManager.CrossCutting.Core.Collections;

namespace My.CoachManager.CrossCutting.Core.Extensions
{
    public static class CollectionExtensions
    {
        #region Public Methods and Operators

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            if (collection is List<T> list)
            {
                list.AddRange(items);
            }
            else
            {
                foreach (var item in items)
                {
                    collection.Add(item);
                }
            }
        }

        public static void ReplaceAll<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            collection.Clear();
            collection.AddRange(items);
        }

        public static void Sort<T, TKey>(this ICollection<T> collection, Func<T, TKey> keySelector)
        {
            var items = collection.OrderBy(keySelector).ToArray();
            collection.ReplaceAll(items);
        }

        public static void SortDescending<T, TKey>(this ICollection<T> collection, Func<T, TKey> keySelector)
        {
            var items = collection.OrderByDescending(keySelector).ToArray();
            collection.ReplaceAll(items);
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)

        {
            return new ObservableCollection<T>(source);
        }

        public static ItemsObservableCollection<T> ToItemsObservableCollection<T>(this IEnumerable<T> source) where T : INotifyPropertyChanged

        {
            var collection = new ItemsObservableCollection<T>();
            collection.AddRange(source);
            return collection;
        }

        #endregion Public Methods and Operators
    }
}