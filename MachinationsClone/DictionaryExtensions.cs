using System.Collections.Generic;

namespace MachinationsClone
{
    public static class DictionaryExtensions
    {
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default(TValue))
        {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : default(TValue);
        }
        
        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
        {
            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = value;
            }
            else
            {
                dictionary.Add(key, value);
            }
        }
        
        public static Dictionary<TKey, TValue> Merge<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IDictionary<TKey, TValue> other)
        {
            var merged = new Dictionary<TKey, TValue>(dictionary);
            foreach (var kvp in other)
            {
                merged.AddOrUpdate(kvp.Key, kvp.Value);
            }

            return merged;
        }
    }
}