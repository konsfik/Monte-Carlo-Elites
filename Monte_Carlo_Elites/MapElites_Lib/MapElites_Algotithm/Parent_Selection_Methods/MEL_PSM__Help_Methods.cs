using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Common_Tools.Elements;
using Common_Tools.Utilities;

namespace MapElites_Lib
{
    public static class MEL_PSM__Help_Methods
    {
        public static Vec2i Best_Value_Coordinates(
            Random rand,
            bool[,] existence_table,
            double[,] value_table,
            double best_value
            )
        {
            int w = existence_table.GetLength(0);
            int h = existence_table.GetLength(1);

            List<Vec2i> candidates = new List<Vec2i>();

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    if (existence_table[x, y]
                        &&
                        value_table[x, y] == best_value
                        )
                    {
                        candidates.Add(new Vec2i(x, y));
                    }
                }
            }

            return candidates.Random_Item(rand);
        }


        public static double UCB_Value_Calculation(
            double times_selected,
            double reward_sum,
            double parent_times_selected,
            double c_value
            )
        {
            if (times_selected <= 0)
            {
                return Double.PositiveInfinity;
            }
            else
            {
                double ni = times_selected;
                double wi = reward_sum;
                double Ni = parent_times_selected;

                double ucb_1_value = (wi / ni) + c_value * Math.Sqrt(Math.Log(Ni) / ni);

                return ucb_1_value;
            }
        }

        public static double[,] UCB_Table_Calculation(
            bool[,] individual_exists_table,
            int[,] selections_table,
            int[,] rewards_table,
            double c_value,
            out double max_ucb_value
            )
        {
            max_ucb_value = Double.NegativeInfinity;

            int w = selections_table.GetLength(0);
            int h = selections_table.GetLength(1);

            int selections_sum = selections_table.Sum();

            double[,] ucb_table = new double[w, h];
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    if (individual_exists_table[x, y] == false)
                    {
                        ucb_table[x, y] = Double.NaN;
                    }
                    else
                    {
                        int here__selections = selections_table[x, y];
                        double here__rewward = rewards_table[x, y];

                        double this_ucb_value = UCB_Value_Calculation(
                            here__selections,
                            here__rewward,
                            selections_sum,
                            c_value
                            );

                        ucb_table[x, y] = this_ucb_value;

                        if (this_ucb_value > max_ucb_value)
                        {
                            max_ucb_value = this_ucb_value;
                        }
                    }
                }
            }

            return ucb_table;
        }

        public static double[,] UCB_Table_Calculation(
            bool[,] individual_exists_table,
            int[,] selections_table,
            double[,] rewards_table,
            double c_value,
            out double max_ucb_value
            )
        {
            max_ucb_value = Double.NegativeInfinity;

            int w = selections_table.GetLength(0);
            int h = selections_table.GetLength(1);

            int selections_sum = selections_table.Sum();

            double[,] ucb_table = new double[w, h];
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    if (individual_exists_table[x, y] == false)
                    {
                        ucb_table[x, y] = Double.NaN;
                    }
                    else
                    {
                        int here__selections = selections_table[x, y];
                        double here__rewward = rewards_table[x, y];

                        double this_ucb_value = UCB_Value_Calculation(
                            here__selections,
                            here__rewward,
                            selections_sum,
                            c_value
                            );

                        ucb_table[x, y] = this_ucb_value;

                        if (this_ucb_value > max_ucb_value)
                        {
                            max_ucb_value = this_ucb_value;
                        }
                    }
                }
            }

            return ucb_table;
        }

        public static double EXPLOIT_Value_Calculation(
            double times_selected,
            double reward_sum
            )
        {
            if (times_selected <= 0)
            {
                return Double.PositiveInfinity;
            }
            else
            {
                return reward_sum / times_selected;
            }
        }

        public static double[,] Curiosity_Score__Table_Calculation(
            bool[,] individual_exists_table,
            int[,] selections_table,
            int[,] success_table,
            double curiosity_reward_value,
            double curiosity_penalty_value,
            out double max_curiosity_value,
            out double min_curiosity_value
            )
        {
            max_curiosity_value = Double.NegativeInfinity;
            min_curiosity_value = Double.PositiveInfinity;

            int w = selections_table.GetLength(0);
            int h = selections_table.GetLength(1);

            double[,] curiosity_table = new double[w, h];
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    if (individual_exists_table[x, y] == false)
                    {
                        curiosity_table[x, y] = Double.NaN;
                    }
                    else
                    {
                        // number of selections at this location
                        double here__selections = selections_table[x, y];
                        // number of successful offspring at this location
                        double here_success = success_table[x, y];
                        // number of failed offspring at this location
                        double here_failure = here__selections - here_success;
                        
                        double here_curiosity_reward = here_success * curiosity_reward_value;
                        double here_curiosity_penalty = here_failure * curiosity_penalty_value;

                        double here_curiosity_score = here_curiosity_reward + here_curiosity_penalty;

                        curiosity_table[x, y] = here_curiosity_score;

                        if (here_curiosity_score > max_curiosity_value)
                        {
                            max_curiosity_value = here_curiosity_score;
                        }
                        if (here_curiosity_score < min_curiosity_value)
                        {
                            min_curiosity_value = here_curiosity_score;
                        }
                    }
                }
            }

            return curiosity_table;
        }

        public static double[,] EXPLOIT_Table_Calculation(
            bool[,] individual_exists_table,
            int[,] selections_table,
            int[,] rewards_table,
            out double max_exploit_value
            )
        {
            max_exploit_value = Double.NegativeInfinity;

            int w = selections_table.GetLength(0);
            int h = selections_table.GetLength(1);

            double[,] exploit_table = new double[w, h];
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    if (individual_exists_table[x, y] == false)
                    {
                        exploit_table[x, y] = Double.NaN;
                    }
                    else
                    {
                        double here__selections = selections_table[x, y];
                        double here__reward = rewards_table[x, y];

                        double this_exploit_value = EXPLOIT_Value_Calculation(
                            here__selections,
                            here__reward
                            );

                        exploit_table[x, y] = this_exploit_value;

                        if (this_exploit_value > max_exploit_value)
                        {
                            max_exploit_value = this_exploit_value;
                        }
                    }
                }
            }

            return exploit_table;
        }

        public static double[,] EXPLOIT_Table_Calculation(
            bool[,] individual_exists_table,
            int[,] selections_table,
            double[,] rewards_table,
            out double max_exploit_value
            )
        {
            max_exploit_value = Double.NegativeInfinity;

            int w = selections_table.GetLength(0);
            int h = selections_table.GetLength(1);

            double[,] exploit_table = new double[w, h];
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    if (individual_exists_table[x, y] == false)
                    {
                        exploit_table[x, y] = Double.NaN;
                    }
                    else
                    {
                        double here__selections = selections_table[x, y];
                        double here__reward = rewards_table[x, y];

                        double this_exploit_value = EXPLOIT_Value_Calculation(
                            here__selections,
                            here__reward
                            );

                        exploit_table[x, y] = this_exploit_value;

                        if (this_exploit_value > max_exploit_value)
                        {
                            max_exploit_value = this_exploit_value;
                        }
                    }
                }
            }

            return exploit_table;
        }

        public static double EXPLORATION_Value_Calcualtion(
            double times_selected
            )
        {
            if (times_selected == 0.0)
            {
                return Double.PositiveInfinity;
            }
            else
            {
                return 1.0 / times_selected;
            }
        }

        public static double[,] EXPLORATION_Table_Calculation(
            bool[,] individual_exists_table,
            int[,] selections_table,
            out double max_exploration_value
            )
        {
            max_exploration_value = Double.NegativeInfinity;

            int w = selections_table.GetLength(0);
            int h = selections_table.GetLength(1);

            double[,] exploration_table = new double[w, h];
            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    if (individual_exists_table[x, y] == false)
                    {
                        exploration_table[x, y] = Double.NaN;
                    }
                    else
                    {
                        double here__selections = selections_table[x, y];

                        double exploration_value = EXPLORATION_Value_Calcualtion(here__selections);

                        exploration_table[x, y] = exploration_value;

                        if (exploration_value > max_exploration_value)
                        {
                            max_exploration_value = exploration_value;
                        }
                    }
                }
            }

            return exploration_table;
        }
    }
}
