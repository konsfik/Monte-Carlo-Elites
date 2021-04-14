using System;
using System.Collections.Generic;
using System.Text;

namespace Perfect_Mazes_Lib
{
    public abstract class PM__Maze_Generation_Method
    {
        public abstract PM_Maze Generate_Maze(
            Random rand,
            int width,
            int height
            );

        public abstract string Description();

        public abstract override string ToString();
    }
}
