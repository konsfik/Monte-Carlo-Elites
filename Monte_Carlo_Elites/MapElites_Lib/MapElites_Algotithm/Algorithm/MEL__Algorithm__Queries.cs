using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Common_Tools.Elements;
using Common_Tools.Utilities;

using Perfect_Mazes_Lib;

namespace MapElites_Lib
{
    public static class MEL__Algorithm__Queries
    {
        public static int Q_Num_Positions<T>(this MEL__Algorithm<T> map_elites)
            where T:MEL__Individual
        {
            return map_elites.state.individuals.GetLength(0) * map_elites.state.individuals.GetLength(1);
        }

        public static int Q_Num_Filled_Positions<T>(this MEL__Algorithm<T> map_elites)
            where T:MEL__Individual
        {
            int width = map_elites.state.individuals.GetLength(0);
            int height = map_elites.state.individuals.GetLength(1);

            int num_filled = 0;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (map_elites.state.individuals[x, y] != null)
                    {
                        num_filled++;
                    }
                }
            }
            return num_filled;
        }

        public static double Q_Maximum_Fitness<T>(this MEL__Algorithm<T> map_elites)
            where T:MEL__Individual
        {
            int w = map_elites.state.individuals.GetLength(0);
            int h = map_elites.state.individuals.GetLength(1);

            double max_fitness = Double.NegativeInfinity;

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    if (map_elites.state.individuals[x, y] != null)
                    {
                        if (map_elites.state.individuals[x, y].fitness > max_fitness)
                        {
                            max_fitness = map_elites.state.individuals[x, y].fitness;
                        }
                    }
                }
            }

            return max_fitness;
        }

        public static int Q_Number_Of_Active_Individuals<T>(this MEL__Algorithm<T> map_elites)
            where T:MEL__Individual
        {
            int w = map_elites.state.individuals.GetLength(0);
            int h = map_elites.state.individuals.GetLength(1);

            int num_inds = 0;

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    if (map_elites.state.individuals[x, y] != null)
                    {
                        num_inds++;
                    }
                }
            }

            return num_inds;
        }

        public static double[,] Q_ST_Fitness_Table<T>(
            this MEL__Algorithm<T> map_elites
            )
            where T:MEL__Individual
        {
            int w = map_elites.state.individuals.GetLength(0);
            int h = map_elites.state.individuals.GetLength(1);

            double[,] fitness_table = new double[w, h];

            for (int x = 0; x < w; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    if (map_elites.state.individuals[x, y] == null)
                    {
                        fitness_table[x, y] = Double.NaN;
                    }
                    else
                    {
                        fitness_table[x, y] = map_elites.state.individuals[x, y].fitness;
                    }
                }
            }

            return fitness_table;
        }

    }
}
