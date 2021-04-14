using System;
using System.Collections.Generic;
using System.Text;

using Common_Tools.Elements;
using Common_Tools.Utilities;

namespace Perfect_Mazes_Lib
{
    public class MDM_RandomRect : PM__Maze_Destruction_Method
    {
        public override void DestroyMaze(
            Random rand,
            PM_Maze maze
            )
        {
            Rect2i boundingRect = maze.BoundingRect();
            Rect2i destructionRect = boundingRect.Random_ContainedRect(rand);
            maze.HOP_DeleteArea(destructionRect);
        }

        public override string ToString()
        {
            return this.GetType().Name.ToString();
        }
    }
}
