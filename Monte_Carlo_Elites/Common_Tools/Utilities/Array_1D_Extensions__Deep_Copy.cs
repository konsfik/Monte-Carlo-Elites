using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Common_Tools.Elements;

namespace Common_Tools.Utilities
{
    public static class Array_1D_Extensions__Deep_Copy
    {

        public static T[] DeepCopy<T>(this T[] original_array)
            where T : IDeepCopyable
        {
            int length = original_array.Length;

            T[] copy_array = new T[length];

            for (int i = 0; i < length; i++)
            {
                copy_array[i] = (T)original_array[i].DeepCopy();
            }

            return copy_array;
        }

        public static double[] DeepCopy(this double[] original_array)
        {
            int length = original_array.Length;

            double[] copy_array = new double[length];

            for (int i = 0; i < length; i++)
            {
                copy_array[i] = original_array[i];
            }

            return copy_array;
        }

        public static float[] DeepCopy(this float[] original_array)
        {
            int length = original_array.Length;

            float[] copy_array = new float[length];

            for (int i = 0; i < length; i++)
            {
                copy_array[i] = original_array[i];
            }

            return copy_array;
        }

        public static int[] DeepCopy(this int[] original_array)
        {
            int length = original_array.Length;

            int[] copy_array = new int[length];

            for (int i = 0; i < length; i++)
            {
                copy_array[i] = original_array[i];
            }

            return copy_array;
        }

        public static bool[] DeepCopy(this bool[] original_array)
        {
            int length = original_array.Length;

            bool[] copy_array = new bool[length];

            for (int i = 0; i < length; i++)
            {
                copy_array[i] = original_array[i];
            }

            return copy_array;
        }

        public static string[] DeepCopy(this string[] original_array)
        {
            int length = original_array.Length;

            string[] copy_array = new string[length];

            for (int i = 0; i < length; i++)
            {
                copy_array[i] = original_array[i];
            }

            return copy_array;
        }

        public static Vec2i[] DeepCopy(this Vec2i[] original_array)
        {
            int length = original_array.Length;

            Vec2i[] copy_array = new Vec2i[length];

            for (int i = 0; i < length; i++)
            {
                copy_array[i] = original_array[i];
            }

            return copy_array;
        }

        public static Directions_Ortho_2D[] DeepCopy(this Directions_Ortho_2D[] original_array)
        {
            int length = original_array.Length;

            Directions_Ortho_2D[] copy_array = new Directions_Ortho_2D[length];

            for (int i = 0; i < length; i++)
            {
                copy_array[i] = original_array[i];
            }

            return copy_array;
        }


        

        

        

    }
}
