using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapElites_Lib
{
    public class MEL__State<T>
        where T:MEL__Individual
    {
        public T[,] individuals;

        public bool[,] individual_exists;

        public int[,] selections__per__location;
        public int[,] offspring_survivals__per__location;

        public int[,] selections__per__individual;
        public int[,] offspring_survivals__per__individual;

        public MEL__State(int feature_1_subdivisions, int feature_2_subdivisions)
        {
            individuals = new T[feature_1_subdivisions, feature_2_subdivisions];

            individual_exists = new bool[feature_1_subdivisions, feature_2_subdivisions];

            selections__per__location = new int[feature_1_subdivisions, feature_2_subdivisions];
            offspring_survivals__per__location = new int[feature_1_subdivisions, feature_2_subdivisions];

            selections__per__individual = new int[feature_1_subdivisions, feature_2_subdivisions];
            offspring_survivals__per__individual = new int[feature_1_subdivisions, feature_2_subdivisions];

            // initialization of all state - tables:
            for (int x = 0; x < feature_1_subdivisions; x++)
            {
                for (int y = 0; y < feature_2_subdivisions; y++)
                {
                    individual_exists[x, y] = false;

                    selections__per__location[x, y] = 0;                          // initialized to zero (0)
                    offspring_survivals__per__location[x, y] = 0;           // initialized to zero (0)

                    selections__per__individual[x, y] = 0;
                    offspring_survivals__per__individual[x, y] = 0;
                }
            }
        }
    }
}
