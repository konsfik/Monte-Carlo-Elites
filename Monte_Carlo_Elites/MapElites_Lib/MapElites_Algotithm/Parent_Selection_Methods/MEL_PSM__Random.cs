using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Common_Tools.Elements;
using Common_Tools.Utilities;


namespace MapElites_Lib
{
    public class MEL_PSM__Random<T> : MEL__Parent_Selection_Method<T>
        where T:MEL__Individual
    {
        public override Vec2i Select_Parent_Coordinates(
            Random randomness_provider,
            MEL__Algorithm<T> map_elites
            )
        {
            int w = map_elites.state.individuals.GetLength(0);
            int h = map_elites.state.individuals.GetLength(1);

            List<Vec2i> active_positions = new List<Vec2i>(w * h);   // set list capacity
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    if (map_elites.state.individual_exists[x, y])
                    {
                        Vec2i p = new Vec2i(x, y);
                        active_positions.Add(p);
                    }
                }
            }

            return active_positions.Random_Item(randomness_provider);
        }

        public override string ToString()
        {
            return this.GetType().Name.ToString();
        }
    }
}
