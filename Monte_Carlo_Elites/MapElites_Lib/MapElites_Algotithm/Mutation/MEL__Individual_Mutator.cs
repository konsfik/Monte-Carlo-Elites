using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapElites_Lib
{
    public abstract class MEL__Individual_Mutator<T>
        where T:MEL__Individual
    {
        public abstract T Generate_Offspring(
            Random randomness_provider,
            T parent
            );

        public abstract override string ToString();
    }
}
