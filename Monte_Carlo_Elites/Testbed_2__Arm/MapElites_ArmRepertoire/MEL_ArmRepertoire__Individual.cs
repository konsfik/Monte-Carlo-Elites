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
    public class MEL_ArmRepertoire__Individual : MEL__Individual
    {
        public List<double> genotype;
        
        public MEL_ArmRepertoire__Individual(List<double> genotype)
        {
            this.genotype = genotype.DeepCopy();
        }

        public int Num_Dimensions()
        {
            return genotype.Count;
        }
    }
}
