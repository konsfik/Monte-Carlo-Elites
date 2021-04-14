using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MapElites_Lib;
using Testbed_1__Rastrigin;

namespace Testbed_1__Rastrigin__Exp
{
    public static class Experiment_Utilities
    {
        public static void Run_Experiment__Fine_Grained(
            string experiment_name,
            MEL__Parent_Selection_Method<MEL_Rastrigin__Individual> parent_selection_method,
            int number_of_dimensions
            )
        {
            int initial_population_size = 100;
            int number_of_iterations = 1_000_000;
            double minimum_gene_value = -5.12;
            double maximum_gene_value = 5.12;
            double gene_value_range = maximum_gene_value - minimum_gene_value;
            double mutation_rate = 0.05;
            double mutation_step = mutation_rate * gene_value_range;
            int num_subdivisions = 100;

            List<int> data_save_points = new List<int>();
            for (int i = 0; i < 100; i += 10) data_save_points.Add(i);
            for (int i = 100; i < 1_000; i += 100) data_save_points.Add(i);
            for (int i = 1_000; i <= 1_000_000; i += 1_000) data_save_points.Add(i);

            List<int> random_seeds = new List<int>() { 1000 };

            List<int> feature_tables_png_save_points = new List<int>() { number_of_iterations };

            MEL_Experiment_Runner.Run_Experiment(
                experiment_name,
                random_seeds,
                new MEL__Operator_Settings<MEL_Rastrigin__Individual>(
                    new MEL_Rastrigin__Individual_Generator(
                        number_of_dimensions,
                        minimum_gene_value,
                        maximum_gene_value
                        ),
                    new MEL_Rastrigin__Individual_Mutator(
                        mutation_step,
                        minimum_gene_value,
                        maximum_gene_value
                        )
                    ),
                new MEL__Evaluation_Settings<MEL_Rastrigin__Individual>(
                    new MEL_Rastrigin__Fitness__Evaluation_Method(),
                    new MEL_Rastrigin__Feature_1__Evaluation_Method(),
                    new MEL_Rastrigin__Feature_2__Evaluation_Method(),
                    minimum_gene_value,
                    maximum_gene_value,
                    num_subdivisions,
                    minimum_gene_value,
                    maximum_gene_value,
                    num_subdivisions
                    ),
                parent_selection_method,
                initial_population_size,
                number_of_iterations,
                data_save_points,
                feature_tables_png_save_points,
                data_save_points,
                data_save_points
                );
        }

        public static void Run_Experiment__Normal(
            string experiment_name,
            MEL__Parent_Selection_Method<MEL_Rastrigin__Individual> parent_selection_method,
            int number_of_dimensions
            )
        {
            

            int initial_population_size = 100;
            int number_of_iterations = 1_000_000;
            double minimum_gene_value = -5.12;
            double maximum_gene_value = 5.12;
            double gene_value_range = maximum_gene_value - minimum_gene_value;
            double mutation_rate = 0.05;
            double mutation_step = mutation_rate * gene_value_range;
            int num_subdivisions = 100;


            List<int> data_save_points = new List<int>();
            for (int i = 0; i < 10_000; i += 1_000) data_save_points.Add(i);
            for (int i = 10_000; i < 100_000; i += 10_000) data_save_points.Add(i);
            for (int i = 100_000; i <= 1_000_000; i += 100_000) data_save_points.Add(i);

            List<int> random_seeds = new List<int>();
            for (int i = 1000; i < 1100; i++)
            {
                random_seeds.Add(i);
            }

            List<int> feature_tables_png_save_points = new List<int>() { number_of_iterations };

            MEL_Experiment_Runner.Run_Experiment(
                experiment_name,
                random_seeds,
                new MEL__Operator_Settings<MEL_Rastrigin__Individual>(
                    new MEL_Rastrigin__Individual_Generator(
                        number_of_dimensions,
                        minimum_gene_value,
                        maximum_gene_value
                        ),
                    new MEL_Rastrigin__Individual_Mutator(
                        mutation_step,
                        minimum_gene_value,
                        maximum_gene_value
                        )
                    ),
                new MEL__Evaluation_Settings<MEL_Rastrigin__Individual>(
                    new MEL_Rastrigin__Fitness__Evaluation_Method(),
                    new MEL_Rastrigin__Feature_1__Evaluation_Method(),
                    new MEL_Rastrigin__Feature_2__Evaluation_Method(),
                    minimum_gene_value,
                    maximum_gene_value,
                    num_subdivisions,
                    minimum_gene_value,
                    maximum_gene_value,
                    num_subdivisions
                    ),
                parent_selection_method,
                initial_population_size,
                number_of_iterations,
                data_save_points,
                feature_tables_png_save_points,
                data_save_points,
                data_save_points
                );
        }
    }
}
