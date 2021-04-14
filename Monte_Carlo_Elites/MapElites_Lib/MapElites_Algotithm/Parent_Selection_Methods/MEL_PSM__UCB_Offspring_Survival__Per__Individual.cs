using System;
using System.Collections.Generic;
using System.Linq;
using Perfect_Mazes_Lib;

using Common_Tools.Elements;
using Common_Tools.Utilities;

namespace MapElites_Lib
{
    public class MEL_PSM__UCB_Offspring_Survival__Per__Individual<T> : MEL__Parent_Selection_Method__UCB<T>
        where T:MEL__Individual
    {
        public MEL_PSM__UCB_Offspring_Survival__Per__Individual(double c_value):base(c_value)
        {

        }

        public override Vec2i Select_Parent_Coordinates(
            Random randomness_provider, 
            MEL__Algorithm<T> map_elites
            )
        {
            double[,] ucb_table = MEL_PSM__Help_Methods.UCB_Table_Calculation(
                map_elites.state.individual_exists,
                map_elites.state.selections__per__individual,
                map_elites.state.offspring_survivals__per__individual,
                c_value,
                out double highest_ucb_value
                );

            Vec2i selected_coordinates = MEL_PSM__Help_Methods.Best_Value_Coordinates(
                randomness_provider,
                map_elites.state.individual_exists,
                ucb_table,
                highest_ucb_value
                );

            return selected_coordinates;
        }

        public override string ToString()
        {
            return this.GetType().Name.ToString() + " | C_VALUE: " + c_value.ToString();
        }
    }
}
