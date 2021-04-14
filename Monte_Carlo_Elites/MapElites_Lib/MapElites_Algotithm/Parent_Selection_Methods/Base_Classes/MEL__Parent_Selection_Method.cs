using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools.Elements;
using Common_Tools.Utilities;

namespace MapElites_Lib
{
    public abstract class MEL__Parent_Selection_Method<T>
        where T:MEL__Individual
    {
        public abstract Vec2i Select_Parent_Coordinates(
            Random randomness_provider,
            MEL__Algorithm<T> map_elites_algorithm
            );

        public abstract override string ToString();
    }
}
