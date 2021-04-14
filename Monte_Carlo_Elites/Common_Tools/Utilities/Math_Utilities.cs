using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Tools.Utilities
{
    public static class Math_Utilities
    {
        public static bool Is_In_Range(
            this in double value,
            in double range_min,
            in double range_max
            )
        {
            if (range_min < range_max)
            {
                return value >= range_min && value <= range_max;
            }
            else if (range_min > range_max)
            {
                return value <= range_min && value >= range_max;
            }
            else if (range_min == range_max)
            {
                return value == range_min;
            }
            else
            {
                return false;
            }
        }

        public static bool Is_In_Range(
            this in float value,
            in float range_min,
            in float range_max
            )
        {
            if (range_min < range_max)
            {
                return value >= range_min && value <= range_max;
            }
            else if (range_min > range_max)
            {
                return value <= range_min && value >= range_max;
            }
            else if (range_min == range_max)
            {
                return value == range_min;
            }
            else
            {
                return false;
            }
        }

        public static bool Is_In_Range(
            this in int value,
            in int range_min,
            in int range_max
            )
        {
            if (range_min < range_max)
            {
                return value >= range_min && value <= range_max;
            }
            else if (range_min > range_max)
            {
                return value <= range_min && value >= range_max;
            }
            else if (range_min == range_max)
            {
                return value == range_min;
            }
            else
            {
                return false;
            }
        }

        public static double Constrain(
            this in double value,
            in double range_min,
            in double range_max
            )
        {
            if (range_min < range_max)
            {
                if (value > range_max) return range_max;
                else if (value < range_min) return range_min;
                else return value;
            }
            else if (range_min > range_max)
            {
                if (value < range_max) return range_max;
                else if (value > range_min) return range_min;
                else return value;
            }
            else if (range_min == range_max)
            {
                return range_min;
            }
            else return value;
        }

        public static float Constrain(
            this in float value,
            in float range_min,
            in float range_max
            )
        {
            if (range_min < range_max)
            {
                if (value > range_max) return range_max;
                else if (value < range_min) return range_min;
                else return value;
            }
            else if (range_min > range_max)
            {
                if (value < range_max) return range_max;
                else if (value > range_min) return range_min;
                else return value;
            }
            else if (range_min == range_max)
            {
                return range_min;
            }
            else return value;
        }

        public static double Map_Value(
            this in double value,
            in double from_min,
            in double from_max,
            in double to_min,
            in double to_max
            )
        {
            double value_dif = value - from_min;
            double from_range = from_max - from_min;
            double to_range = to_max - to_min;

            double mapped_value = value_dif * to_range / from_range + to_min;

            return mapped_value;
        }

        public static float Map_Value(
            this in float value,
            in float from_min,
            in float from_max,
            in float to_min,
            in float to_max
            )
        {
            float value_dif = value - from_min;
            float from_range = from_max - from_min;
            float to_range = to_max - to_min;

            float mapped_value = value_dif * to_range / from_range + to_min;

            return mapped_value;
        }

        public static double Map_Value_Constrained(
            this in double value,
            in double from_min,
            in double from_max,
            in double to_min,
            in double to_max
            )
        {
            double mapped_value = Map_Value(
                value,
                from_min,
                from_max,
                to_min,
                to_max
                );

            double constrained_value = mapped_value.Constrain(to_min, to_max);

            return constrained_value;
        }


        public static float Map_Value_Constrained(
            this in float value,
            in float from_min,
            in float from_max,
            in float to_min,
            in float to_max
            )
        {
            float mapped_value = Map_Value(
                value,
                from_min,
                from_max,
                to_min,
                to_max
                );

            float constrained_value = mapped_value.Constrain(to_min, to_max);

            return constrained_value;
        }


    }
}
