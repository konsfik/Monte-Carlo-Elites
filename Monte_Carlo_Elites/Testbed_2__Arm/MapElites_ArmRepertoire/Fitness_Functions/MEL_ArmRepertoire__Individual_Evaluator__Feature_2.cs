using MapElites_Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testbed_2__Arm
{
    public class MEL_ArmRepertoire__Individual_Evaluator__Feature_2 
        : MEL__Individual_Evaluation_Method<MEL_ArmRepertoire__Individual>
    {

        public override double Calculate_Fitness(MEL_ArmRepertoire__Individual individual)
        {
            int num_genes = individual.Num_Dimensions();
            double joint_length = 1.0 / (double)num_genes;

            double feature_2 = 0.0;
            for (int i = 0; i < num_genes; i++)
            {
                double partial_angle_sum = 0;
                for (int j = 0; j <= i; j++)
                {
                    double current_angle = individual.genotype[j];
                    partial_angle_sum += current_angle;
                }
                feature_2 += joint_length * Math.Sin(partial_angle_sum);
            }
            return feature_2;
        }

        public override string ToString()
        {
            return this.GetType().Name.ToString();
        }
    }
}
