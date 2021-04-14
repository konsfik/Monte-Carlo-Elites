using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Perfect_Mazes_Lib;

namespace MapElites_Lib
{
    public class MEL__Operator_Settings<T>
        where T: MEL__Individual
    {
        public MEL__Individual_Generator<T> individual_generator;
        public MEL__Individual_Mutator<T> individual_mutator;

        public MEL__Operator_Settings(
            MEL__Individual_Generator<T> individual_generator,
            MEL__Individual_Mutator<T> individual_mutator
            )
        {
            this.individual_generator = individual_generator;
            this.individual_mutator = individual_mutator;
        }
    }
}
