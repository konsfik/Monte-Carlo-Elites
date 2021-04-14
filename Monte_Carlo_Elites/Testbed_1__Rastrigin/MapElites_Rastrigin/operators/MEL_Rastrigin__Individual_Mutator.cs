using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MapElites_Lib;

using Common_Tools.Elements;
using Common_Tools.Utilities;

namespace Testbed_1__Rastrigin
{
    public class MEL_Rastrigin__Individual_Mutator : MEL__Individual_Mutator<MEL_Rastrigin__Individual>
    {
        public double mutation_rate;
        public double min_gene_value;
        public double max_gene_value;

        public MEL_Rastrigin__Individual_Mutator(
            double mutation_step,
            double min_gene_value,
            double max_gene_value
            )
        {
            this.mutation_rate = mutation_step;
            this.min_gene_value = min_gene_value;
            this.max_gene_value = max_gene_value;
        }

        public override MEL_Rastrigin__Individual Generate_Offspring(
            Random randomness_provider,
            MEL_Rastrigin__Individual parent
            )
        {
            List<double> genome_copy = parent.genotype.DeepCopy();
            
            for (int i = 0; i < genome_copy.Count; i++)
            {
                double mutation_value = Math_Utilities.Map_Value(
                    randomness_provider.NextDouble(),
                    0.0,
                    1.0,
                    -mutation_rate,
                    mutation_rate
                    );

                genome_copy[i] += mutation_value;

                if (genome_copy[i] > max_gene_value)
                {
                    genome_copy[i] = max_gene_value;
                }
                else if (genome_copy[i] < min_gene_value)
                {
                    genome_copy[i] = min_gene_value;
                }
            }

            MEL_Rastrigin__Individual offspring = new MEL_Rastrigin__Individual(genome_copy);

            return offspring;
        }

        public override string ToString()
        {
            string description = "";
            description += this.GetType().Name.ToString();
            description += " | mutation_rate: " + mutation_rate.ToString();
            description += " | min_gene_value: " + min_gene_value.ToString();
            description += " | max_gene_value: " + max_gene_value.ToString();
            return description;
        }
    }
}
