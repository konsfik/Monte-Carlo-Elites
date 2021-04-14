using System;
using System.Collections.Generic;
//using System.Linq;

using Common_Tools.Elements;
using Common_Tools.Utilities;

namespace MapElites_Lib
{
    public class MEL_PSM__GREEDY_Parent_Fitness<T> : MEL__Parent_Selection_Method<T>
        where T:MEL__Individual
    {
        public override Vec2i Select_Parent_Coordinates(Random randomness_provider, MEL__Algorithm<T> map_elites)
        {
            //int w = map_elites.state.individuals.GetLength(0);
            //int h = map_elites.state.individuals.GetLength(1);

            double[,] parent_fitness_table = map_elites.Q_ST_Fitness_Table();
            double highest_fitness = parent_fitness_table.Max();

            Vec2i best_value_coordinates=  MEL_PSM__Help_Methods.Best_Value_Coordinates(
                randomness_provider,
                map_elites.state.individual_exists,
                parent_fitness_table,
                highest_fitness
                );

            return best_value_coordinates;

            //List<Vec2i> candidates = new List<Vec2i>();

            //for (int x = 0; x < w; x++)
            //{
            //    for (int y = 0; y < h; y++)
            //    {
            //        if (parent_fitness_table[x, y] == highest_fitness)
            //        {
            //            candidates.Add(new Vec2i(x, y));
            //        }
            //    }
            //}

            //return candidates.Random_Item(randomness_provider);
        }

        public override string ToString()
        {
            return this.GetType().Name.ToString();
        }
    }
}
