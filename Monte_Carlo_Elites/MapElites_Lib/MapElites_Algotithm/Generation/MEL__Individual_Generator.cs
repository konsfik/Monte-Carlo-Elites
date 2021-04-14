using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapElites_Lib
{
    public abstract class MEL__Individual_Generator<T>
        where T:MEL__Individual
    {
        public abstract T Generate_Individual(System.Random randomness_provider);

        public abstract override string ToString();
    }
}
