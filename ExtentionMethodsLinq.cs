using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassTest
{
    static internal class ExtentionMethodsLinq
    {
        internal static IEnumerable<Tresult> MySelect<Tsource, Tresult>(this IEnumerable<Tsource> source, Func<Tsource, Tresult> projection)
        {
            foreach (var item in source)
            {

                yield return projection(item);
            }
        }        
        internal static IEnumerable<T> MyWhere<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (var item in source)
                if (predicate(item))
                    yield return item;
        }
        internal static T MyFirst<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {

            foreach (T item in source)
            {
                if (predicate(item))
                    return item;
            }
            throw new InvalidOperationException();
        }
        internal static T MyFirst<T>(this IEnumerable<T> source)
        {

            foreach (T item in source)
            {
                return item;
            }
            throw new InvalidOperationException();
        }
        internal static int MyCount<T>(this IEnumerable<T> source)
        {
            var collection = source as ICollection<T>;
            if (collection != null)
            {
                return collection.Count;
            }
            int count = 0;
            foreach (T item in source) count++;
            return count;
        }
        internal static int MyCount<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            int count = 0;
            var collection = source as ICollection<T>;
            if (collection != null)
            {
                foreach (T item in collection)
                    if (predicate(item)) count++;
            }
            else
            {
                foreach (T item in source)
                    if (predicate(item)) count++;
            }


            return count;
        }
        internal static T MyAggregate<T, TSource>(this IEnumerable<TSource> source, T seed, Func<T, TSource, T> fold)
        {
            var value = seed;
            foreach (var item in source)
            {
                value = fold(value, item);
            }
            return value;
        }
        internal static IEnumerable<T> MyConcat<T>(this IEnumerable<T> source, IEnumerable<T> other)
        {

            foreach (var item2 in source)
            {
                yield return item2;
            }
            foreach (var item in other)
            {
                yield return item;
            }
        }
        internal static IEnumerable<T> MyUnion<T>(this IEnumerable<T> source, IEnumerable<T> other)
        {
            var hash = new HashSet<T>();
            foreach (var item in source)
            {
                if (hash.Add(item))
                    yield return item;
            }
            foreach (var item in other)
            {
                if (hash.Add(item))
                    yield return item;
            }

        }
        internal static IEnumerable<T> MyIntercect<T>(this IEnumerable<T> source, IEnumerable<T> other)
        {
            var hash = new HashSet<T>(other);
            foreach (var item in source)
            {
                if (hash.Remove(item))
                    yield return item;
            }

        }
        internal static IEnumerable<T> MyExept<T>(this IEnumerable<T> source, IEnumerable<T> other)
        {
            var hash = new HashSet<T>(other);
            foreach (var item in source)
            {
                if (hash.Add(item))
                    yield return item;
            }

        }
        internal static Dictionary<Tkey, Tvalue> MyToDictionary<TSource, Tkey, Tvalue>(
            this IEnumerable<TSource> source,
            Func<TSource, Tkey> keySelector,
            Func<TSource, Tvalue> valueSelector)
        {
            var dictionary = new Dictionary<Tkey, Tvalue>();
            foreach (var item in source)
            {
                dictionary.Add(keySelector(item), valueSelector(item));
            }
            return dictionary;
        }    

        //usando System.Collection; versione non generic di IEnumerable (per una signature piu pulita)
        internal static IEnumerable<TResult> MyCast<TResult>(this IEnumerable souce)
        {
            foreach (var item in souce)
            {
                yield return (TResult)item;
            }
        }
        internal static IEnumerable<TResult> MyOfType<TResult>(this IEnumerable souce)
        {
           foreach (var item in souce)
            {

                if (item is TResult)
                    yield return (TResult)item;
            }
        }
       
    }
}
