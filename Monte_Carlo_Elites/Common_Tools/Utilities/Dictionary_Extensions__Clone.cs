using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Tools.Utilities
{
    public static class Dictionary_Extensions__Clone
    {
        public static Dictionary<T, R> Clone<T, R>(this Dictionary<T, R> originalDictionary)
            where T : struct
            where R : struct
        {
            Dictionary<T, R> dictionaryCopy = new Dictionary<T, R>();

            foreach (var kvp in originalDictionary)
            {
                var key = kvp.Key;
                var value = kvp.Value;
                dictionaryCopy.Add(key, value);
            }

            return dictionaryCopy;
        }
    }
}
