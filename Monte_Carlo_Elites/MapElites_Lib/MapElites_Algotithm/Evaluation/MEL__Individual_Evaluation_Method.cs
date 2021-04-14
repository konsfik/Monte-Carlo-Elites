using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapElites_Lib
{
    public abstract class MEL__Individual_Evaluation_Method<T>
        where T:MEL__Individual
    {

        public abstract double Calculate_Fitness(T individual);

        public abstract override string ToString();
    }
}
