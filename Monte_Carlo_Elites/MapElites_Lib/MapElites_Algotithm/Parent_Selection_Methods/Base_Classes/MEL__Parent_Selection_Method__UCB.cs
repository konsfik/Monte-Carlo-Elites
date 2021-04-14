using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapElites_Lib
{
    public abstract class MEL__Parent_Selection_Method__UCB<T> : MEL__Parent_Selection_Method<T>
        where T:MEL__Individual
    {
        public double c_value;

        public MEL__Parent_Selection_Method__UCB(double c_value)
        {
            this.c_value = c_value;
        }
    }
}
