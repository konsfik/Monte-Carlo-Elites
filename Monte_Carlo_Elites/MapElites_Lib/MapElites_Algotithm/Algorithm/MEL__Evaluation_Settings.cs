using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Perfect_Mazes_Lib;

namespace MapElites_Lib
{
    public class MEL__Evaluation_Settings<T>
        where T:MEL__Individual
    {
        public MEL__Individual_Evaluation_Method<T> fitness_calculator;
        public MEL__Individual_Evaluation_Method<T> feature_1_calculator;
        public MEL__Individual_Evaluation_Method<T> feature_2_calculator;
        public double feature_1_minimum_value;
        public double feature_1_maximum_value;
        public int feature_1_subdivisions;
        public double[] feature_1_value_ranges;
        public double feature_2_minimum_value;
        public double feature_2_maximum_value;
        public int feature_2_subdivisions;
        public double[] feature_2_value_ranges;

        public MEL__Evaluation_Settings(
            MEL__Individual_Evaluation_Method<T> fitness_calculator,
            MEL__Individual_Evaluation_Method<T> feature_1_calculator,
            MEL__Individual_Evaluation_Method<T> feature_2_calculator,

            double feature_1_minimum_value,
            double feature_1_maximum_value,
            int feature_1_subdivisions,

            double feature_2_minimum_value,
            double feature_2_maximum_value,
            int feature_2_subdivisions
            )
        {
            this.fitness_calculator = fitness_calculator;
            this.feature_1_calculator = feature_1_calculator;
            this.feature_2_calculator = feature_2_calculator;

            this.feature_1_minimum_value = feature_1_minimum_value;
            this.feature_1_maximum_value = feature_1_maximum_value;
            this.feature_1_subdivisions = feature_1_subdivisions;

            // fill the ranges...
            feature_1_value_ranges = new double[feature_1_subdivisions];
            double range_1_total = feature_1_maximum_value - feature_1_minimum_value;
            double range_1_step = range_1_total / (double)feature_1_subdivisions;
            for (int i = 0; i < feature_1_subdivisions; i++)
            {
                feature_1_value_ranges[i] = feature_1_minimum_value + i * range_1_step + range_1_step;
            }

            this.feature_2_minimum_value = feature_2_minimum_value;
            this.feature_2_maximum_value = feature_2_maximum_value;
            this.feature_2_subdivisions = feature_2_subdivisions;

            // fill the ranges...
            feature_2_value_ranges = new double[feature_2_subdivisions];
            double range_2_total = feature_2_maximum_value - feature_2_minimum_value;
            double range_2_step = range_2_total / (double)feature_2_subdivisions;
            for (int i = 0; i < feature_2_subdivisions; i++)
            {
                feature_2_value_ranges[i] = feature_2_minimum_value + i * range_2_step + range_2_step;
            }
        }
    }
}
