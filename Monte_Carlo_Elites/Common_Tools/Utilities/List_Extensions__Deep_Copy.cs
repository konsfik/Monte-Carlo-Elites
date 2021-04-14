using System;
using System.Collections.Generic;
using System.Text;

using Common_Tools.Elements;

namespace Common_Tools.Utilities
{
    public static class List_Extensions__Deep_Copy
    {
        public static List<T> DeepCopy<T>(this List<T> original_list)
            where T : IDeepCopyable
        {
            List<T> copy_list = new List<T>();

            foreach (var item in original_list)
            {
                copy_list.Add((T)item.DeepCopy());
            }

            return copy_list;
        }

        public static List<double> DeepCopy(this List<double> original_list)
        {
            return new List<double>(original_list);
        }

        public static List<float> DeepCopy(this List<float> original_list)
        {
            return new List<float>(original_list);
        }

        public static List<int> DeepCopy(this List<int> original_list)
        {
            return new List<int>(original_list);
        }

        public static List<string> DeepCopy(this List<string> original_list)
        {
            return new List<string>(original_list);
        }

        public static List<bool> DeepCopy(this List<bool> original_list)
        {
            return new List<bool>(original_list);
        }

        public static List<Vec2i> DeepCopy(this List<Vec2i> original_list)
        {
            return new List<Vec2i>(original_list);
        }

        public static List<Directions_Ortho_2D> DeepCopy(this List<Directions_Ortho_2D> original_list)
        {
            return new List<Directions_Ortho_2D>(original_list);
        }

    }
}
