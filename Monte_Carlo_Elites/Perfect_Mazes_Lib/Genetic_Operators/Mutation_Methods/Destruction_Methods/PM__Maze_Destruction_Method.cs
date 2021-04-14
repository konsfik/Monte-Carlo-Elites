using System;
using System.Collections.Generic;
using System.Text;

namespace Perfect_Mazes_Lib
{
    public abstract class PM__Maze_Destruction_Method
    {
        public abstract void DestroyMaze(
            Random rand,
            PM_Maze maze
            );

        public abstract override string ToString();
    }
}
