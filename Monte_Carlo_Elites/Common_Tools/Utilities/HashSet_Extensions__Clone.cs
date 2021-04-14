using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Tools.Utilities
{
    public static class HashSet_Extensions__Clone
    {
        public static HashSet<T> Clone<T>(this HashSet<T> originalHashSet)
            where T : struct
        {
            HashSet<T> hashSetCopy = new HashSet<T>();

            foreach (var element in originalHashSet)
            {
                hashSetCopy.Add(element);
            }

            return hashSetCopy;
        }
    }
}
