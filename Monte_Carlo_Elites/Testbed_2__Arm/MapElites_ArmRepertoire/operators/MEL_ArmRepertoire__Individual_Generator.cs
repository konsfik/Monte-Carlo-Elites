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
    public class MEL_ArmRepertoire__Individual_Generator 
        : MEL__Individual_Generator<MEL_ArmRepertoire__Individual>
    {
        public int num_dimensions;
        public double min_gene_value;
        public double max_gene_value;

        public MEL_ArmRepertoire__Individual_Generator(
            int num_dimensions,
            double min_gene_value,
            double max_gene_value
            )
        {
            this.num_dimensions = num_dimensions;
            this.min_gene_value = min_gene_value;
            this.max_gene_value = max_gene_value;
        }

        public override MEL_ArmRepertoire__Individual Generate_Individual(Random randomness_provider)
        {
            List<double> genotype = new List<double>();

            for (int i = 0; i < num_dimensions; i++) {
                double this_gene_value = Math_Utilities.Map_Value(
                    randomness_provider.NextDouble(), 
                    0.0,
                    1.0,
                    min_gene_value,
                    max_gene_value
                    );
                genotype.Add(this_gene_value);
            }

            MEL_ArmRepertoire__Individual individual = new MEL_ArmRepertoire__Individual(
                genotype
                );

            return individual;
        }

        public override string ToString()
        {
            string description = "";
            description += this.GetType().Name.ToString();
            description += " | num_dimensions: " + num_dimensions.ToString();
            description += " | min_gene_value: " + min_gene_value.ToString();
            description += " | max_gene_value: " + max_gene_value.ToString();
            return description;
        }
    }
}
