using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MapElites_Lib;
using Testbed_2__Arm;

namespace Testbed_2__Arm__Exp
{
    public static class ArmRepertoire_Experiment_Utilities
    {

        public static void Run_Experiment__Fine_Grained(
            string experiment_name,
            MEL__Parent_Selection_Method<MEL_ArmRepertoire__Individual> parent_selection_method,
            int number_of_dimensions
            )
        {
            // experiment - specific - settings
            int number_of_iterations = 1_000_000;

            List<int> data_save_points = new List<int>();
            for (int i = 0; i < 100; i += 10) data_save_points.Add(i);
            for (int i = 100; i < 1_000; i += 100) data_save_points.Add(i);
            for (int i = 1_000; i <= 1_000_000; i += 1_000) data_save_points.Add(i);

            List<int> random_seeds = new List<int>() { 1000 };

            List<int> feature_tables_png_save_points = new List<int>() { number_of_iterations };

            // general settings
            double individual_gene_min_value = -Math.PI;
            double individual_gene_max_value = Math.PI;
            double uniform_mutation_rate = 0.05;
            double gene_value_range = individual_gene_max_value - individual_gene_min_value;
            double mutation_step = uniform_mutation_rate * gene_value_range;

            double feature_1_min_value = -1.0;
            double feature_1_max_value = 1.0;
            int feature_1_subdivisions = 100;
            double feature_2_min_value = -1.0;
            double feature_2_max_value = 1.0;
            int feature_2_subdivisions = 100;
            int initial_population = 100;
            

            MEL_Experiment_Runner.Run_Experiment(
                experiment_name,
                random_seeds,
                new MEL__Operator_Settings<MEL_ArmRepertoire__Individual>(
                    new MEL_ArmRepertoire__Individual_Generator(
                        number_of_dimensions,
                        individual_gene_min_value,
                        individual_gene_max_value
                        ),
                    new MEL_ArmRepertoire__Individual_Mutator__Mutation_Step__Uniform_Rate(
                        mutation_step,
                        individual_gene_min_value,
                        individual_gene_max_value
                        )
                    ),
                new MEL__Evaluation_Settings<MEL_ArmRepertoire__Individual>(
                    new MEL_ArmRepertoire__Individual_Evaluator__Fitness(),
                    new MEL_ArmRepertoire__Individual_Evaluator__Feature_1(),
                    new MEL_ArmRepertoire__Individual_Evaluator__Feature_2(),
                    feature_1_min_value,
                    feature_1_max_value,
                    feature_1_subdivisions,
                    feature_2_min_value,
                    feature_2_max_value,
                    feature_2_subdivisions
                    ),
                parent_selection_method,
                initial_population,
                number_of_iterations,
                data_save_points,
                feature_tables_png_save_points,
                data_save_points,
                data_save_points
                );
        }


        public static void Run_Experiment(
            string experiment_name,
            MEL__Parent_Selection_Method<MEL_ArmRepertoire__Individual> parent_selection_method,
            int number_of_dimensions
            )
        {
            // experiment - specific - settings
            int number_of_iterations = 1_000_000;
            List<int> data_save_points = new List<int>();
            for (int i = 0; i < 10_000; i += 1_000) data_save_points.Add(i);
            for (int i = 10_000; i < 100_000; i += 10_000) data_save_points.Add(i);
            for (int i = 100_000; i <= 1_000_000; i += 100_000) data_save_points.Add(i);

            List<int> feature_tables_png_save_points = new List<int>() { number_of_iterations };


            // general settings
            double individual_gene_min_value = -Math.PI;
            double individual_gene_max_value = Math.PI;
            double uniform_mutation_rate = 0.05;
            double gene_value_range = individual_gene_max_value - individual_gene_min_value;
            double mutation_step = uniform_mutation_rate * gene_value_range;

            double feature_1_min_value = -1.0;
            double feature_1_max_value = 1.0;
            int feature_1_subdivisions = 100;
            double feature_2_min_value = -1.0;
            double feature_2_max_value = 1.0;
            int feature_2_subdivisions = 100;
            int initial_population = 100;
            List<int> random_seeds = new List<int>();
            for (int i = 1000; i < 1100; i++) random_seeds.Add(i);

            MEL_Experiment_Runner.Run_Experiment(
                experiment_name,
                random_seeds,
                new MEL__Operator_Settings<MEL_ArmRepertoire__Individual>(
                    new MEL_ArmRepertoire__Individual_Generator(
                        number_of_dimensions,
                        individual_gene_min_value,
                        individual_gene_max_value
                        ),
                    new MEL_ArmRepertoire__Individual_Mutator__Mutation_Step__Uniform_Rate(
                        mutation_step,
                        individual_gene_min_value,
                        individual_gene_max_value
                        )
                    ),
                new MEL__Evaluation_Settings<MEL_ArmRepertoire__Individual>(
                    new MEL_ArmRepertoire__Individual_Evaluator__Fitness(),
                    new MEL_ArmRepertoire__Individual_Evaluator__Feature_1(),
                    new MEL_ArmRepertoire__Individual_Evaluator__Feature_2(),
                    feature_1_min_value,
                    feature_1_max_value,
                    feature_1_subdivisions,
                    feature_2_min_value,
                    feature_2_max_value,
                    feature_2_subdivisions
                    ),
                parent_selection_method,
                initial_population,
                number_of_iterations,
                data_save_points,
                feature_tables_png_save_points,
                data_save_points,
                data_save_points
                );
        }

    }
}
