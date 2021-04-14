using System;
using System.Collections.Generic;
using System.Text;

using Common_Tools.Elements;
using Common_Tools.Utilities;

namespace Perfect_Mazes_Lib
{
    public static class PM_Maze_Operators
    {
        #region High level OPerators
        public static void HOP_DeleteArea(this PM_Maze maze, Rect2i area)
        {
            if (maze.Q_Is_Area_InBounds(area) == false)
            {
                return;
            }
            for (int x = 0; x < maze.Q_Width(); x++)
            {
                for (int y = 0; y < maze.Q_Height(); y++)
                {
                    Vec2i cell = new Vec2i(x, y);
                    if (area.Contains(cell))
                    {
                        HashSet<UEdge2i> cellEdges = maze.Cell_ActiveEdges_Set(cell);
                        foreach (var edge in cellEdges)
                        {
                            maze.OP_RemoveEdge(edge);
                        }
                    }
                }
            }
        }

        public static void HOP_DeleteRandomArea(this PM_Maze maze, Random rand)
        {
            Rect2i randomArea = maze.BoundingRect().Random_ContainedRect(rand);
            maze.HOP_DeleteArea(randomArea);
        }

        public static void HOP_KeepArea(this PM_Maze maze, Rect2i area)
        {
            if (maze.Q_Is_Area_InBounds(area) == false)
            {
                return;
            }
            for (int x = 0; x < maze.Q_Width(); x++)
            {
                for (int y = 0; y < maze.Q_Height(); y++)
                {
                    Vec2i cell = new Vec2i(x, y);
                    if (area.Contains(cell) == false)
                    {
                        HashSet<UEdge2i> cellEdges = maze.Cell_ActiveEdges_Set(cell);
                        foreach (var edge in cellEdges)
                        {
                            maze.OP_RemoveEdge(edge);
                        }
                    }
                }
            }
        }

        public static void Fill_Random_DFS(
            this PM_Maze maze,
            Random rand
            )
        {
            if (maze == null)
            {
                return;
            }
            List<UEdge2i> allPossibleEdges = maze.ConnectedCells_ExpansionEdges_List();
            // temporarily commented out
            allPossibleEdges.Shuffle(rand);

            while (allPossibleEdges.Count > 0)
            {
                UEdge2i edge = allPossibleEdges.Pop_Last_Item();
                if (maze.Q_Is_Cell_Unconnected(edge.exit))
                {
                    maze.OP_AddEdge(edge);
                    List<UEdge2i> newEdges = maze.Cell_ExpansionEdges_List(edge.exit);
                    newEdges.Shuffle(rand);
                    allPossibleEdges.AddRange(newEdges);
                }
            }
        }

        public static void Fill_RandomBFS(
            this PM_Maze maze,
            Random rand
            )
        {
            if (maze == null)
            {
                return;
            }
            // edges 
            List<UEdge2i> edges = maze.ConnectedCells_ExpansionEdges_List();

            while (edges.Count > 0)
            {
                UEdge2i edge = edges.Pop_Random_Item(rand);
                if (maze.Q_Is_Cell_Unconnected(edge.exit))
                {
                    maze.OP_AddEdge(edge);
                    edges.AddRange(maze.Cell_ExpansionEdges_List(edge.exit));
                }
            }
        }

        public static void Repair_Connectivity(
            this PM_Maze maze,
            Random rand
            )
        {

            List<HashSet<Vec2i>> islands = maze.Islands(rand);

            while (islands.Count > 1)
            {
                HashSet<Vec2i> selectedIsland = islands.Pop_Random_Item(rand);

                HashSet<Vec2i> allOtherNodes = maze.CellsPositions_All_Set();
                allOtherNodes.ExceptWith(selectedIsland);

                HashSet<Vec2i> frontier = new HashSet<Vec2i>();
                foreach (var cell in selectedIsland)
                {
                    //HashSet<PM_V2> inactiveEdges = maze.

                    bool isFrontier = false;
                    List<Vec2i> geomNei = maze.Cell_GeometricNeighbors_List(cell);
                    foreach (var nei in geomNei)
                    {
                        if (selectedIsland.Contains(nei) == false)
                        {
                            isFrontier = true;
                            break;
                        }
                    }

                    if (isFrontier)
                    {
                        frontier.Add(cell);
                    }
                }

                Vec2i selectedNode = frontier.Random_Item(rand);
                HashSet<Vec2i> possibleJoins = maze.Cell_GeometricNeighbors_Set(selectedNode);
                possibleJoins.ExceptWith(selectedIsland);

                Vec2i joinedNode = possibleJoins.Random_Item(rand);

                maze.OP_AddEdge(new UEdge2i(selectedNode, joinedNode));

                int joinedIslandIndex = -1;
                for (int i = 0; i < islands.Count; i++)
                {
                    if (islands[i].Contains(joinedNode))
                    {
                        joinedIslandIndex = i;
                        break;
                    }
                }
                HashSet<Vec2i> joinedIsland = islands[joinedIslandIndex];

                islands.RemoveAt(joinedIslandIndex);

                joinedIsland.UnionWith(selectedIsland);

                islands.Add(joinedIsland);
            }
        }
        #endregion

        #region basic OPerators
        public static void OP_DeleteCell(this PM_Maze maze, Vec2i cell)
        {
            HashSet<UEdge2i> cellActiveEdges = maze.Cell_ActiveEdges_Set(cell);
            foreach (UEdge2i edge in cellActiveEdges)
            {
                maze.OP_RemoveEdge(edge);
            }
        }

        public static void OP_DeleteCell(this PM_Maze maze, int x, int y)
        {
            maze.OP_DeleteCell(new Vec2i(x, y));
        }

        public static void OP_AddEdge(this PM_Maze maze, UEdge2i edge)
        {
            if (maze.Q_Is_Edge_InBounds(edge))
            {
                Directions_Ortho_2D origin_direction = edge.To_Direction();
                maze.LOP_Add_Cell_Directions(edge.origin, origin_direction);

                Directions_Ortho_2D exit_direction = edge.Reverse().ToDirection();
                maze.LOP_Add_Cell_Directions(edge.exit, exit_direction);
            }
        }

        public static void OP_RemoveEdge(this PM_Maze maze, UEdge2i edge)
        {
            if (maze.Q_Is_Edge_InBounds(edge))
            {
                Directions_Ortho_2D origin_direction = edge.To_Direction();
                maze.LLOP_Remove_Cell_Directions(edge.origin, origin_direction);

                Directions_Ortho_2D exit_direction = edge.Reverse().ToDirection();
                maze.LLOP_Remove_Cell_Directions(edge.exit, exit_direction);
            }
        }
        #endregion

        #region Low Level OPerators
        public static void LOP_Set_Cell_Directions(
            this PM_Maze maze, 
            int x, 
            int y, 
            Directions_Ortho_2D dir
            )
        {
            maze.cells[x, y] = dir;
        }

        public static void LOP_Set_Cell_Directions(
            this PM_Maze maze, 
            Vec2i pos, 
            Directions_Ortho_2D dir
            )
        {
            maze.LOP_Set_Cell_Directions(pos.x, pos.y, dir);
        }

        public static void LLOP_Add_Cell_Directions(
            this PM_Maze maze, 
            int x, 
            int y, 
            Directions_Ortho_2D dir
            )
        {
            maze.cells[x, y] |= dir;
        }

        public static void LOP_Add_Cell_Directions(this PM_Maze maze, Vec2i pos, Directions_Ortho_2D dir)
        {
            maze.LLOP_Add_Cell_Directions(pos.x, pos.y, dir);
        }

        public static void LOP_Remove_Cell_Directions(this PM_Maze maze, int x, int y, Directions_Ortho_2D dir)
        {
            maze.cells[x, y] &= ~dir;
        }

        public static void LLOP_Remove_Cell_Directions(this PM_Maze maze, Vec2i pos, Directions_Ortho_2D dir)
        {
            maze.cells[pos.x, pos.y] &= ~dir;
        }
        #endregion
    }
}
