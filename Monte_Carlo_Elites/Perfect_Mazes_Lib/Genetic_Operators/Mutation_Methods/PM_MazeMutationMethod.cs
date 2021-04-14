using System;
using System.Collections.Generic;
using System.Text;

namespace Perfect_Mazes_Lib
{
    public class PM_MazeMutationMethod
    {
        public PM__Maze_Destruction_Method MazeDestructionMethod { get; private set; }
        public PM__Maze_Repair_Method MazeRepairMethod { get; private set; }

        public PM_MazeMutationMethod(
            PM__Maze_Destruction_Method mazeDestructionMethod,
            PM__Maze_Repair_Method mazeRepairMethod
            )
        {
            MazeDestructionMethod = mazeDestructionMethod;
            MazeRepairMethod = mazeRepairMethod;
        }

        public void MutateMaze(
            Random rand,
            PM_Maze maze,
            int currentNumIterations,
            int maxNumIterations
            )
        {
            MazeDestructionMethod.DestroyMaze(rand, maze);
            MazeRepairMethod.RefillMaze(rand, maze);
        }
    }
}
