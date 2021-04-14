using MapElites_Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testbed_2__Arm
{
    public class MEL_ArmRepertoire__Individual_Evaluator__Feature_1 
        : MEL__Individual_Evaluation_Method<MEL_ArmRepertoire__Individual>
    {

        public override double Calculate_Fitness(MEL_ArmRepertoire__Individual individual)
        {
            //l1 cos(y1) + l2 cos(y1 + y2) + · · · + ln cos(y1 + · · · + yn)
            int num_genes = individual.Num_Dimensions();
            double joint_length = 1.0 / (double)num_genes;

            double feature_1 = 0.0;
            for (int i = 0; i < num_genes; i++)
            {
                double partial_angle_sum = 0;
                for (int j = 0; j <= i; j++)
                {
                    double current_angle = individual.genotype[j];
                    partial_angle_sum += current_angle;
                }
                feature_1 += joint_length * Math.Cos(partial_angle_sum);
            }
            return feature_1;
        }

        public override string ToString()
        {
            return this.GetType().Name.ToString();
        }
    }
}
