using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using MapElites_Lib;
using Common_Tools.Utilities;
using Common_Tools.Elements;

using Testbed_2__Arm;

namespace Testbed_2__Arm__Exp
{
    public delegate void Del();

    class Program
    {

        static void Main(string[] args)
        {
            var methods = typeof(Program).GetMethods(BindingFlags.Public | BindingFlags.Static)
                .ToList()
                .FindAll(x => x.Name.Contains("Arm_"));

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

        /// <summary>
        /// SLM_0: Random selection (R)
        /// </summary>
        public static void Arm_12D__SLM_0()
        {
            // settings:
            string experiment_name = "Arm_12D__SLM_0";

            MEL__Parent_Selection_Method<MEL_ArmRepertoire__Individual> parent_selection_method =
                new MEL_PSM__Random<MEL_ArmRepertoire__Individual>();

            ArmRepertoire_Experiment_Utilities.Run_Experiment(
                experiment_name,
                parent_selection_method,
                12
                );
        }

        /// <summary>
        /// SLM_1: Greedy selection (G)
        /// </summary>
        public static void Arm_12D__SLM_1()
        {
            // settings:
            string experiment_name = "Arm_12D__SLM_1";

            MEL__Parent_Selection_Method<MEL_ArmRepertoire__Individual> parent_selection_method =
                new MEL_PSM__GREEDY_Parent_Fitness<MEL_ArmRepertoire__Individual>();

            ArmRepertoire_Experiment_Utilities.Run_Experiment(
                experiment_name,
                parent_selection_method,
                12
                );
        }

        /// <summary>
        /// SLM_2: Exploit offspring survival, per individual (Ei)
        /// </summary>
        public static void Arm_12D__SLM_2()
        {
            // settings:
            string experiment_name = "Arm_12D__SLM_2";

            MEL__Parent_Selection_Method<MEL_ArmRepertoire__Individual> parent_selection_method =
                new MEL_PSM__EXPLOIT_Offspring_Survival__Per__Individual<MEL_ArmRepertoire__Individual>();

            ArmRepertoire_Experiment_Utilities.Run_Experiment(
                experiment_name,
                parent_selection_method,
                12
                );
        }

        /// <summary>
        /// SLM_6: Exploit offspring cell, per cell (Ec)
        /// </summary>
        public static void Arm_12D__SLM_6()
        {
            // settings:
            string experiment_name = "Arm_12D__SLM_6";

            MEL__Parent_Selection_Method<MEL_ArmRepertoire__Individual> parent_selection_method =
                new MEL_PSM__EXPLOIT_Offspring_Survival__Per__Location<MEL_ArmRepertoire__Individual>();

            ArmRepertoire_Experiment_Utilities.Run_Experiment(
                experiment_name,
                parent_selection_method,
                12
                );
        }

        /// <summary>
        /// SLM_10: UCB offspring cell, per individual (Ui)
        /// </summary>
        public static void Arm_12D__SLM_10()
        {
            // settings:
            string experiment_name = "Arm_12D__SLM_10";

            MEL__Parent_Selection_Method<MEL_ArmRepertoire__Individual> parent_selection_method =
                new MEL_PSM__UCB_Offspring_Survival__Per__Individual<MEL_ArmRepertoire__Individual>(1.0 / Math.Sqrt(2.0));

            ArmRepertoire_Experiment_Utilities.Run_Experiment(
                experiment_name,
                parent_selection_method,
                12
                );
        }

        /// <summary>
        /// SLM_14: UCB offspring cell, per cell (Uc)
        /// </summary>
        public static void Arm_12D__SLM_14()
        {
            // settings:
            string experiment_name = "Arm_12D__SLM_14";

            MEL__Parent_Selection_Method<MEL_ArmRepertoire__Individual> parent_selection_method =
                new MEL_PSM__UCB_Offspring_Survival__Per__Location<MEL_ArmRepertoire__Individual>(1.0 / Math.Sqrt(2.0));

            ArmRepertoire_Experiment_Utilities.Run_Experiment(
                experiment_name,
                parent_selection_method,
                12
                );
        }

        /// <summary>
        /// SLM_22: Exploration, per individual (Xi)
        /// </summary>
        public static void Arm_12D__SLM_22()
        {
            // settings:
            string experiment_name = "Arm_12D__SLM_22";

            MEL__Parent_Selection_Method<MEL_ArmRepertoire__Individual> parent_selection_method =
                new MEL_PSM__EXPLORE__Per__Individual<MEL_ArmRepertoire__Individual>();

            ArmRepertoire_Experiment_Utilities.Run_Experiment(
                experiment_name,
                parent_selection_method,
                12
                );
        }

        /// <summary>
        /// SLM_23: Exploration, per cell (Xc)
        /// </summary>
        public static void Arm_12D__SLM_23()
        {
            // settings:
            MEL__Parent_Selection_Method<MEL_ArmRepertoire__Individual> parent_selection_method =
                new MEL_PSM__EXPLORE__Per__Location<MEL_ArmRepertoire__Individual>();

            ArmRepertoire_Experiment_Utilities.Run_Experiment(
                experiment_name: "Arm_12D__SLM_23",
                parent_selection_method: parent_selection_method,
                number_of_dimensions: 12
                );
        }

        /// <summary>
        /// SLM_25: Curiosity Score (C)
        /// </summary>
        public static void Arm_12D__SLM_25()
        {
            // settings:
            string experiment_name = "Arm_12D__SLM_25";

            MEL__Parent_Selection_Method<MEL_ArmRepertoire__Individual> parent_selection_method =
                new MEL_PSM__Curiosity<MEL_ArmRepertoire__Individual>(
                    1.0,
                    -0.5
                    );

            ArmRepertoire_Experiment_Utilities.Run_Experiment(
                experiment_name,
                parent_selection_method,
                12
                );
        }

    }
}
