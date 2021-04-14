using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

//using PM_Core_Lib;
using Drawing_Utilities;
using Common_Tools.Utilities;

namespace MapElites_Lib
{
    public static class MEL_Experiment_Runner
    {
        public static void Run_Experiment<T>(
            string experiment_name,
            List<int> random_seeds, // implies number of repetitions...
            MEL__Operator_Settings<T> operator_settings,
            MEL__Evaluation_Settings<T> eval_settings,
            MEL__Parent_Selection_Method<T> selection_method,
            int initial_population,
            int total_num_iterations,
            List<int> iterations_for_feature_tables_csv,
            List<int> iterations_for_feature_tables_png,
            List<int> iterations_for_data_logging,
            List<int> iterations_for_console_logging
            )
            where T : MEL__Individual
        {
            // prepare paths etc...
            string general_output_folder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "output"
                );
            if (IO_Utilities.Folder_Exists(general_output_folder) == false)
            {
                // create the general output folder
                IO_Utilities.CreateFolder(general_output_folder);
            }
            string this_experiment__output_folder = Path.Combine(
                    general_output_folder,
                    experiment_name + "___" + DateTime.UtcNow.Ticks.ToString()
                );
            if (IO_Utilities.Folder_Exists(this_experiment__output_folder) == false)
            {
                // create this experiment's folder
                IO_Utilities.CreateFolder(this_experiment__output_folder);
            }

            string data_logging_file_path = Path.Combine(
                this_experiment__output_folder,
                experiment_name + "_data.csv"
                );

            if (IO_Utilities.File_Exists(data_logging_file_path) == false)
                IO_Utilities.Create_File(data_logging_file_path, false, false);

            // create and save the data header...
            string data_header = Map_Elites__Data_Header();
            IO_Utilities.Append_To_File(data_logging_file_path, data_header);

            MEL__Algorithm<T> map_elites = new MEL__Algorithm<T>(
                operator_settings,
                eval_settings,
                selection_method
                );

            Save_Experiment_Settings(
                this_experiment__output_folder,
                experiment_name,
                map_elites
                );

            foreach (var random_seed in random_seeds)
            {
                // initialize the random numbers generator...
                Random randomness_provider = new Random(random_seed);

                map_elites = new MEL__Algorithm<T>(
                    operator_settings,
                    eval_settings,
                    selection_method
                    );

                // generate the initial population
                map_elites.Generate_Initial_Population(randomness_provider, initial_population);

                // save the data at this stage, before any operation...
                Save_Feature_Tables_CSV(
                    this_experiment__output_folder,
                    random_seed,
                    0,
                    map_elites
                    );
                Save_Feature_Tables_PNG(
                    this_experiment__output_folder: this_experiment__output_folder,
                    random_seed: random_seed,
                    current_iteration: 0,
                    map_elites: map_elites,
                    min_fitness: 0.0, 
                    max_fitness: 1.0
                    );
                string data_row = Map_Elites__Data_Row(random_seed, 0, map_elites);
                IO_Utilities.Append_To_File(data_logging_file_path, data_row);

                Console_Log(experiment_name, random_seed, 0);

                for (int current_iteration = 1; current_iteration <= total_num_iterations; current_iteration++)
                {
                    // advance algorithm
                    map_elites.Select_And_Mutate_Individual(randomness_provider);

                    // perhaps, save data
                    if (iterations_for_feature_tables_csv.Contains(current_iteration))
                    {
                        Save_Feature_Tables_CSV(
                            this_experiment__output_folder,
                            random_seed,
                            current_iteration,
                            map_elites
                            );
                    }
                    if (iterations_for_feature_tables_png.Contains(current_iteration))
                    {
                        Save_Feature_Tables_PNG(
                            this_experiment__output_folder:this_experiment__output_folder,
                            random_seed: random_seed,
                            current_iteration: current_iteration,
                            map_elites: map_elites,
                            min_fitness: 0.0,
                            max_fitness: 1.0
                            );
                    }
                    if (iterations_for_data_logging.Contains(current_iteration))
                    {
                        data_row = Map_Elites__Data_Row(random_seed, current_iteration, map_elites);
                        IO_Utilities.Append_To_File(data_logging_file_path, data_row);
                    }
                    if (iterations_for_console_logging.Contains(current_iteration))
                    {
                        Console_Log(experiment_name, random_seed, current_iteration);
                    }

                }
            }
        }

        private static void Console_Log(
            string experiment_name,
            int random_seed,
            int current_iteration
            )
        {
            string description = "";
            description += DateTime.Now.ToString() + " | ";
            description += experiment_name + " | ";
            description += "seed: " + random_seed.ToString() + " | ";
            description += "iter: " + current_iteration.ToString();

            Console.WriteLine(description);
        }

        public static void Save_Experiment_Settings<T>(
            string this_experiment__output_folder,
            string experiment_name,
            MEL__Algorithm<T> map_elites
            )
            where T : MEL__Individual
        {
            string experiment_description_file_path =
                Path.Combine(this_experiment__output_folder,
                "experiment_settings.txt"
                );

            string text_to_write = "experiment name: " + experiment_name + "\n";
            text_to_write += map_elites.Get_Settings_Description();

            IO_Utilities.Append_To_File(
                experiment_description_file_path,
                text_to_write
                );
        }

        public static void Save_Feature_Tables_CSV<T>(
            string this_experiment__output_folder,
            int random_seed,
            int current_iteration,
            MEL__Algorithm<T> map_elites
            )
            where T : MEL__Individual
        {
            // data analysis tables - folder paths
            string individual_exists__csv_folder = Path.Combine(
                this_experiment__output_folder, "CSV_0_A_individual_exists");
            string fitness__csv_folder = Path.Combine(
                this_experiment__output_folder, "CSV_0_B_fitness");
            string selections_per_location__csv_folder = Path.Combine(
                this_experiment__output_folder, "CSV_1_A_selections_per_location");
            string offspring_survivals_per_location__csv_folder = Path.Combine(
                this_experiment__output_folder, "CSV_1_B_offspring_survivals_per_location");

            IO_Utilities.CreateFolder(individual_exists__csv_folder);
            IO_Utilities.CreateFolder(fitness__csv_folder);
            IO_Utilities.CreateFolder(selections_per_location__csv_folder);
            IO_Utilities.CreateFolder(offspring_survivals_per_location__csv_folder);

            /////////////////////////////////////////////////////////////////////////
            // SAVE CSV FILES...
            ////////////////////////////////////////////////////////////////////////

            string individual_exists_table__csv__file_path =
                Path.Combine(individual_exists__csv_folder,
                "individual_exists_table__"
                + "seed_" + random_seed.ToString()
                + "__iter_" + current_iteration.ToString()
                + ".csv"
                );
            IO_Utilities.Append_To_File(
                individual_exists_table__csv__file_path,
                map_elites.state.individual_exists.To_CSV_0_1()
                );

            string fitness_table__csv__file_path =
                Path.Combine(fitness__csv_folder,
                "fitness_table__"
                + "seed_" + random_seed.ToString()
                + "__iter_" + current_iteration.ToString()
                + ".csv"
                );
            IO_Utilities.Append_To_File(
                fitness_table__csv__file_path,
                map_elites.Q_ST_Fitness_Table().To_CSV()
                );


            string selections_per_location__csv__file_path =
                Path.Combine(selections_per_location__csv_folder,
                "selections_per_location__"
                + "seed_" + random_seed.ToString()
                + "__iter_" + current_iteration.ToString()
                + ".csv"
                );
            IO_Utilities.Append_To_File(
                selections_per_location__csv__file_path,
                map_elites.state.selections__per__location.To_CSV()
                );

            string offspring_survivals_per_location__csv__file_path =
                Path.Combine(offspring_survivals_per_location__csv_folder,
                "offspring_survivals_per_location__"
                + "seed_" + random_seed.ToString()
                + "__iter_" + current_iteration.ToString()
                + ".csv"
                );
            IO_Utilities.Append_To_File(
                offspring_survivals_per_location__csv__file_path,
                map_elites.state.offspring_survivals__per__location.To_CSV()
                );
        }

        public static void Save_Feature_Tables_PNG<T>(
            string this_experiment__output_folder,
            int random_seed,
            int current_iteration,
            MEL__Algorithm<T> map_elites,
            double min_fitness,
            double max_fitness
            )
            where T : MEL__Individual
        {
            // data analysis tables - folder paths
            string individual_exists__png_folder = Path.Combine(
                this_experiment__output_folder, "PNG_0_A_individual_exists");
            string fitness__png_folder = Path.Combine(
                this_experiment__output_folder, "PNG_0_B_fitness");
            string selections_per_location__png_folder = Path.Combine(
                this_experiment__output_folder, "PNG_1_A_selections_per_location");
            string offspring_survivals_per_location__png_folder = Path.Combine(
                this_experiment__output_folder, "PNG_1_B_offspring_survivals_per_location");

            IO_Utilities.CreateFolder(individual_exists__png_folder);
            IO_Utilities.CreateFolder(fitness__png_folder);
            IO_Utilities.CreateFolder(selections_per_location__png_folder);
            IO_Utilities.CreateFolder(offspring_survivals_per_location__png_folder);

            /////////////////////////////////////////////////////////////////////////
            // SAVE PNG FILES...
            ////////////////////////////////////////////////////////////////////////

            Bitmap individual_exists__image =
                map_elites.state.individual_exists.To_HeatMap(
                    Color.FromArgb(255, 255, 255),
                    Color.FromArgb(0, 0, 0)
                    );

            string individual_exists__png__file_path =
                Path.Combine(individual_exists__png_folder,
                "individual_exists__"
                + "seed_" + random_seed.ToString()
                + "__iter_" + current_iteration.ToString()
                + ".png"
                );
            individual_exists__image.SaveToDisk(
                individual_exists__png__file_path
                );
            individual_exists__image.Dispose();



            var fitness_table = map_elites.Q_ST_Fitness_Table();

            Bitmap fitness__image =
                fitness_table.To_HeatMap(
                    min_fitness,
                    max_fitness,
                    Color.Red,
                    Color.Green,
                    Color.Magenta
                    );
            string fitness__png__file_path =
                Path.Combine(fitness__png_folder,
                "fitness__"
                + "seed_" + random_seed.ToString()
                + "__iter_" + current_iteration.ToString()
                + ".png"
                );
            fitness__image.SaveToDisk(
                fitness__png__file_path
                );
            fitness__image.Dispose();


            Bitmap selections_per_location__image =
                map_elites.state.selections__per__location.To_HeatMap(
                    0,
                    map_elites.state.selections__per__location.Max(),
                    Color.Red,
                    Color.Magenta
                    );
            string selections_per_location__png__file_path =
                Path.Combine(selections_per_location__png_folder,
                "selections_per_location__"
                + "seed_" + random_seed.ToString()
                + "__iter_" + current_iteration.ToString()
                + ".png"
                );

            selections_per_location__image.SaveToDisk(
                selections_per_location__png__file_path
                );
            selections_per_location__image.Dispose();


            Bitmap offspring_survivals_per_location__image =
                map_elites.state.offspring_survivals__per__location.To_HeatMap(
                    0,
                    map_elites.state.offspring_survivals__per__location.Max(),
                    Color.Red,
                    Color.Magenta
                    );
            string offspring_survivals_per_location__png__file_path =
                Path.Combine(offspring_survivals_per_location__png_folder,
                "offspring_survivals_per_location__"
                + "seed_" + random_seed.ToString()
                + "__iter_" + current_iteration.ToString()
                + ".png"
                );
            offspring_survivals_per_location__image.SaveToDisk(
                offspring_survivals_per_location__png__file_path
                );
            offspring_survivals_per_location__image.Dispose();
        }

        public static string Map_Elites__Data_Row<T>(
            int random_seed,
            int iteration,
            MEL__Algorithm<T> map_elites
            )
            where T : MEL__Individual
        {
            string data_row = "";

            // time - ticks
            data_row += DateTime.UtcNow.Ticks.ToString() + ",";

            // time
            data_row += DateTime.UtcNow.ToString() + ",";

            // RANDOM SEED
            data_row += random_seed.ToString() + ",";

            // ITERATION
            data_row += iteration.ToString() + ",";

            // NUM POSITIONS
            int num_positions = map_elites.Q_Num_Positions();
            data_row += num_positions.ToString() + ",";

            // NUM FILLED POSITIONS
            int num_filled_positions = map_elites.Q_Num_Filled_Positions();
            data_row += num_filled_positions.ToString() + ",";

            // PERCENT FILLED POSITIONS
            double percent_filled_positions = (double)num_filled_positions / (double)num_positions;
            data_row += percent_filled_positions.ToString() + ",";

            // MAXIMUM FITNESS
            double maximum_fitness = map_elites.Q_Maximum_Fitness();
            data_row += maximum_fitness.ToString() + ",";

            // FITNESS SUM
            double fitness_sum = map_elites.Q_Fitness_Sum();
            data_row += fitness_sum.ToString() + ",";

            // FITNESS SUM OVER NUMBER OF POSITIONS
            double fitness_sum_OVER_positions = fitness_sum / (double)num_positions;
            data_row += fitness_sum_OVER_positions.ToString() + ",";

            // FITNESS SUM OVER NUMBER OF FILLED POSITIONS
            double fitness_sum_OVER_filled_positions = fitness_sum / (double)num_filled_positions;
            data_row += fitness_sum_OVER_filled_positions.ToString() + ",";

            data_row += "\n";


            return data_row;
        }

        public static string Map_Elites__Data_Header()
        {
            string data_header = "";

            data_header += "Time_Ticks,";
            data_header += "Time,";
            data_header += "Random_Seed,";
            data_header += "Iteration,";
            data_header += "Num_Positions,";
            data_header += "Num_Filled_Positions,";
            data_header += "Percent_Filled_Positions,";
            data_header += "Maximum_Fitness,";
            data_header += "Fitness_Sum,";
            data_header += "Fitness_Sum__OVER__Positions,";
            data_header += "Fitness_Sum__OVER__Filled_Positions\n";

            return data_header;
        }
    }
}
