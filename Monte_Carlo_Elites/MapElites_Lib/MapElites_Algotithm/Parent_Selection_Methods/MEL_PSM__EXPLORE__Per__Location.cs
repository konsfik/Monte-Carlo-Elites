using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools.Elements;
using Common_Tools.Utilities;

namespace MapElites_Lib
{
    public class MEL_PSM__EXPLORE__Per__Location<T> : MEL__Parent_Selection_Method<T>
        where T:MEL__Individual
    {
        public override Vec2i Select_Parent_Coordinates(
            Random randomness_provider,
            MEL__Algorithm<T> map_elites
            )
        {
            double[,] exploration_table = MEL_PSM__Help_Methods.EXPLORATION_Table_Calculation(
                map_elites.state.individual_exists,
                map_elites.state.selections__per__location,
                out double highest_ucb_value
                );

            Vec2i selected_coordinates = MEL_PSM__Help_Methods.Best_Value_Coordinates(
                randomness_provider,
                map_elites.state.individual_exists,
                exploration_table,
                highest_ucb_value
                );

            return selected_coordinates;
        }

        public override string ToString()
        {
            return this.GetType().Name.ToString();
        }
    }
}
