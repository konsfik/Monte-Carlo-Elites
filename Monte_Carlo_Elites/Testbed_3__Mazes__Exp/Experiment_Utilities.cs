using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

using Common_Tools.Elements;
using Common_Tools.Utilities;

using Drawing_Utilities;
using Perfect_Mazes_Lib;

using MapElites_Lib;
using Testbed_3__Mazes;

namespace Testbed_3__Mazes__Exp
{
    class Experiment_Utilities
    {
        public static void Run_Multi_Experiment(
            string experiment_name__first_part,
            PM__Maze_Evaluation_Method fitness_calculator,
            PM__Maze_Evaluation_Method feature_1_calculator,
            PM__Maze_Evaluation_Method feature_2_calculator,
            Dictionary<string,MEL__Parent_Selection_Method<MEL__Maze_Individual>> parent_selection_methods,
            List<int> maze_sizes,
            List<int> iterations_for_feature_tables_csv,
            List<int> iterations_for_feature_tables_png,
            List<int> iterations_for_drawing_mazes,
            List<int> iterations_for_data_logging
            )
        {
            

            for (int ms = 0; ms < maze_sizes.Count; ms++)
            {
                int maze_size = maze_sizes[ms];

                foreach (var vp in parent_selection_methods) {
                    MEL__Parent_Selection_Method<MEL__Maze_Individual> selection_method = vp.Value;
                    string experiment_name = experiment_name__first_part;
                    experiment_name += "__M_" + ms.ToString();
                    experiment_name += "__" + vp.Key + "__";

                    Experiment_Utilities.Run_Experiment(
                        experiment_name,
                        selection_method,
                        maze_size,
                        fitness_calculator,
                        feature_1_calculator,
                        feature_2_calculator,
                        iterations_for_feature_tables_csv,
                        iterations_for_feature_tables_png,
                        iterations_for_drawing_mazes,
                        iterations_for_data_logging
                        );
                }
            }
        }

        public static void Run_Experiment__Fine_Grained(
            string experiment_name,
            MEL__Parent_Selection_Method<MEL__Maze_Individual> parent_selection_method,
            List<int> random_seeds,
            int num_iterations,
            int maze_size,
            PM__Maze_Evaluation_Method fitness_calculator,
            PM__Maze_Evaluation_Method feature_1_calculator,
            PM__Maze_Evaluation_Method feature_2_calculator,
            List<int> iterations_for_feature_tables_csv,
            List<int> iterations_for_feature_tables_png,
            List<int> iterations_for_drawing_mazes,
            List<int> iterations_for_data_logging,
            List<int> iterations_for_console_logging
            )
        {

            MEL_Mazes__Individual_Generator individual_generator = new MEL_Mazes__Individual_Generator(
                new MGM__Random_DFS(),
                maze_size,
                maze_size
                );

            MEL_Mazes__Individual_Mutation_Method individual_mutator = new MEL_Mazes__Individual_Mutation_Method(
                    new MDM_RandomPoints_AtLeast_One(0.02),
                    new MRM__Random_DFS()
                );
            MEL__Operator_Settings<MEL__Maze_Individual> maze_generation_settings =
                new MEL__Operator_Settings<MEL__Maze_Individual>(
                    individual_generator,
                    individual_mutator
                    );

            MEL_Mazes__Individual_Evaluation_Method fitness_evaluation_method = new MEL_Mazes__Individual_Evaluation_Method(
                fitness_calculator
                );

            MEL_Mazes__Individual_Evaluation_Method feature_1_evaluation_method = new MEL_Mazes__Individual_Evaluation_Method(
                feature_1_calculator
                );

            MEL_Mazes__Individual_Evaluation_Method feature_2_evaluation_method = new MEL_Mazes__Individual_Evaluation_Method(
                feature_2_calculator
                );

            MEL__Evaluation_Settings<MEL__Maze_Individual> maze_evaluation_settings =
                new MEL__Evaluation_Settings<MEL__Maze_Individual>(
                    fitness_evaluation_method,
                    feature_1_evaluation_method,
                    feature_2_evaluation_method,
                    0.0,
                    1.0,
                    50,
                    0.0,
                    1.0,
                    50
                    );

            int initial_population = 1000;


            Experiment_Utilities.Run_Experiment(
                experiment_name,
                random_seeds,

                maze_generation_settings,
                maze_evaluation_settings,
                parent_selection_method,

                initial_population,
                num_iterations,

                iterations_for_feature_tables_csv,
                iterations_for_feature_tables_png,
                iterations_for_drawing_mazes,
                iterations_for_data_logging,
                iterations_for_console_logging
                );
        }

        public static void Run_Experiment(
            string experiment_name,
            MEL__Parent_Selection_Method<MEL__Maze_Individual> parent_selection_method,
            int maze_size,
            PM__Maze_Evaluation_Method fitness_calculator,
            PM__Maze_Evaluation_Method feature_1_calculator,
            PM__Maze_Evaluation_Method feature_2_calculator,
            List<int> iterations_for_feature_tables_csv,
            List<int> iterations_for_feature_tables_png,
            List<int> iterations_for_drawing_mazes,
            List<int> iterations_for_data_logging
            )
        {
            MEL_Mazes__Individual_Generator individual_generator = new MEL_Mazes__Individual_Generator(
                new MGM__Random_DFS(),
                maze_size,
                maze_size
                );

            MEL_Mazes__Individual_Mutation_Method individual_mutator = new MEL_Mazes__Individual_Mutation_Method(
                    new MDM_RandomPoints_AtLeast_One(0.02),
                    new MRM__Random_DFS()
                    );

            MEL__Operator_Settings<MEL__Maze_Individual> maze_generation_settings =
                new MEL__Operator_Settings<MEL__Maze_Individual>(
                    individual_generator,
                    individual_mutator
                    );

            MEL_Mazes__Individual_Evaluation_Method fitness_evaluation_method = new MEL_Mazes__Individual_Evaluation_Method(
                fitness_calculator
                );

            MEL_Mazes__Individual_Evaluation_Method feature_1_evaluation_method = new MEL_Mazes__Individual_Evaluation_Method(
                feature_1_calculator
                );

            MEL_Mazes__Individual_Evaluation_Method feature_2_evaluation_method = new MEL_Mazes__Individual_Evaluation_Method(
                feature_2_calculator
                );

            MEL__Evaluation_Settings<MEL__Maze_Individual> maze_evaluation_settings =
                new MEL__Evaluation_Settings<MEL__Maze_Individual>(
                    fitness_evaluation_method,
                    feature_1_evaluation_method,
                    feature_2_evaluation_method,
                    0.0,
                    1.0,
                    50,
                    0.0,
                    1.0,
                    50
                    );

            int initial_population = 1000;
            int num_iterations = 100000;

            List<int> iterations_for_console_logging = new List<int>();
            for (int i = 0; i <= num_iterations; i++)
            {
                if (i % 10000 == 0) iterations_for_console_logging.Add(i);
            }

            List<int> random_seeds = new List<int>();
            for (int i = 1000; i < 1100; i++)
            {
                random_seeds.Add(i);
            }

            Experiment_Utilities.Run_Experiment(
                experiment_name,
                random_seeds,

                maze_generation_settings,
                maze_evaluation_settings,
                parent_selection_method,

                initial_population,
                num_iterations,

                iterations_for_feature_tables_csv,
                iterations_for_feature_tables_png,
                iterations_for_drawing_mazes,
                iterations_for_data_logging,
                iterations_for_console_logging
                );
        }


        public static Dictionary<string, MEL__Parent_Selection_Method<MEL__Maze_Individual>> Selection_Methods__Dictionary()
        {
            Dictionary<string, MEL__Parent_Selection_Method<MEL__Maze_Individual>> parent_selection_methods_dict =
                new Dictionary<string, MEL__Parent_Selection_Method<MEL__Maze_Individual>>();
            double c_value = 1.0 / Math.Sqrt(2.0);

            parent_selection_methods_dict.Add(
                "SLM_0",
                new MEL_PSM__Random<MEL__Maze_Individual>()
                );

            parent_selection_methods_dict.Add(
                "SLM_1",
                new MEL_PSM__GREEDY_Parent_Fitness<MEL__Maze_Individual>()
                );

            parent_selection_methods_dict.Add(
                "SLM_2",
                new MEL_PSM__EXPLOIT_Offspring_Survival__Per__Individual<MEL__Maze_Individual>()
                );

            parent_selection_methods_dict.Add(
                "SLM_6",
                new MEL_PSM__EXPLOIT_Offspring_Survival__Per__Location<MEL__Maze_Individual>()
                );


            parent_selection_methods_dict.Add(
                "SLM_10",
                new MEL_PSM__UCB_Offspring_Survival__Per__Individual<MEL__Maze_Individual>(c_value)
                );

            parent_selection_methods_dict.Add(
                "SLM_14",
                new MEL_PSM__UCB_Offspring_Survival__Per__Location<MEL__Maze_Individual>(c_value)
                );


            parent_selection_methods_dict.Add(
                "SLM_22",
                new MEL_PSM__EXPLORE__Per__Individual<MEL__Maze_Individual>()
                );
            parent_selection_methods_dict.Add(
                "SLM_23",
                new MEL_PSM__EXPLORE__Per__Location<MEL__Maze_Individual>()
                );

            parent_selection_methods_dict.Add(
                "SLM_25",
                new MEL_PSM__Curiosity<MEL__Maze_Individual>(1.0, -0.5)
                );

            return parent_selection_methods_dict;
        }


        public static void Run_Experiment(
            string experiment_name,
            List<int> random_seeds, // implies number of repetitions...
            MEL__Operator_Settings<MEL__Maze_Individual> maze_generation_settings,
            MEL__Evaluation_Settings<MEL__Maze_Individual> maze_evaluation_settings,
            MEL__Parent_Selection_Method<MEL__Maze_Individual> selection_method,
            int initial_population,
            int total_num_iterations,
            List<int> iterations_for_feature_tables_csv,
            List<int> iterations_for_feature_tables_png,
            List<int> iterations_for_drawing_mazes,
            List<int> iterations_for_data_logging,
            List<int> iterations_for_console_logging
            )
        {
            // prepare paths etc...
            string general_output_folder = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "output"
                );
            string this_experiment__output_folder = Path.Combine(
                    general_output_folder,
                    experiment_name + "_" + DateTime.UtcNow.Ticks.ToString()
                );

            // create the general output folder
            IO_Utilities.CreateFolder(general_output_folder);

            // create this experiment's folder
            IO_Utilities.CreateFolder(this_experiment__output_folder);

            // create mazes - folder
            string mazes_folder =
                Path.Combine(
                    this_experiment__output_folder,
                    "mazes"
                    );
            if (iterations_for_drawing_mazes.Count > 0)
            {
                IO_Utilities.CreateFolder(mazes_folder);
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

            MEL__Algorithm<MEL__Maze_Individual> map_elites = new MEL__Algorithm<MEL__Maze_Individual>(
                maze_generation_settings,
                maze_evaluation_settings,
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

                map_elites = new MEL__Algorithm<MEL__Maze_Individual>(
                    maze_generation_settings,
                    maze_evaluation_settings,
                    selection_method
                    );

                // generate the initial population
                map_elites.Generate_Initial_Population(randomness_provider, initial_population);

                for (int current_iteration = 0; current_iteration <= total_num_iterations; current_iteration++)
                {
                    // advance algorithm
                    map_elites.Select_And_Mutate_Individual(randomness_provider);

                    // perhaps, save data
                    if (iterations_for_feature_tables_csv.Contains(current_iteration))
                    {
                        Save_Feature_Tables_CSV__Minimal(
                            this_experiment__output_folder,
                            random_seed,
                            current_iteration,
                            map_elites
                            );
                    }
                    if (iterations_for_feature_tables_png.Contains(current_iteration))
                    {
                        Save_Feature_Tables_PNG__Minimal(
                            this_experiment__output_folder,
                            random_seed,
                            current_iteration,
                            map_elites
                            );
                    }
                    if (iterations_for_drawing_mazes.Contains(current_iteration))
                    {
                        Save_Mazes(
                            mazes_folder,
                            random_seed,
                            current_iteration,
                            map_elites
                            );
                    }
                    if (iterations_for_data_logging.Contains(current_iteration))
                    {
                        string data_row = Map_Elites__Data_Row(random_seed, current_iteration, map_elites);
                        IO_Utilities.Append_To_File(data_logging_file_path, data_row);
                    }
                    if (iterations_for_console_logging.Contains(current_iteration))
                    {
                        string description = "";
                        description += experiment_name + " | ";
                        description += "seed: " + random_seed.ToString() + " | ";
                        description += "iter: " + current_iteration.ToString();

                        Console.WriteLine(description);
                    }

                }
            }
        }

        public static void Save_Experiment_Settings(
            string this_experiment__output_folder,
            string experiment_name,
            MEL__Algorithm<MEL__Maze_Individual> map_elites
            )
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

        public static void Save_Feature_Tables_CSV__Minimal(
            string this_experiment__output_folder,
            int random_seed,
            int current_iteration,
            MEL__Algorithm<MEL__Maze_Individual> map_elites
            )
        {
            // data analysis tables - folder paths
            string individual_exists__csv_folder = Path.Combine(
                this_experiment__output_folder, "CSV_0_A_individual_exists");
            string fitness__csv_folder = Path.Combine(
                this_experiment__output_folder, "CSV_0_B_fitness");

            string selections_per_location__csv_folder = Path.Combine(
                this_experiment__output_folder, "CSV_1_A_selections_per_location");

            IO_Utilities.CreateFolder(individual_exists__csv_folder);
            IO_Utilities.CreateFolder(fitness__csv_folder);
            IO_Utilities.CreateFolder(selections_per_location__csv_folder);

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
        }

        public static void Save_Feature_Tables_PNG__Minimal(
            string this_experiment__output_folder,
            int random_seed,
            int current_iteration,
            MEL__Algorithm<MEL__Maze_Individual> map_elites
            )
        {
            // data analysis tables - folder paths
            string individual_exists__png_folder = Path.Combine(
                this_experiment__output_folder, "PNG_0_A_individual_exists");
            string fitness__png_folder = Path.Combine(
                this_experiment__output_folder, "PNG_0_B_fitness");
            string selections_per_location__png_folder = Path.Combine(
                this_experiment__output_folder, "PNG_1_A_selections_per_location");

            IO_Utilities.CreateFolder(individual_exists__png_folder);
            IO_Utilities.CreateFolder(fitness__png_folder);
            IO_Utilities.CreateFolder(selections_per_location__png_folder);

            /////////////////////////////////////////////////////////////////////////
            // SAVE PNG FILES...
            ////////////////////////////////////////////////////////////////////////

            bool[,] individual_exists_table = map_elites.state.individual_exists.DeepCopy();
            Bitmap individual_exists__image =
                individual_exists_table.To_HeatMap(
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
                    0.0,
                    fitness_table.Max(),
                    Color.DarkRed,
                    Color.Pink,
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
        }

        public static string Map_Elites__Data_Header()
        {
            string data_header = "";

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

        public static string Map_Elites__Data_Row(
            int random_seed,
            int iteration,
            MEL__Algorithm<MEL__Maze_Individual> map_elites
            )
        {
            string data_row = "";

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

        public static void Save_Mazes(
            string folder_path,
            int random_seed,
            int iteration,
            MEL__Algorithm<MEL__Maze_Individual> map_elites
            )
        {
            Color maze_walls_color = Color.FromArgb(32, 32, 32);
            int maze_cell_drawing_size = 4;

            int table_width = map_elites.state.individuals.GetLength(0);
            int table_height = map_elites.state.individuals.GetLength(1);

            Bitmap[,] maze_images = new Bitmap[table_width, table_height];

            for (int x = 0; x < table_width; x++)
            {
                for (int y = 0; y < table_height; y++)
                {
                    if (map_elites.state.individuals[x, y] != null)
                    {
                        double fitness = map_elites.state.individuals[x, y].fitness;

                        double c_f = Math_Utilities.Map_Value(fitness, 0.0, 1.0, 0.0, 255.0);
                        int c = (int)c_f;

                        Color cc = Color.FromArgb(255, c, c);
                        MEL__Maze_Individual mi = (MEL__Maze_Individual)(map_elites.state.individuals[x, y]);
                        PM_Maze maze = mi.maze;
                        maze_images[x, y] = Maze_Drawing_Utilities.Draw(
                                maze,
                                cc,
                                maze_walls_color,
                                maze_cell_drawing_size
                            );
                    }
                }
            }
            int w = ((MEL_Mazes__Individual_Generator)map_elites.operator_settings.individual_generator).width;
            int h = ((MEL_Mazes__Individual_Generator)map_elites.operator_settings.individual_generator).height;
            int maze_drawing_width = w * maze_cell_drawing_size + 2;
            int maze_drawing_height = h * maze_cell_drawing_size + 2;

            int total_width = maze_drawing_width * map_elites.state.individuals.GetLength(0);
            int total_height = maze_drawing_height * map_elites.state.individuals.GetLength(1);



            Bitmap img = new Bitmap(total_width, total_height);
            using (Graphics bufferGraphics = Graphics.FromImage(img))
            {
                bufferGraphics.Clear(Color.White);

                for (int x = 0; x < table_width; x++)
                {
                    for (int y = 0; y < table_height; y++)
                    {
                        if (maze_images[x, y] != null)
                        {
                            bufferGraphics.DrawImage(
                                maze_images[x, y],
                                new Point(maze_drawing_width * x, maze_drawing_height * y)
                                );
                        }
                    }
                }
            }
            string fp = Path.Combine(
                    folder_path,
                    "random_seed_" + random_seed.ToString()
                    + "iter_" + iteration.ToString()
                    + ".png"
                );
            img.SaveToDisk(fp);
            img.Dispose();
        }

    }
}
