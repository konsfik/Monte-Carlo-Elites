using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MapElites_Lib;

using Common_Tools.Elements;
using Common_Tools.Utilities;

namespace Testbed_2__Arm
{
    public class MEL_ArmRepertoire__Individual_Mutator__Mutation_Step__Uniform_Rate 
        : MEL__Individual_Mutator<MEL_ArmRepertoire__Individual>
    {
        public double mutation_step;
        public double min_gene_value;
        public double max_gene_value;

        public MEL_ArmRepertoire__Individual_Mutator__Mutation_Step__Uniform_Rate(
            double mutation_rate,
            double min_gene_value,
            double max_gene_value
            )
        {
            this.mutation_step = mutation_rate;
            this.min_gene_value = min_gene_value;
            this.max_gene_value = max_gene_value;
        }

        public override MEL_ArmRepertoire__Individual Generate_Offspring(
            Random randomness_provider,
            MEL_ArmRepertoire__Individual parent
            )
        {
            List<double> genome_copy = parent.genotype.DeepCopy();

            double value_range = max_gene_value - min_gene_value;

            for (int i = 0; i < genome_copy.Count; i++)
            {
                double mutation_value = Math_Utilities.Map_Value(
                    randomness_provider.NextDouble(),
                    0.0,
                    1.0,
                    -mutation_step,
                    mutation_step
                    );

                genome_copy[i] += mutation_value;

                if (genome_copy[i] > max_gene_value)
                {
                    genome_copy[i] -= value_range;
                }
                else if (genome_copy[i] < min_gene_value)
                {
                    genome_copy[i] += value_range;
                }
            }

            MEL_ArmRepertoire__Individual offspring = new MEL_ArmRepertoire__Individual(genome_copy);

            return offspring;
        }

        public override string ToString()
        {
            string description = "";
            description += this.GetType().Name.ToString();
            description += " | mutation_rate: " + mutation_step.ToString();
            description += " | min_gene_value: " + min_gene_value.ToString();
            description += " | max_gene_value: " + max_gene_value.ToString();
            return description;
        }
    }
}
