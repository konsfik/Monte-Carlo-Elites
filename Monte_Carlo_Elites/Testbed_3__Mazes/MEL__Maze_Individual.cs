using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MapElites_Lib;
using Perfect_Mazes_Lib;

namespace Testbed_3__Mazes
{
    public class MEL__Maze_Individual : MEL__Individual
    {
        public readonly PM_Maze maze;

        public MEL__Maze_Individual(PM_Maze maze)
        {
            this.maze = (PM_Maze)maze.DeepCopy();
        }
    }
}
