using System;
using System.Collections.Generic;
using System.Text;

namespace Perfect_Mazes_Lib
{
    public abstract class PM__Maze_Evaluation_Method
    {
        public abstract double Evaluate(PM_Maze maze);

        public abstract override string ToString();
    }
}
