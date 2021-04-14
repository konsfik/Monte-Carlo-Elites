using System;
using System.Collections.Generic;
using System.Text;

using Common_Tools.Elements;

namespace Common_Tools.Utilities
{
    public static class Array_2D_Extensions__Deep_Copy
    {

        public static T[,] DeepCopy<T>(this T[,] originalArray)
            where T : IDeepCopyable
        {
            int width = originalArray.GetLength(0);
            int height = originalArray.GetLength(1);

            T[,] arrayCopy = new T[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    arrayCopy[x, y] = (T)originalArray[x, y].DeepCopy();
                }
            }

            return arrayCopy;
        }

        public static double[,] DeepCopy(this double[,] originalArray)
        {
            int width = originalArray.GetLength(0);
            int height = originalArray.GetLength(1);

            double[,] arrayCopy = new double[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    arrayCopy[x, y] = originalArray[x, y];
                }
            }

            return arrayCopy;
        }

        public static float[,] DeepCopy(this float[,] originalArray)
        {
            int width = originalArray.GetLength(0);
            int height = originalArray.GetLength(1);

            float[,] arrayCopy = new float[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    arrayCopy[x, y] = originalArray[x, y];
                }
            }

            return arrayCopy;
        }

        public static int[,] DeepCopy(this int[,] originalArray)
        {
            int width = originalArray.GetLength(0);
            int height = originalArray.GetLength(1);

            int[,] arrayCopy = new int[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    arrayCopy[x, y] = originalArray[x, y];
                }
            }

            return arrayCopy;
        }

        public static bool[,] DeepCopy(this bool[,] originalArray)
        {
            int width = originalArray.GetLength(0);
            int height = originalArray.GetLength(1);

            bool[,] arrayCopy = new bool[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    arrayCopy[x, y] = originalArray[x, y];
                }
            }

            return arrayCopy;
        }

        public static string[,] DeepCopy(this string[,] originalArray)
        {
            int width = originalArray.GetLength(0);
            int height = originalArray.GetLength(1);

            string[,] arrayCopy = new string[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    arrayCopy[x, y] = originalArray[x, y];
                }
            }

            return arrayCopy;
        }

        public static Directions_Ortho_2D[,] DeepCopy(this Directions_Ortho_2D[,] originalArray)
        {
            int width = originalArray.GetLength(0);
            int height = originalArray.GetLength(1);

            Directions_Ortho_2D[,] arrayCopy = new Directions_Ortho_2D[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    arrayCopy[x, y] = originalArray[x, y];
                }
            }

            return arrayCopy;
        }

        public static Vec2i[,] DeepCopy(this Vec2i[,] originalArray)
        {
            int width = originalArray.GetLength(0);
            int height = originalArray.GetLength(1);

            Vec2i[,] arrayCopy = new Vec2i[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    arrayCopy[x, y] = originalArray[x, y];
                }
            }

            return arrayCopy;
        }

        public static Edge2i[,] DeepCopy(this Edge2i[,] originalArray)
        {
            int width = originalArray.GetLength(0);
            int height = originalArray.GetLength(1);

            Edge2i[,] arrayCopy = new Edge2i[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    arrayCopy[x, y] = originalArray[x, y];
                }
            }

            return arrayCopy;
        }

        public static UEdge2i[,] DeepCopy(this UEdge2i[,] originalArray)
        {
            int width = originalArray.GetLength(0);
            int height = originalArray.GetLength(1);

            UEdge2i[,] arrayCopy = new UEdge2i[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    arrayCopy[x, y] = originalArray[x, y];
                }
            }

            return arrayCopy;
        }

    }
}
