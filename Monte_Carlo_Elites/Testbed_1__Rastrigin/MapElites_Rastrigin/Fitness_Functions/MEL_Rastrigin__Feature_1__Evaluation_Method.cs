﻿using MapElites_Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testbed_1__Rastrigin
{
    public class MEL_Rastrigin__Feature_1__Evaluation_Method : MEL__Individual_Evaluation_Method<MEL_Rastrigin__Individual>
    {
        public override double Calculate_Fitness(MEL_Rastrigin__Individual individual)
        {
            return individual.genotype[0];
        }

        public override string ToString()
        {
            return this.GetType().Name.ToString();
        }
    }
}
