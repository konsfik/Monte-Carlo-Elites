using MapElites_Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testbed_1__Rastrigin
{

    public class MEL_Rastrigin__Fitness__Evaluation_Method : MEL__Individual_Evaluation_Method<MEL_Rastrigin__Individual>
    {
        public override double Calculate_Fitness(
            MEL_Rastrigin__Individual individual
            )
        {
            double sum = 0.0;

            foreach (var x in individual.genotype)
            {
                sum += x * x + Math.Cos(2.0 * Math.PI * x);
            }

            int num_dimensions = individual.Num_Dimensions();

            double fitness = 10.0 * num_dimensions + sum;

            return fitness;
        }

        public override string ToString()
        {
            return this.GetType().Name.ToString();
        }
    }
}
