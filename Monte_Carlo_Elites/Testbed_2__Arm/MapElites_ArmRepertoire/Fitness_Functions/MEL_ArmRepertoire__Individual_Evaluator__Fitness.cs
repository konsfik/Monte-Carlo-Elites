using MapElites_Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools.Elements;
using Common_Tools.Utilities;

namespace Testbed_2__Arm
{

    public class MEL_ArmRepertoire__Individual_Evaluator__Fitness 
        : MEL__Individual_Evaluation_Method<MEL_ArmRepertoire__Individual>
    {

        public override double Calculate_Fitness(MEL_ArmRepertoire__Individual individual)
        {
            double gene_variance = individual.genotype.Variance();
            return -gene_variance;
        }

        public override string ToString()
        {
            return this.GetType().Name.ToString();
        }
    }
}
