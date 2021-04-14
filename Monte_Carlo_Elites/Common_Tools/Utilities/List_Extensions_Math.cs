using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Tools.Utilities
{
    public static class List_Extensions_Math
    {
        public static double Mean(this List<double> values)
        {
            double sum = 0;
            foreach (var value in values)
            {
                sum += value;
            }
            return sum / (double)values.Count;
        }

        public static float Mean(this List<float> values)
        {
            float sum = 0;
            foreach (var value in values)
            {
                sum += value;
            }
            return sum / (float)values.Count;
        }

        public static double Variance(this List<double> values)
        {
            double mean = values.Mean();
            double variance_sum = 0;
            foreach (var value in values)
            {
                variance_sum += (mean - value) * (mean - value); // (mean - value) ^ 2
            }
            double variance = variance_sum / (double)values.Count;
            return variance;
        }

        public static float Variance(this List<float> values)
        {
            float mean = values.Mean();
            float variance_sum = 0.0f;
            foreach (var value in values)
            {
                variance_sum += (mean - value) * (mean - value); // (mean - value) ^ 2
            }
            float variance = variance_sum / (float)values.Count;
            return variance;
        }

        public static double StandardDeviation(this List<double> values)
        {
            return Math.Sqrt(values.Variance());
        }

        public static float StandardDeviation(this List<float> values)
        {
            return (float)Math.Sqrt(values.Variance());
        }
    }
}
