using System;
using System.Collections.Generic;
using System.Text;

namespace Perfect_Mazes_Lib
{
    public class MRM__Random_DFS : PM__Maze_Repair_Method
    {
        public override void RefillMaze(Random rand, PM_Maze maze)
        {
            maze.Fill_Random_DFS(rand);
            maze.Repair_Connectivity(rand);
        }

        public override string ToString()
        {
            return this.GetType().Name.ToString();
        }
    }
}
