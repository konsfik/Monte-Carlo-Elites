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
    public class MEL_Rastrigin__Individual : MEL__Individual
    {
        public List<double> genotype;
        
        public MEL_Rastrigin__Individual(List<double> genotype)
        {
            this.genotype = genotype.DeepCopy();
        }

        public int Num_Dimensions()
        {
            return genotype.Count;
        }
    }
}
