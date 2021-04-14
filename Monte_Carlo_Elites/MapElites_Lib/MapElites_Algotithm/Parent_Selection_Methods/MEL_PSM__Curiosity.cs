using Common_Tools.Elements;
using Common_Tools.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapElites_Lib
{
    public class MEL_PSM__Curiosity<T> : MEL__Parent_Selection_Method<T>
        where T : MEL__Individual
    {
        public readonly double reward;
        public readonly double penalty;

        public MEL_PSM__Curiosity(
            double reward,
            double penalty
            )
        {
            this.reward = reward;
            this.penalty = penalty;
        }


        public override Vec2i Select_Parent_Coordinates(Random randomness_provider, MEL__Algorithm<T> map_elites_algorithm)
        {
            // first calculate the curiosity score for all individuals.
            // curiosity score must be calculated per individual, not per location.
            double[,] curiosity_table = MEL_PSM__Help_Methods.Curiosity_Score__Table_Calculation(
                individual_exists_table: map_elites_algorithm.state.individual_exists,
                selections_table: map_elites_algorithm.state.selections__per__individual,
                success_table: map_elites_algorithm.state.offspring_survivals__per__individual,
                curiosity_reward_value: reward,
                curiosity_penalty_value: penalty,
                out double max_curiosity_value,
                out double min_curiosity_value
                );

            int w = curiosity_table.GetLength(0);
            int h = curiosity_table.GetLength(1);

            // subtract the minimum curiosity value from all the table's cells to bring the minimum to zero.
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    if (Double.IsNaN(curiosity_table[x, y]) == false)
                    {
                        // offset the curiosity of that position so as to bring the minimum to zero
                        // i.e. subtract the minimum value from it.
                        curiosity_table[x, y] -= min_curiosity_value;
                    }
                }
            }

            // recalculate the minimum and maximum value.
            max_curiosity_value = max_curiosity_value - min_curiosity_value;
            min_curiosity_value = 0.0;

            // calculate the value range
            double value_range = max_curiosity_value - min_curiosity_value;

            if (value_range == 0)
            {
                List<Vec2i> available_positions = new List<Vec2i>();
                for (int x = 0; x < w; x++)
                {
                    for (int y = 0; y < h; y++)
                    {
                        if (Double.IsNaN(curiosity_table[x, y]) == false)
                        {
                            //// since this position is available, add it to the list.
                            available_positions.Add(new Vec2i(x, y));
                        }
                    }
                }
                return available_positions.Random_Item(randomness_provider);
            }

            double curiosity_sum = 0.0;
            foreach (double cur in curiosity_table)
            {
                if (double.IsNaN(cur) == false)
                {
                    curiosity_sum += cur;
                }
            }

            // roll the dice.
            double dice_roll = randomness_provider.NextDouble() * curiosity_sum;

            double partial_sum = 0.0;

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    double position_curiosity = curiosity_table[x, y];
                    if (Double.IsNaN(position_curiosity) == false)
                    {
                        partial_sum += position_curiosity;
                        if (partial_sum >= dice_roll)
                        {
                            return new Vec2i(x, y);
                        }
                    }
                }
            }

            throw new System.Exception("error here!");
        }

        public override string ToString()
        {
            return this.GetType().Name.ToString();
        }
    }
}
