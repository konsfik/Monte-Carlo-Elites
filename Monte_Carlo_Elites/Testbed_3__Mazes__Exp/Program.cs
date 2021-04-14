using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;

//using Drawing_Utilities;
using Perfect_Mazes_Lib;

using MapElites_Lib;
using Testbed_3__Mazes;

namespace Testbed_3__Mazes__Exp
{
    class Program
    {
        public delegate void Del();

        static void Main(string[] args)
        {
            var methods = typeof(Program).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .ToList()
                .FindAll(x => x.Name.Contains("Experiment_"));

            Dictionary<int, Del> menuMethods = new Dictionary<int, Del>();
            Dictionary<int, string> menuMethodNames = new Dictionary<int, string>();
            for (int i = 0; i < methods.Count; i++)
            {
                var method = methods[i];
                menuMethods.Add(i, (Del)Del.CreateDelegate(typeof(Del), methods[i]));
                menuMethodNames.Add(i, methods[i].Name);
            }

            while (true)
            {
                int selection = -1;
                bool parseSuccessful = false;
                while (parseSuccessful == false || menuMethods.ContainsKey(selection) == false)
                {
                    Console.Clear();

                    Console.WriteLine("Select:");

                    foreach (var key in menuMethodNames.Keys)
                    {
                        Console.WriteLine(key.ToString() + ": " + menuMethodNames[key] + "\n");
                    }

                    string userInput = Console.ReadLine();
                    parseSuccessful = int.TryParse(userInput, out selection);
                }

                menuMethods[selection].Invoke();

                Console.ReadKey();
            }
        }

        ////////////////////////////////////////////////////////////////////////
        /// ACTUAL EXPERIMENTS
        ////////////////////////////////////////////////////////////////////////

        public static void Experiment_26__SLM_0__Fine_Grained()
        {

            List<int> iterations_for_feature_tables__csv = new List<int>();
            for (int i = 0; i < 100; i += 10) iterations_for_feature_tables__csv.Add(i);
            for (int i = 100; i < 1_000; i += 100) iterations_for_feature_tables__csv.Add(i);
            for (int i = 1_000; i <= 1_000_000; i += 1_000) iterations_for_feature_tables__csv.Add(i);

            Experiment_Utilities.Run_Experiment__Fine_Grained(
                "Experiment_26__SLM_0",
                new MEL_PSM__Random<MEL__Maze_Individual>(),
                new List<int>() { 1000 },
                1_000_000,
                8,
                new MEM__Medium_Solution_Length(),
                new MEM__Percent_Corners(),
                new MEM__Symmetry_Over_Y_Axis(),
                iterations_for_feature_tables__csv,
                new List<int>() { 0, 1_000, 10_000, 100_000, 1_000_000 },
                new List<int>() { 0, 1_000, 10_000, 100_000, 1_000_000 },
                new List<int>() { 0, 1_000, 10_000, 100_000, 1_000_000 },
                iterations_for_feature_tables__csv
                );
        }

        public static void Experiment_26__SLM_10__Fine_Grained()
        {

            List<int> iterations_for_feature_tables__csv = new List<int>();
            for (int i = 0; i < 100; i += 10) iterations_for_feature_tables__csv.Add(i);
            for (int i = 100; i < 1_000; i += 100) iterations_for_feature_tables__csv.Add(i);
            for (int i = 1_000; i <= 1_000_000; i += 1_000) iterations_for_feature_tables__csv.Add(i);

            Experiment_Utilities.Run_Experiment__Fine_Grained(
                "Experiment_26__SLM_10",
                new MEL_PSM__UCB_Offspring_Survival__Per__Individual<MEL__Maze_Individual>(1.0 / Math.Sqrt(2.0)),
                new List<int>() { 1000 },
                1_000_000,
                8,
                new MEM__Medium_Solution_Length(),
                new MEM__Percent_Corners(),
                new MEM__Symmetry_Over_Y_Axis(),
                iterations_for_feature_tables__csv,
                new List<int>() { 0, 1_000, 10_000, 100_000, 1_000_000 },
                new List<int>() { 0, 1_000, 10_000, 100_000, 1_000_000 },
                new List<int>() { 0, 1_000, 10_000, 100_000, 1_000_000 },
                iterations_for_feature_tables__csv
                );
        }

        public static void Experiment_26__SLM_14__Fine_Grained()
        {

            List<int> iterations_for_feature_tables__csv = new List<int>();
            for (int i = 0; i < 100; i += 10) iterations_for_feature_tables__csv.Add(i);
            for (int i = 100; i < 1_000; i += 100) iterations_for_feature_tables__csv.Add(i);
            for (int i = 1_000; i <= 1_000_000; i += 1_000) iterations_for_feature_tables__csv.Add(i);

            Experiment_Utilities.Run_Experiment__Fine_Grained(
                experiment_name: "Experiment_26__SLM_14",
                parent_selection_method: new MEL_PSM__UCB_Offspring_Survival__Per__Location<MEL__Maze_Individual>(1.0 / Math.Sqrt(2.0)),
                random_seeds: new List<int>() { 1000 },
                1_000_000,
                8,
                new MEM__Medium_Solution_Length(),
                new MEM__Percent_Corners(),
                new MEM__Symmetry_Over_Y_Axis(),
                iterations_for_feature_tables_csv: iterations_for_feature_tables__csv,
                iterations_for_feature_tables_png: new List<int>() { 0, 1_000, 10_000, 100_000, 1_000_000 },
                new List<int>() { 0, 1_000, 10_000, 100_000, 1_000_000 },
                new List<int>() { 0, 1_000, 10_000, 100_000, 1_000_000 },
                iterations_for_feature_tables__csv
                );
        }


        public static void Experiment_1()
        {
            Experiment_Utilities.Run_Multi_Experiment(
                experiment_name__first_part: "Experiment_1",
                fitness_calculator: new MEM__Percent_Corners(),
                feature_1_calculator: new MEM__Percent_Corridors(),
                feature_2_calculator: new MEM__Symmetry_Over_Y_Axis(),
                parent_selection_methods: Experiment_Utilities.Selection_Methods__Dictionary(),
                maze_sizes: new List<int>() { 8, 16 },
                iterations_for_feature_tables_csv: new List<int>() { 0, 1000, 10000, 100000 },
                iterations_for_feature_tables_png: new List<int>() { 100000 },
                iterations_for_drawing_mazes: new List<int>() { 100000 },
                iterations_for_data_logging: new List<int>() { 0, 1000, 10000, 100000 }
                );
        }

        public static void Experiment_2()
        {
            Experiment_Utilities.Run_Multi_Experiment(
                experiment_name__first_part: "Experiment_2",
                fitness_calculator: new MEM__Percent_Corners(),
                feature_1_calculator: new MEM__Percent_Corridors(),
                feature_2_calculator: new MEM__Symmetry_Over_X_And_Y_Axes(),
                parent_selection_methods: Experiment_Utilities.Selection_Methods__Dictionary(),
                maze_sizes: new List<int>() { 8, 16 },
                iterations_for_feature_tables_csv: new List<int>() { 0, 1000, 10000, 100000 },
                iterations_for_feature_tables_png: new List<int>() { 100000 },
                iterations_for_drawing_mazes: new List<int>() { 100000 },
                iterations_for_data_logging: new List<int>() { 0, 1000, 10000, 100000 }
                );
        }

        public static void Experiment_3()
        {
            Experiment_Utilities.Run_Multi_Experiment(
                experiment_name__first_part: "Experiment_3",
                fitness_calculator: new MEM__Percent_Corners(),
                feature_1_calculator: new MEM__Percent_Corridors(),
                feature_2_calculator: new MEM__Medium_Solution_Length(),
                parent_selection_methods: Experiment_Utilities.Selection_Methods__Dictionary(),
                maze_sizes: new List<int>() { 8, 16 },
                iterations_for_feature_tables_csv: new List<int>() { 0, 1000, 10000, 100000 },
                iterations_for_feature_tables_png: new List<int>() { 100000 },
                iterations_for_drawing_mazes: new List<int>() { 100000 },
                iterations_for_data_logging: new List<int>() { 0, 1000, 10000, 100000 }
                );
        }

        public static void Experiment_4()
        {
            Experiment_Utilities.Run_Multi_Experiment(
                experiment_name__first_part: "Experiment_4",
                fitness_calculator: new MEM__Percent_Corners(),
                feature_1_calculator: new MEM__Symmetry_Over_Y_Axis(),
                feature_2_calculator: new MEM__Symmetry_Over_X_And_Y_Axes(),
                parent_selection_methods: Experiment_Utilities.Selection_Methods__Dictionary(),
                maze_sizes: new List<int>() { 8, 16 },
                iterations_for_feature_tables_csv: new List<int>() { 0, 1000, 10000, 100000 },
                iterations_for_feature_tables_png: new List<int>() { 100000 },
                iterations_for_drawing_mazes: new List<int>() { 100000 },
                iterations_for_data_logging: new List<int>() { 0, 1000, 10000, 100000 }
                );
        }

        public static void Experiment_5()
        {
            Experiment_Utilities.Run_Multi_Experiment(
                experiment_name__first_part: "Experiment_5",
                fitness_calculator: new MEM__Percent_Corners(),
                feature_1_calculator: new MEM__Symmetry_Over_Y_Axis(),
                feature_2_calculator: new MEM__Medium_Solution_Length(),
                parent_selection_methods: Experiment_Utilities.Selection_Methods__Dictionary(),
                maze_sizes: new List<int>() { 8, 16 },
                iterations_for_feature_tables_csv: new List<int>() { 0, 1000, 10000, 100000 },
                iterations_for_feature_tables_png: new List<int>() { 100000 },
                iterations_for_drawing_mazes: new List<int>() { 100000 },
                iterations_for_data_logging: new List<int>() { 0, 1000, 10000, 100000 }
                );
        }

        public static void Experiment_6()
        {
            Experiment_Utilities.Run_Multi_Experiment(
                experiment_name__first_part: "Experiment_6",
                fitness_calculator: new MEM__Percent_Corners(),
                feature_1_calculator: new MEM__Symmetry_Over_X_And_Y_Axes(),
                feature_2_calculator: new MEM__Medium_Solution_Length(),
                parent_selection_methods: Experiment_Utilities.Selection_Methods__Dictionary(),
                maze_sizes: new List<int>() { 8, 16 },
                iterations_for_feature_tables_csv: new List<int>() { 0, 1000, 10000, 100000 },
                iterations_for_feature_tables_png: new List<int>() { 100000 },
                iterations_for_drawing_mazes: new List<int>() { 100000 },
                iterations_for_data_logging: new List<int>() { 0, 1000, 10000, 100000 }
                );
        }
        
        public static void Experiment_7()
        {
            Experiment_Utilities.Run_Multi_Experiment(
                experiment_name__first_part: "Experiment_7",
                fitness_calculator: new MEM__Percent_Corridors(),
                feature_1_calculator: new MEM__Percent_Corners(),
                feature_2_calculator: new MEM__Symmetry_Over_Y_Axis(),
                parent_selection_methods: Experiment_Utilities.Selection_Methods__Dictionary(),
                maze_sizes: new List<int>() { 8, 16 },
                iterations_for_feature_tables_csv: new List<int>() { 0, 1000, 10000, 100000 },
                iterations_for_feature_tables_png: new List<int>() { 100000 },
                new List<int>() { 100000 },
                new List<int>() { 0, 1000, 10000, 100000 }
                );
        }

        public static void Experiment_8()
        {
            Experiment_Utilities.Run_Multi_Experiment(
                experiment_name__first_part: "Experiment_8",
                fitness_calculator: new MEM__Percent_Corridors(),
                feature_1_calculator: new MEM__Percent_Corners(),
                feature_2_calculator: new MEM__Symmetry_Over_X_And_Y_Axes(),
                parent_selection_methods: Experiment_Utilities.Selection_Methods__Dictionary(),
                maze_sizes: new List<int>() { 8, 16 },
                iterations_for_feature_tables_csv: new List<int>() { 0, 1000, 10000, 100000 },
                iterations_for_feature_tables_png: new List<int>() { 100000 },
                iterations_for_drawing_mazes: new List<int>() { 100000 },
                iterations_for_data_logging: new List<int>() { 0, 1000, 10000, 100000 }
                );
        }

        public static void Experiment_9()
        {
            Experiment_Utilities.Run_Multi_Experiment(
                experiment_name__first_part: "Experiment_9",
                fitness_calculator: new MEM__Percent_Corridors(),
                feature_1_calculator: new MEM__Percent_Corners(),
                feature_2_calculator: new MEM__Medium_Solution_Length(),
                parent_selection_methods: Experiment_Utilities.Selection_Methods__Dictionary(),
                maze_sizes: new List<int>() { 8, 16 },
                iterations_for_feature_tables_csv: new List<int>() { 0, 1000, 10000, 100000 },
                iterations_for_feature_tables_png: new List<int>() { 100000 },
                iterations_for_drawing_mazes: new List<int>() { 100000 },
                iterations_for_data_logging: new List<int>() { 0, 1000, 10000, 100000 }
                );
        }

        public static void Experiment_10()
        {
            Experiment_Utilities.Run_Multi_Experiment(
                experiment_name__first_part: "Experiment_10",
                fitness_calculator: new MEM__Percent_Corridors(),
                feature_1_calculator: new MEM__Symmetry_Over_Y_Axis(),
                feature_2_calculator: new MEM__Symmetry_Over_X_And_Y_Axes(),
                parent_selection_methods: Experiment_Utilities.Selection_Methods__Dictionary(),
                maze_sizes: new List<int>() { 8, 16 },
                iterations_for_feature_tables_csv: new List<int>() { 0, 1000, 10000, 100000 },
                iterations_for_feature_tables_png: new List<int>() { 100000 },
                iterations_for_drawing_mazes: new List<int>() { 100000 },
                iterations_for_data_logging: new List<int>() { 0, 1000, 10000, 100000 }
                );
        }

        public static void Experiment_11()
        {
            Experiment_Utilities.Run_Multi_Experiment(
                experiment_name__first_part: "Experiment_11",
                fitness_calculator: new MEM__Percent_Corridors(),
                feature_1_calculator: new MEM__Symmetry_Over_Y_Axis(),
                feature_2_calculator: new MEM__Medium_Solution_Length(),
                parent_selection_methods: Experiment_Utilities.Selection_Methods__Dictionary(),
                maze_sizes: new List<int>() { 8, 16 },
                iterations_for_feature_tables_csv: new List<int>() { 0, 1000, 10000, 100000 },
                iterations_for_feature_tables_png: new List<int>() { 100000 },
                iterations_for_drawing_mazes: new List<int>() { 100000 },
                iterations_for_data_logging: new List<int>() { 0, 1000, 10000, 100000 }
                );
        }

        public static void Experiment_12()
        {
            Experiment_Utilities.Run_Multi_Experiment(
                experiment_name__first_part: "Experiment_12",
                fitness_calculator: new MEM__Percent_Corridors(),
                feature_1_calculator: new MEM__Symmetry_Over_X_And_Y_Axes(),
                feature_2_calculator: new MEM__Medium_Solution_Length(),
                parent_selection_methods: Experiment_Utilities.Selection_Methods__Dictionary(),
                maze_sizes: new List<int>() { 8, 16 },
                iterations_for_feature_tables_csv: new List<int>() { 0, 1000, 10000, 100000 },
                iterations_for_feature_tables_png: new List<int>() { 100000 },
                iterations_for_drawing_mazes: new List<int>() { 100000 },
                iterations_for_data_logging: new List<int>() { 0, 1000, 10000, 100000 }
                );
        }

        public static void Experiment_13()
        {
            Experiment_Utilities.Run_Multi_Experiment(
                experiment_name__first_part: "Experiment_13",
                fitness_calculator: new MEM__Symmetry_Over_Y_Axis(),
                feature_1_calculator: new MEM__Percent_Corners(),
                feature_2_calculator: new MEM__Percent_Corridors(),
                parent_selection_methods: Experiment_Utilities.Selection_Methods__Dictionary(),
                maze_sizes: new List<int>() { 8, 16 },
                iterations_for_feature_tables_csv: new List<int>() { 0, 1000, 10000, 100000 },
                iterations_for_feature_tables_png: new List<int>() { 100000 },
                iterations_for_drawing_mazes: new List<int>() { 100000 },
                iterations_for_data_logging: new List<int>() { 0, 1000, 10000, 100000 }
                );
        }

        public static void Experiment_14()
        {
            Experiment_Utilities.Run_Multi_Experiment(
                experiment_name__first_part: "Experiment_14",
                fitness_calculator: new MEM__Symmetry_Over_Y_Axis(),
                feature_1_calculator: new MEM__Percent_Corners(),
                feature_2_calculator: new MEM__Symmetry_Over_X_And_Y_Axes(),
                parent_selection_methods: Experiment_Utilities.Selection_Methods__Dictionary(),
                maze_sizes: new List<int>() { 8, 16 },
                iterations_for_feature_tables_csv: new List<int>() { 0, 1000, 10000, 100000 },
                iterations_for_feature_tables_png: new List<int>() { 100000 },
                iterations_for_drawing_mazes: new List<int>() { 100000 },
                iterations_for_data_logging: new List<int>() { 0, 1000, 10000, 100000 }
                );
        }

        public static void Experiment_15()
        {
            Experiment_Utilities.Run_Multi_Experiment(
                experiment_name__first_part: "Experiment_15",
                fitness_calculator: new MEM__Symmetry_Over_Y_Axis(),
                feature_1_calculator: new MEM__Percent_Corners(),
                feature_2_calculator: new MEM__Medium_Solution_Length(),
                parent_selection_methods: Experiment_Utilities.Selection_Methods__Dictionary(),
                maze_sizes: new List<int>() { 8, 16 },
                iterations_for_feature_tables_csv: new List<int>() { 0, 1000, 10000, 100000 },
                iterations_for_feature_tables_png: new List<int>() { 100000 },
                iterations_for_drawing_mazes: new List<int>() { 100000 },
                iterations_for_data_logging: new List<int>() { 0, 1000, 10000, 100000 }
                );
        }

        public static void Experiment_16()
        {
            Experiment_Utilities.Run_Multi_Experiment(
                experiment_name__first_part: "Experiment_16",
                fitness_calculator: new MEM__Symmetry_Over_Y_Axis(),
                feature_1_calculator: new MEM__Percent_Corridors(),
                feature_2_calculator: new MEM__Symmetry_Over_X_And_Y_Axes(),
                parent_selection_methods: Experiment_Utilities.Selection_Methods__Dictionary(),
                maze_sizes: new List<int>() { 8, 16 },
                iterations_for_feature_tables_csv: new List<int>() { 0, 1000, 10000, 100000 },
                iterations_for_feature_tables_png: new List<int>() { 100000 },
                iterations_for_drawing_mazes: new List<int>() { 100000 },
                iterations_for_data_logging: new List<int>() { 0, 1000, 10000, 100000 }
                );
        }

        public static void Experiment_17()
        {
            Experiment_Utilities.Run_Multi_Experiment(
                experiment_name__first_part: "Experiment_17",
                fitness_calculator: new MEM__Symmetry_Over_Y_Axis(),
                feature_1_calculator: new MEM__Percent_Corridors(),
                feature_2_calculator: new MEM__Medium_Solution_Length(),
                parent_selection_methods: Experiment_Utilities.Selection_Methods__Dictionary(),
                maze_sizes: new List<int>() { 8, 16 },
                iterations_for_feature_tables_csv: new List<int>() { 0, 1000, 10000, 100000 },
                iterations_for_feature_tables_png: new List<int>() { 100000 },
                iterations_for_drawing_mazes: new List<int>() { 100000 },
                iterations_for_data_logging: new List<int>() { 0, 1000, 10000, 100000 }
                );
        }

        public static void Experiment_18()
        {
            Experiment_Utilities.Run_Multi_Experiment(
                experiment_name__first_part: "Experiment_18",
                fitness_calculator: new MEM__Symmetry_Over_Y_Axis(),
                feature_1_calculator: new MEM__Symmetry_Over_X_And_Y_Axes(),
                feature_2_calculator: new MEM__Medium_Solution_Length(),
                parent_selection_methods: Experiment_Utilities.Selection_Methods__Dictionary(),
                maze_sizes: new List<int>() { 8, 16 },
                iterations_for_feature_tables_csv: new List<int>() { 0, 1000, 10000, 100000 },
                iterations_for_feature_tables_png: new List<int>() { 100000 },
                iterations_for_drawing_mazes: new List<int>() { 100000 },
                iterations_for_data_logging: new List<int>() { 0, 1000, 10000, 100000 }
                );
        }

        public static void Experiment_19()
        {
            Experiment_Utilities.Run_Multi_Experiment(
                experiment_name__first_part: "Experiment_19",
                fitness_calculator: new MEM__Symmetry_Over_X_And_Y_Axes(),
                feature_1_calculator: new MEM__Percent_Corners(),
                feature_2_calculator: new MEM__Percent_Corridors(),
                parent_selection_methods: Experiment_Utilities.Selection_Methods__Dictionary(),
                maze_sizes: new List<int>() { 8, 16 },
                iterations_for_feature_tables_csv: new List<int>() { 0, 1000, 10000, 100000 },
                iterations_for_feature_tables_png: new List<int>() { 100000 },
                iterations_for_drawing_mazes: new List<int>() { 100000 },
                iterations_for_data_logging: new List<int>() { 0, 1000, 10000, 100000 }
                );
        }

        public static void Experiment_20()
        {
            Experiment_Utilities.Run_Multi_Experiment(
                experiment_name__first_part: "Experiment_20",
                fitness_calculator: new MEM__Symmetry_Over_X_And_Y_Axes(),
                feature_1_calculator: new MEM__Percent_Corners(),
                feature_2_calculator: new MEM__Symmetry_Over_Y_Axis(),
                parent_selection_methods: Experiment_Utilities.Selection_Methods__Dictionary(),
                maze_sizes: new List<int>() { 8, 16 },
                iterations_for_feature_tables_csv: new List<int>() { 0, 1000, 10000, 100000 },
                iterations_for_feature_tables_png: new List<int>() { 100000 },
                iterations_for_drawing_mazes: new List<int>() { 100000 },
                iterations_for_data_logging: new List<int>() { 0, 1000, 10000, 100000 }
                );
        }

        public static void Experiment_21()
        {
            Experiment_Utilities.Run_Multi_Experiment(
                experiment_name__first_part: "Experiment_21",
                fitness_calculator: new MEM__Symmetry_Over_X_And_Y_Axes(),
                feature_1_calculator: new MEM__Percent_Corners(),
                feature_2_calculator: new MEM__Medium_Solution_Length(),
                parent_selection_methods: Experiment_Utilities.Selection_Methods__Dictionary(),
                maze_sizes: new List<int>() { 8, 16 },
                iterations_for_feature_tables_csv: new List<int>() { 0, 1000, 10000, 100000 },
                iterations_for_feature_tables_png: new List<int>() { 100000 },
                iterations_for_drawing_mazes: new List<int>() { 100000 },
                iterations_for_data_logging: new List<int>() { 0, 1000, 10000, 100000 }
                );
        }

        public static void Experiment_22()
        {
            Experiment_Utilities.Run_Multi_Experiment(
                experiment_name__first_part: "Experiment_22",
                fitness_calculator: new MEM__Symmetry_Over_X_And_Y_Axes(),
                feature_1_calculator: new MEM__Percent_Corridors(),
                feature_2_calculator: new MEM__Percent_Corners(),
                parent_selection_methods: Experiment_Utilities.Selection_Methods__Dictionary(),
                maze_sizes: new List<int>() { 8, 16 },
                iterations_for_feature_tables_csv: new List<int>() { 0, 1000, 10000, 100000 },
                iterations_for_feature_tables_png: new List<int>() { 100000 },
                iterations_for_drawing_mazes: new List<int>() { 100000 },
                iterations_for_data_logging: new List<int>() { 0, 1000, 10000, 100000 }
                );
        }

        public static void Experiment_23()
        {
            Experiment_Utilities.Run_Multi_Experiment(
                experiment_name__first_part: "Experiment_23",
                fitness_calculator: new MEM__Symmetry_Over_X_And_Y_Axes(),
                feature_1_calculator: new MEM__Percent_Corridors(),
                feature_2_calculator: new MEM__Symmetry_Over_Y_Axis(),
                parent_selection_methods: Experiment_Utilities.Selection_Methods__Dictionary(),
                maze_sizes: new List<int>() { 8, 16 },
                iterations_for_feature_tables_csv: new List<int>() { 0, 1000, 10000, 100000 },
                iterations_for_feature_tables_png: new List<int>() { 100000 },
                iterations_for_drawing_mazes: new List<int>() { 100000 },
                iterations_for_data_logging: new List<int>() { 0, 1000, 10000, 100000 }
                );
        }

        public static void Experiment_24()
        {
            Experiment_Utilities.Run_Multi_Experiment(
                experiment_name__first_part: "Experiment_24",
                fitness_calculator: new MEM__Symmetry_Over_X_And_Y_Axes(),
                feature_1_calculator: new MEM__Percent_Corridors(),
                feature_2_calculator: new MEM__Medium_Solution_Length(),
                parent_selection_methods: Experiment_Utilities.Selection_Methods__Dictionary(),
                maze_sizes: new List<int>() { 8, 16 },
                iterations_for_feature_tables_csv: new List<int>() { 0, 1000, 10000, 100000 },
                iterations_for_feature_tables_png: new List<int>() { 100000 },
                iterations_for_drawing_mazes: new List<int>() { 100000 },
                iterations_for_data_logging: new List<int>() { 0, 1000, 10000, 100000 }
                );
        }

        public static void Experiment_25()
        {
            Experiment_Utilities.Run_Multi_Experiment(
                experiment_name__first_part: "Experiment_25",
                fitness_calculator: new MEM__Medium_Solution_Length(),
                feature_1_calculator: new MEM__Percent_Corners(),
                feature_2_calculator: new MEM__Percent_Corridors(),
                parent_selection_methods: Experiment_Utilities.Selection_Methods__Dictionary(),
                maze_sizes: new List<int>() { 8, 16 },
                iterations_for_feature_tables_csv: new List<int>() { 0, 1000, 10000, 100000 },
                iterations_for_feature_tables_png: new List<int>() { 100000 },
                iterations_for_drawing_mazes: new List<int>() { 100000 },
                iterations_for_data_logging: new List<int>() { 0, 1000, 10000, 100000 }
                );
        }

        public static void Experiment_26()
        {
            Experiment_Utilities.Run_Multi_Experiment(
                experiment_name__first_part: "Experiment_26",
                fitness_calculator: new MEM__Medium_Solution_Length(),
                feature_1_calculator: new MEM__Percent_Corners(),
                feature_2_calculator: new MEM__Symmetry_Over_Y_Axis(),
                parent_selection_methods: Experiment_Utilities.Selection_Methods__Dictionary(),
                maze_sizes: new List<int>() { 8, 16 },
                iterations_for_feature_tables_csv: new List<int>() { 0, 1000, 10000, 100000 },
                iterations_for_feature_tables_png: new List<int>() { 100000 },
                iterations_for_drawing_mazes: new List<int>() { 100000 },
                iterations_for_data_logging: new List<int>() { 0, 1000, 10000, 100000 }
                );
        }

        public static void Experiment_27()
        {
            Experiment_Utilities.Run_Multi_Experiment(
                experiment_name__first_part: "Experiment_27",
                fitness_calculator: new MEM__Medium_Solution_Length(),
                feature_1_calculator: new MEM__Percent_Corners(),
                feature_2_calculator: new MEM__Symmetry_Over_X_And_Y_Axes(),
                parent_selection_methods: Experiment_Utilities.Selection_Methods__Dictionary(),
                maze_sizes: new List<int>() { 8, 16 },
                iterations_for_feature_tables_csv: new List<int>() { 0, 1000, 10000, 100000 },
                iterations_for_feature_tables_png: new List<int>() { 100000 },
                iterations_for_drawing_mazes: new List<int>() { 100000 },
                iterations_for_data_logging: new List<int>() { 0, 1000, 10000, 100000 }
                );
        }

        public static void Experiment_28()
        {
            Experiment_Utilities.Run_Multi_Experiment(
                experiment_name__first_part: "Experiment_28",
                fitness_calculator: new MEM__Medium_Solution_Length(),
                feature_1_calculator: new MEM__Percent_Corridors(),
                feature_2_calculator: new MEM__Symmetry_Over_Y_Axis(),
                parent_selection_methods: Experiment_Utilities.Selection_Methods__Dictionary(),
                maze_sizes: new List<int>() { 8, 16 },
                iterations_for_feature_tables_csv: new List<int>() { 0, 1000, 10000, 100000 },
                iterations_for_feature_tables_png: new List<int>() { 100000 },
                iterations_for_drawing_mazes: new List<int>() { 100000 },
                iterations_for_data_logging: new List<int>() { 0, 1000, 10000, 100000 }
                );
        }

        public static void Experiment_29()
        {
            Experiment_Utilities.Run_Multi_Experiment(
                experiment_name__first_part: "Experiment_29",
                fitness_calculator: new MEM__Medium_Solution_Length(),
                feature_1_calculator: new MEM__Percent_Corridors(),
                feature_2_calculator: new MEM__Symmetry_Over_X_And_Y_Axes(),
                parent_selection_methods: Experiment_Utilities.Selection_Methods__Dictionary(),
                maze_sizes: new List<int>() { 8, 16 },
                iterations_for_feature_tables_csv: new List<int>() { 0, 1000, 10000, 100000 },
                iterations_for_feature_tables_png: new List<int>() { 100000 },
                iterations_for_drawing_mazes: new List<int>() { 100000 },
                iterations_for_data_logging: new List<int>() { 0, 1000, 10000, 100000 }
                );
        }

        public static void Experiment_30()
        {
            Experiment_Utilities.Run_Multi_Experiment(
                experiment_name__first_part: "Experiment_30",
                fitness_calculator: new MEM__Medium_Solution_Length(),
                feature_1_calculator: new MEM__Symmetry_Over_Y_Axis(),
                feature_2_calculator: new MEM__Symmetry_Over_X_And_Y_Axes(),
                parent_selection_methods: Experiment_Utilities.Selection_Methods__Dictionary(),
                maze_sizes: new List<int>() { 8, 16 },
                iterations_for_feature_tables_csv: new List<int>() { 0, 1000, 10000, 100000 },
                iterations_for_feature_tables_png: new List<int>() { 100000 },
                iterations_for_drawing_mazes: new List<int>() { 100000 },
                iterations_for_data_logging: new List<int>() { 0, 1000, 10000, 100000 }
                );
        }
    }
}
