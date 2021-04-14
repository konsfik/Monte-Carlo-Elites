using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MapElites_Lib;
using Perfect_Mazes_Lib;

namespace Testbed_3__Mazes
{
    public class MEL_Mazes__Individual_Generator : MEL__Individual_Generator<MEL__Maze_Individual>
    {
        public readonly PM__Maze_Generation_Method maze_generation_method;
        public readonly int width;
        public readonly int height;

        public MEL_Mazes__Individual_Generator(
            PM__Maze_Generation_Method maze_generation_method,
            int width, 
            int height
            )
        {
            this.maze_generation_method = maze_generation_method;
            this.width = width;
            this.height = height;
        }

        public override MEL__Maze_Individual Generate_Individual(Random randomness_provider)
        {
            PM_Maze maze = maze_generation_method.Generate_Maze(
                randomness_provider,
                width,
                height
                );

            MEL__Maze_Individual maze_individual = new MEL__Maze_Individual(maze);

            return maze_individual;
        }

        public override string ToString()
        {
            return this.GetType().ToString();
        }
    }
}
