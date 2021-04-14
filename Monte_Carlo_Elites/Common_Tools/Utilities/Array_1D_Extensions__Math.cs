using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common_Tools.Utilities
{
    public static class Array_1D_Extensions__Math
    {
        public static double Sum(this double[] array)
        {
            int len = array.Length;

            double sum = 0.0d;
            for (int i = 0; i < len; i++)
            {
                sum += array[i];
            }

            return sum;
        }

        public static float Sum(this float[] array)
        {
            int len = array.Length;

            float sum = 0.0f;
            for (int i = 0; i < len; i++)
            {
                sum += array[i];
            }

            return sum;
        }

        public static int Sum(this int[] array)
        {
            int len = array.Length;

            int sum = 0;
            for (int i = 0; i < len; i++)
            {
                sum += array[i];
            }

            return sum;
        }

        public static int Sum(this bool[] array)
        {
            int len = array.Length;

            int sum = 0;

            for (int i = 0; i < len; i++)
            {
                if (array[i])
                {
                    sum++;
                }
            }

            return sum;
        }

    }
}
