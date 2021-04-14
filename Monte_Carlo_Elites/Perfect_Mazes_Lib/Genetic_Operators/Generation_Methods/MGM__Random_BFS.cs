using System;
using System.Collections.Generic;
using System.Text;

using Common_Tools.Elements;
using Common_Tools.Utilities;

namespace Perfect_Mazes_Lib
{
    public class MGM__Random_BFS : PM__Maze_Generation_Method
    {
        public override PM_Maze Generate_Maze(
            Random rand,
            int width,
            int height
            )
        {
            PM_Maze maze = new PM_Maze(width, height);
            Vec2i current_position = new Vec2i(rand.Next(width), rand.Next(height));
            List<UEdge2i> possible_expansion_edges = maze.Cell_ExpansionEdges_List(current_position);

            while (possible_expansion_edges.Count > 0)
            {
                UEdge2i edge = possible_expansion_edges.Pop_Random_Item(rand);
                current_position = edge.exit;
                if (maze.Q_Is_Cell_Unconnected(current_position))
                {
                    maze.OP_AddEdge(edge);
                    List<UEdge2i> newEdges = maze.Cell_ExpansionEdges_List(current_position);
                    possible_expansion_edges.AddRange(newEdges);
                }
            }
            return maze;
        }

        public override string Description()
        {
            return this.GetType().Name.ToString();
        }

        public override string ToString()
        {
            return Description();
        }
    }
}
