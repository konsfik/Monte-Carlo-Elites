using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Perfect_Mazes_Lib
{
    public class MazeGenerator_Assistant
    {
        public static PM_Maze RepairMaze_RandomBFS(
            Random rand,
            PM_Maze maze
            )
        {
            maze.Fill_RandomBFS(rand);

            maze.Repair_Connectivity(rand);

            return maze;
        }

        public static PM_Maze RepairMaze_RandomDFS(
            Random rand,
            PM_Maze maze
            )
        {
            maze.Fill_Random_DFS(rand);

            maze.Repair_Connectivity(rand);

            return maze;
        }
    }
}
