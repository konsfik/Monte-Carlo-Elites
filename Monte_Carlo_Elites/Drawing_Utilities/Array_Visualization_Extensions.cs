using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools.Elements;
using Common_Tools.Utilities;

namespace Drawing_Utilities
{
    public static class Array_Visualization_Extensions
    {
        public static Bitmap To_HeatMap(
            this bool[,] values_table,
            Color true_color,
            Color false_color
            )
        {
            int w = values_table.GetLength(0);
            int h = values_table.GetLength(1);

            Bitmap image = new Bitmap(w, h);
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    bool value = values_table[x, y];
                    if (value == true)
                    {
                        image.SetPixel(x, y, true_color);
                    }
                    else
                    {
                        image.SetPixel(x, y, false_color);
                    }
                }
            }
            return image;
        }

        public static Bitmap To_HeatMap(
            this int[,] values_table,
            int min_value,
            int max_value,
            Color out_of_range__low__color,
            Color out_of_range__high__color
            )
        {
            Bitmap image = new Bitmap(values_table.GetLength(0), values_table.GetLength(1));
            for (int x = 0; x < values_table.GetLength(0); x++)
            {
                for (int y = 0; y < values_table.GetLength(1); y++)
                {
                    double value = values_table[x, y];
                    if (value < min_value)
                    {
                        image.SetPixel(x, y, out_of_range__low__color);
                    }
                    else if (value > max_value)
                    {
                        image.SetPixel(x, y, out_of_range__high__color);
                    }
                    else
                    {
                        if (min_value == max_value)
                        {
                            Color c = Color.FromArgb(0, 0, 0);
                            image.SetPixel(x, y, c);
                        }
                        else
                        {
                            double cv = Math_Utilities.Map_Value(value, min_value, max_value, 0.0, 255.0);
                            int ci = (int)cv;
                            Color c = Color.FromArgb(ci, ci, ci);
                            image.SetPixel(x, y, c);
                        }


                    }
                }
            }
            return image;
        }

        public static Bitmap To_HeatMap(
            this double[,] values_table,
            double min_value,
            double max_value,
            Color out_of_range__low__color,
            Color out_of_range__high__color,
            Color not_a_number__color
            )
        {
            int w = values_table.GetLength(0);
            int h = values_table.GetLength(1);

            Bitmap image = new Bitmap(w, h);
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    if (min_value == max_value)
                    {
                        double value = values_table[x, y];
                        if (Double.IsNaN(value))
                        {
                            image.SetPixel(x, y, not_a_number__color);
                        }
                        else
                        {
                            Color c = Color.FromArgb(0, 0, 0);
                            image.SetPixel(x, y, c);
                        }
                    }
                    else
                    {
                        double value = values_table[x, y];
                        if (Double.IsNaN(value))
                        {
                            image.SetPixel(x, y, not_a_number__color);
                        }
                        else if (value < min_value)
                        {
                            image.SetPixel(x, y, out_of_range__low__color);
                        }
                        else if (value > max_value)
                        {
                            image.SetPixel(x, y, out_of_range__high__color);
                        }
                        else
                        {
                            double cv = Math_Utilities.Map_Value(value, min_value, max_value, 0.0, 255.0);
                            int ci = (int)cv;
                            Color c = Color.FromArgb(ci, ci, ci);
                            image.SetPixel(x, y, c);
                        }
                    }
                }
            }
            return image;
        }
    }
}
