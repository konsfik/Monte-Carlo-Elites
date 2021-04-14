using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Common_Tools.Elements;
using Common_Tools.Utilities;

namespace Perfect_Mazes_Lib
{
    public static class PM_MazeExtensions_Queries
    {

        public static List<HashSet<Vec2i>> Islands(
            this PM_Maze maze,
            Random rand)
        {
            List<HashSet<Vec2i>> islands = new List<HashSet<Vec2i>>();

            HashSet<Vec2i> allCells = maze.CellsPositions_All_Set();

            while (allCells.Count > 0)
            {
                Vec2i randomCell = allCells.Random_Item(rand);
                HashSet<Vec2i> island = maze.BFS_ReachableCells_Set(randomCell, true);
                islands.Add(island);
                allCells.ExceptWith(island);
                //allCells.RemoveWhere(x => island.Contains(x));
            }

            return islands;
        }

        public static Vec2i Find_SymmetricCell_Over_X_Axis(
            this PM_Maze maze,
            Vec2i cell
            )
        {
            return new Vec2i(cell.x, maze.Q_Width() - 1 - cell.y);
        }

        public static Vec2i Find_SymmetricCell_Over_Y_Axis(
            this PM_Maze maze,
            Vec2i cell
            )
        {
            return new Vec2i(maze.Q_Width() - 1 - cell.x, cell.y);
        }

        public static int ShortestDistance(this PM_Maze maze, Vec2i root, Vec2i destination)
        {
            if (maze.BoundingRect().Contains(root) == false)
            {
                throw new SystemException("root not contained in maze");
            }
            if (maze.BoundingRect().Contains(root) == false)
            {
                throw new SystemException("destination not contained in maze");
            }

            if (root == destination)
            {
                return 0;
            }

            List<Vec2i> shortestPath = maze.BFS_ShortestPath(root, destination);

            return shortestPath.Count - 1;
        }

        /// <summary>
        /// Method that searches whether the destination cell is reachable from the root cell.
        /// It searches the graph using a BFS algorithm.
        /// </summary>
        /// <param name="maze"></param>
        /// <param name="root"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static bool Q_Is_Reachable_BFS(this PM_Maze maze, Vec2i root, Vec2i destination)
        {
            if (root.Equals(destination))
            {
                return true;
            }

            List<Vec2i> all_nodes = maze.CellsPositions_All_List();

            Dictionary<Vec2i, bool> visited_nodes = new Dictionary<Vec2i, bool>();
            foreach (var n in all_nodes)
            {
                visited_nodes.Add(n, false);
            }

            Queue<Vec2i> searchQueue = new Queue<Vec2i>();

            searchQueue.Enqueue(root);
            visited_nodes[root] = true;

            while (searchQueue.Count > 0)
            {
                var current = searchQueue.Dequeue();
                var neighbors = maze.Cells_Connected_To_Cell__List(current);
                List<Vec2i> unvisitedNeighbors = neighbors.FindAll(x => visited_nodes[x] == false);

                foreach (var neighbor in unvisitedNeighbors)
                {
                    searchQueue.Enqueue(neighbor);
                    visited_nodes[neighbor] = true;

                    if (neighbor.Equals(destination))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static List<Vec2i> BFS_ShortestPath(
            this PM_Maze maze,
            Vec2i root,
            Vec2i destination
            )
        {
            if (root.Equals(destination))
            {
                throw new System.ArgumentException("root equals destination");
            }

            Dictionary<Vec2i, Vec2i> predecessors = new Dictionary<Vec2i, Vec2i>();

            var allCells = maze.CellsPositions_All_List();

            foreach (var node in allCells)
            {
                predecessors.Add(node, node);
            }

            List<Vec2i> visitedNodes = new List<Vec2i>();

            Queue<Vec2i> searchQueue = new Queue<Vec2i>();

            searchQueue.Enqueue(root);
            visitedNodes.Add(root);
            bool foundDestination = false;

            while (searchQueue.Count > 0 && foundDestination == false)
            {
                var current = searchQueue.Dequeue();
                var neighbors = maze.Cells_Connected_To_Cell__List(current);
                List<Vec2i> unvisitedNeighbors = neighbors.FindAll(x => visitedNodes.Contains(x) == false);

                foreach (var neighbor in unvisitedNeighbors)
                {
                    predecessors[neighbor] = current;
                    searchQueue.Enqueue(neighbor);
                    visitedNodes.Add(neighbor);

                    if (neighbor.Equals(destination))
                    {
                        foundDestination = true;
                        break;
                    }
                }
            }

            List<Vec2i> shortestPath = new List<Vec2i>();

            bool pathFinished = false;
            var currentPathPosition = destination;
            shortestPath.Add(currentPathPosition);
            while (pathFinished == false)
            {
                var predecessor = predecessors[currentPathPosition];
                if (predecessor.Equals(currentPathPosition) == false)
                {
                    shortestPath.Add(predecessor);
                    currentPathPosition = predecessor;
                }
                else
                {
                    pathFinished = true;
                }
            }

            shortestPath.Reverse();

            if (
                shortestPath.Contains(root)
                &&
                shortestPath.Contains(destination)
                )
            {
                return shortestPath;
            }

            return new List<Vec2i>();
        }

        public static int Num_Steps_NecessaryToReachAllCells(
            this PM_Maze maze,
            Vec2i root
            )
        {
            var allNodes = maze.CellsPositions_All_List();

            Dictionary<Vec2i, Vec2i> predecessors = new Dictionary<Vec2i, Vec2i>();
            Dictionary<Vec2i, int> depths = new Dictionary<Vec2i, int>();

            depths.Add(root, 0);

            foreach (var node in allNodes)
            {
                predecessors.Add(node, node);
            }

            HashSet<Vec2i> visited_Nodes = new HashSet<Vec2i>() { root };

            Queue<Vec2i> searchQueue = new Queue<Vec2i>();
            searchQueue.Enqueue(root);

            while (searchQueue.Count > 0)
            {
                var current = searchQueue.Dequeue();

                var neighbors = maze.Cells_Connected_To_Cell__List(current);
                List<Vec2i> unvisitedNeighbors = neighbors.FindAll(x => visited_Nodes.Contains(x) == false);

                foreach (var neighbor in unvisitedNeighbors)
                {
                    predecessors[neighbor] = current;
                    searchQueue.Enqueue(neighbor);
                    visited_Nodes.Add(neighbor);
                    depths.Add(neighbor, depths[current] + 1);
                }
            }

            int maxDepth = 0;
            foreach (var node in allNodes)
            {
                if (depths[node] > maxDepth) maxDepth = depths[node];
            }

            return maxDepth;
        }

        public static HashSet<Vec2i> BFS_ReachableCells_Set(
            this PM_Maze maze,
            Vec2i root,
            bool includeRoot
            )
        {
            var allNodes = maze.CellsPositions_All_List();

            Dictionary<Vec2i, Vec2i> predecessors = new Dictionary<Vec2i, Vec2i>();

            foreach (var node in allNodes)
            {
                predecessors.Add(node, node);
            }

            HashSet<Vec2i> visited_Nodes = new HashSet<Vec2i>() { root };

            Queue<Vec2i> searchQueue = new Queue<Vec2i>();
            searchQueue.Enqueue(root);

            while (searchQueue.Count > 0)
            {
                var current = searchQueue.Dequeue();

                var neighbors = maze.Cells_Connected_To_Cell__List(current);
                List<Vec2i> unvisitedNeighbors = neighbors.FindAll(x => visited_Nodes.Contains(x) == false);

                foreach (var neighbor in unvisitedNeighbors)
                {
                    predecessors[neighbor] = current;
                    searchQueue.Enqueue(neighbor);
                    visited_Nodes.Add(neighbor);
                }
            }

            if (includeRoot)
            {
                return visited_Nodes;
            }
            else
            {
                visited_Nodes.Remove(root);
                return visited_Nodes;
            }

        }

        public static HashSet<Vec2i> BFS_ReachableNodes_MaximumSteps(
            this PM_Maze maze,
            Vec2i root,
            bool includeRoot,
            int maximumSteps
            )
        {
            // start the bfs search...

            Dictionary<Vec2i, Vec2i> predecessors = new Dictionary<Vec2i, Vec2i>();
            Dictionary<Vec2i, int> numSteps = new Dictionary<Vec2i, int>();

            var allNodes = maze.CellsPositions_All_List();

            //T dummyNode = T.Dummy();
            foreach (var node in allNodes)
            {
                predecessors.Add(node, node);
                numSteps.Add(node, int.MaxValue);
            }

            HashSet<Vec2i> visited_Nodes = new HashSet<Vec2i>() { root };

            Queue<Vec2i> searchQueue = new Queue<Vec2i>();
            searchQueue.Enqueue(root);
            numSteps[root] = 0;

            while (searchQueue.Count > 0)
            {
                var current = searchQueue.Dequeue();
                if (numSteps[current] < maximumSteps)
                {
                    HashSet<Vec2i> unvisitedNeighbors = maze.Cells_Connected_To_Cell__Set(current);
                    unvisitedNeighbors.ExceptWith(visited_Nodes);

                    foreach (var neighbor in unvisitedNeighbors)
                    {
                        predecessors[neighbor] = current;
                        numSteps[neighbor] = numSteps[current] + 1;
                        searchQueue.Enqueue(neighbor);
                        visited_Nodes.Add(neighbor);
                    }
                }
            }

            HashSet<Vec2i> reachable_within_steps = new HashSet<Vec2i>();

            foreach (var node in visited_Nodes)
            {
                if (numSteps[node] <= maximumSteps)
                {
                    reachable_within_steps.Add(node);
                }
            }

            if (includeRoot)
            {
                return reachable_within_steps;
            }
            else
            {
                reachable_within_steps.Remove(root);
                return reachable_within_steps;
            }

        }

        public static bool IsCyclic(
            this PM_Maze maze
            )
        {
            List<Vec2i> nodes = maze.CellsPositions_All_List();

            // Mark all the vertices as not visited  
            // and not part of recursion stack 
            HashSet<Vec2i> visitedNodes = new HashSet<Vec2i>();

            // Call the recursive helper function  
            // to detect cycle in different DFS trees 
            foreach (var node in nodes)
            {
                // Don't recur for u if already visited 
                if (visitedNodes.Contains(node) == false)
                {
                    if (IsCyclic_Helper(maze, node, visitedNodes, new Vec2i(-1, -1)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        // A recursive function that uses visited[]  
        // and parent to detect cycle in subgraph 
        // reachable from vertex v. 
        private static bool IsCyclic_Helper(
            PM_Maze maze,
            Vec2i node,
            HashSet<Vec2i> visitedNodes,
            Vec2i parent
            )
        {
            // Mark the current node as visited 
            visitedNodes.Add(node);

            var nodeNeighbors = maze.Cells_Connected_To_Cell__List(node);

            // Recur for all the vertices  
            // adjacent to this vertex 
            foreach (var neighbor in nodeNeighbors)
            {
                // If an adjacent is not visited,  
                // then recur for that adjacent 
                if (visitedNodes.Contains(neighbor) == false)
                {
                    if (IsCyclic_Helper(maze, neighbor, visitedNodes, node))
                        return true;
                }

                // If an adjacent is visited and  
                // not parent of current vertex, 
                // then there is a cycle. 
                else if (neighbor != parent)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// A maze is fully connected, when every cell can be visited from any cell.
        /// In order to test for that, we do a bfs from cell (0,0), and see if the visited cells are all cells.
        /// </summary>
        /// <param name="maze"></param>
        /// <returns></returns>
        public static bool IsFullyConnected(
            this PM_Maze maze
            )
        {
            return maze.Num_ReachableCells(new Vec2i(0, 0)) == maze.NumCells__All();
        }

        /// <summary>
        /// A maze is fully expanded, when all cells are connected to at least one other cell.
        /// Otherwise put, there is no remaining cell that has no active connections.
        /// I.e. there are no more edges left that would connect to an unconnected cell.
        /// A fully expanded maze can be not fully connected.
        /// </summary>
        /// <param name="maze"></param>
        /// <returns></returns>
        public static bool IsFullyExpanded(
            this PM_Maze maze
            )
        {
            return maze.NumCells__OfSpecificDirections(Directions_Ortho_2D.None) == 0;
        }

        public static bool IsPerfectMaze(
            this PM_Maze maze
            )
        {
            return maze.IsFullyConnected() && !maze.IsCyclic();
        }

        public static int Num_ReachableCells(
            this PM_Maze maze,
            Vec2i cell
            )
        {
            return maze.ReachableCells_List(cell, null).Count;
        }

        public static Rect2i BoundingRect(
            this PM_Maze maze
            )
        {
            return new Rect2i(
                new Vec2i(0, 0),
                new Vec2i(maze.Q_Width() - 1, maze.Q_Height() - 1)
                );
        }

        //public static HashSet<PM_Vec2> ReachableCells_Set(
        //    this PM_Maze maze,
        //    PM_Vec2 cell,
        //    HashSet<PM_Vec2> visitedCells
        //    )
        //{
        //    if (visitedCells == null || visitedCells.Count == 0)
        //    {
        //        visitedCells = new HashSet<PM_Vec2>() { cell };
        //    }

        //    HashSet<PM_Vec2> neighbors = maze.Cell_ActiveNeighbors_Set(cell);
        //    foreach (var neighbor in neighbors)
        //    {
        //        if (visitedCells.Contains(neighbor) == false)
        //        {
        //            visitedCells.Add(neighbor);
        //            maze.ReachableCells_Set(neighbor, visitedCells);
        //        }
        //    }

        //    return visitedCells;
        //}

        public static List<Vec2i> ReachableCells_List(
            this PM_Maze maze,
            Vec2i cell,
            List<Vec2i> visitedCells
            )
        {
            if (visitedCells == null || visitedCells.Count == 0)
            {
                visitedCells = new List<Vec2i>() { cell };
            }

            List<Vec2i> neighbors = maze.Cells_Connected_To_Cell__List(cell);
            foreach (var neighbor in neighbors)
            {
                if (visitedCells.Contains(neighbor) == false)
                {
                    visitedCells.Add(neighbor);
                    maze.ReachableCells_List(neighbor, visitedCells);
                }
            }

            return visitedCells;
        }

        public static Vec2i RandomCell(
            this PM_Maze maze,
            Random rand
            )
        {
            return new Vec2i(
                rand.Next(maze.Q_Width()),
                rand.Next(maze.Q_Height())
                );
        }

        public static HashSet<UEdge2i> All_ExpansionEdges_Set(
            this PM_Maze maze
            )
        {
            int width = maze.Q_Width();
            int height = maze.Q_Height();

            HashSet<UEdge2i> all_ExpansionEdges = new HashSet<UEdge2i>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    all_ExpansionEdges.UnionWith(maze.Cell_ExpansionEdges_Set(x, y));
                }
            }
            return all_ExpansionEdges;
        }

        public static List<UEdge2i> All_ExpansionEdges_List(
            this PM_Maze maze
            )
        {
            int width = maze.Q_Width();
            int height = maze.Q_Height();

            List<UEdge2i> all_ExpansionEdges = new List<UEdge2i>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    all_ExpansionEdges.AddRange(maze.Cell_ExpansionEdges_Set(x, y));
                }
            }
            return all_ExpansionEdges;
        }

        public static HashSet<Vec2i> ConnectedCells_Set(
            this PM_Maze maze
            )
        {
            int width = maze.Q_Width();
            int height = maze.Q_Height();

            HashSet<Vec2i> connectedCells = new HashSet<Vec2i>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vec2i cell = new Vec2i(x, y);
                    if (maze.Q_Is_Cell_Connected(cell))
                    {
                        connectedCells.Add(cell);
                    }
                }
            }
            return connectedCells;
        }

        public static List<Vec2i> ConnectedCells_List(
            this PM_Maze maze
            )
        {
            int width = maze.Q_Width();
            int height = maze.Q_Height();

            List<Vec2i> connectedCells = new List<Vec2i>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vec2i cell = new Vec2i(x, y);
                    if (maze.Q_Is_Cell_Connected(cell))
                    {
                        connectedCells.Add(cell);
                    }
                }
            }
            return connectedCells;
        }

        public static HashSet<UEdge2i> ConnectedCells_ExpansionEdges_Set(
            this PM_Maze maze
            )
        {
            HashSet<UEdge2i> expansionEdges = new HashSet<UEdge2i>();
            HashSet<Vec2i> connectedCells = maze.ConnectedCells_Set();
            foreach (var cell in connectedCells)
            {
                if (maze.Q_Is_Cell_Connected(cell))
                {
                    expansionEdges.UnionWith(maze.Cell_ExpansionEdges_Set(cell));
                }
            }
            return expansionEdges;
        }

        public static List<UEdge2i> ConnectedCells_ExpansionEdges_List(
            this PM_Maze maze
            )
        {
            List<UEdge2i> expansionEdges = new List<UEdge2i>();
            List<Vec2i> connectedCells = maze.ConnectedCells_List();
            foreach (var cell in connectedCells)
            {
                if (maze.Q_Is_Cell_Connected(cell))
                {
                    expansionEdges.AddRange(maze.Cell_ExpansionEdges_List(cell));
                }
            }
            return expansionEdges;
        }

        public static HashSet<Vec2i> Cells_Connected_To_Cell__Set(
            this PM_Maze maze,
            Vec2i cell
            )
        {
            HashSet<Vec2i> neighbors = new HashSet<Vec2i>();

            Directions_Ortho_2D directions = maze.Q_Cell_Directions(cell);
            if (directions.HasFlag(Directions_Ortho_2D.U))
            {
                neighbors.Add(cell.To_Up());
            }
            if (directions.HasFlag(Directions_Ortho_2D.D))
            {
                neighbors.Add(cell.To_Down());
            }
            if (directions.HasFlag(Directions_Ortho_2D.L))
            {
                neighbors.Add(cell.To_Left());
            }
            if (directions.HasFlag(Directions_Ortho_2D.R))
            {
                neighbors.Add(cell.To_Right());
            }

            return neighbors;
        }

        public static List<Vec2i> Cells_Connected_To_Cell__List(
            this PM_Maze maze,
            Vec2i cell
            )
        {
            List<Vec2i> neighbors = new List<Vec2i>();

            Directions_Ortho_2D directions = maze.Q_Cell_Directions(cell);
            if (directions.HasFlag(Directions_Ortho_2D.U))
            {
                neighbors.Add(cell.To_Up());
            }
            if (directions.HasFlag(Directions_Ortho_2D.D))
            {
                neighbors.Add(cell.To_Down());
            }
            if (directions.HasFlag(Directions_Ortho_2D.L))
            {
                neighbors.Add(cell.To_Left());
            }
            if (directions.HasFlag(Directions_Ortho_2D.R))
            {
                neighbors.Add(cell.To_Right());
            }

            return neighbors;
        }

        public static HashSet<UEdge2i> Cell_InactiveEdges_InBounds_Set(
            this PM_Maze maze,
            Vec2i pos
            )
        {
            HashSet<UEdge2i> inactiveEdges_InBounds = new HashSet<UEdge2i>();
            if (maze.Q_Cell_Directions(pos).HasFlag(Directions_Ortho_2D.U) == false)
            {
                UEdge2i upEdge = new UEdge2i(pos, pos.To_Up());
                if (maze.Q_Is_Edge_InBounds(upEdge))
                {
                    inactiveEdges_InBounds.Add(upEdge);
                }
            }
            if (maze.Q_Cell_Directions(pos).HasFlag(Directions_Ortho_2D.D) == false)
            {
                UEdge2i downEdge = new UEdge2i(pos, pos.To_Down());
                if (maze.Q_Is_Edge_InBounds(downEdge))
                {
                    inactiveEdges_InBounds.Add(downEdge);
                }
            }
            if (maze.Q_Cell_Directions(pos).HasFlag(Directions_Ortho_2D.L) == false)
            {
                UEdge2i leftEdge = new UEdge2i(pos, pos.To_Left());
                if (maze.Q_Is_Edge_InBounds(leftEdge))
                {
                    inactiveEdges_InBounds.Add(leftEdge);
                }
            }
            if (maze.Q_Cell_Directions(pos).HasFlag(Directions_Ortho_2D.R) == false)
            {
                UEdge2i rightEdge = new UEdge2i(pos, pos.To_Right());
                if (maze.Q_Is_Edge_InBounds(rightEdge))
                {
                    inactiveEdges_InBounds.Add(rightEdge);
                }
            }
            return inactiveEdges_InBounds;
        }

        public static List<UEdge2i> Cell_InactiveEdges_InBounds_List(
            this PM_Maze maze,
            Vec2i pos
            )
        {
            List<UEdge2i> inactiveEdges_InBounds = new List<UEdge2i>();
            if (maze.Q_Cell_Directions(pos).HasFlag(Directions_Ortho_2D.U) == false)
            {
                UEdge2i upEdge = new UEdge2i(pos, pos.To_Up());
                if (maze.Q_Is_Edge_InBounds(upEdge))
                {
                    inactiveEdges_InBounds.Add(upEdge);
                }
            }
            if (maze.Q_Cell_Directions(pos).HasFlag(Directions_Ortho_2D.D) == false)
            {
                UEdge2i downEdge = new UEdge2i(pos, pos.To_Down());
                if (maze.Q_Is_Edge_InBounds(downEdge))
                {
                    inactiveEdges_InBounds.Add(downEdge);
                }
            }
            if (maze.Q_Cell_Directions(pos).HasFlag(Directions_Ortho_2D.L) == false)
            {
                UEdge2i leftEdge = new UEdge2i(pos, pos.To_Left());
                if (maze.Q_Is_Edge_InBounds(leftEdge))
                {
                    inactiveEdges_InBounds.Add(leftEdge);
                }
            }
            if (maze.Q_Cell_Directions(pos).HasFlag(Directions_Ortho_2D.R) == false)
            {
                UEdge2i rightEdge = new UEdge2i(pos, pos.To_Right());
                if (maze.Q_Is_Edge_InBounds(rightEdge))
                {
                    inactiveEdges_InBounds.Add(rightEdge);
                }
            }
            return inactiveEdges_InBounds;
        }

        public static HashSet<UEdge2i> Cell_InactiveEdges_InBounds_Set(
            this PM_Maze maze,
            int x,
            int y
            )
        {
            return maze.Cell_InactiveEdges_InBounds_Set(new Vec2i(x, y));
        }

        public static List<UEdge2i> Cell_InactiveEdges_InBounds_List(
            this PM_Maze maze,
            int x,
            int y
            )
        {
            return maze.Cell_InactiveEdges_InBounds_List(new Vec2i(x, y));
        }

        public static HashSet<UEdge2i> Cell_ExpansionEdges_Set(
            this PM_Maze maze,
            Vec2i origin
            )
        {
            var possibleConnections = new HashSet<UEdge2i>();

            if (maze.Q_Cell_Directions(origin).HasFlag(Directions_Ortho_2D.L) == false)
            {
                Vec2i leftPosition = origin.To_Left();
                if (maze.Q_Is_Cell_InBounds(leftPosition) && maze.Q_Is_Cell_Unconnected(leftPosition))
                {
                    possibleConnections.Add(new UEdge2i(origin, leftPosition));
                }
            }
            if (maze.Q_Cell_Directions(origin).HasFlag(Directions_Ortho_2D.R) == false)
            {
                Vec2i rightPosition = origin.To_Right();
                if (maze.Q_Is_Cell_InBounds(rightPosition) && maze.Q_Is_Cell_Unconnected(rightPosition))
                {
                    possibleConnections.Add(new UEdge2i(origin, rightPosition));
                }
            }
            if (maze.Q_Cell_Directions(origin).HasFlag(Directions_Ortho_2D.D) == false)
            {
                Vec2i downPosition = origin.To_Down();
                if (maze.Q_Is_Cell_InBounds(downPosition) && maze.Q_Is_Cell_Unconnected(downPosition))
                {
                    possibleConnections.Add(new UEdge2i(origin, downPosition));
                }
            }
            if (maze.Q_Cell_Directions(origin).HasFlag(Directions_Ortho_2D.U) == false)
            {
                Vec2i upPosition = origin.To_Up();
                if (maze.Q_Is_Cell_InBounds(upPosition) && maze.Q_Is_Cell_Unconnected(upPosition))
                {
                    possibleConnections.Add(new UEdge2i(origin, upPosition));
                }
            }
            return possibleConnections;
        }

        public static HashSet<UEdge2i> Cell_ExpansionEdges_Set(
            this PM_Maze maze,
            int x,
            int y
            )
        {
            return maze.Cell_ExpansionEdges_Set(new Vec2i(x, y));
        }

        public static List<UEdge2i> Cell_ExpansionEdges_List(
            this PM_Maze maze,
            Vec2i origin
            )
        {
            var possibleConnections = new List<UEdge2i>();

            if (maze.Q_Cell_Directions(origin).HasFlag(Directions_Ortho_2D.L) == false)
            {
                Vec2i leftPosition = origin.To_Left();
                if (maze.Q_Is_Cell_InBounds(leftPosition) && maze.Q_Is_Cell_Unconnected(leftPosition))
                {
                    possibleConnections.Add(new UEdge2i(origin, leftPosition));
                }
            }
            if (maze.Q_Cell_Directions(origin).HasFlag(Directions_Ortho_2D.R) == false)
            {
                Vec2i rightPosition = origin.To_Right();
                if (maze.Q_Is_Cell_InBounds(rightPosition) && maze.Q_Is_Cell_Unconnected(rightPosition))
                {
                    possibleConnections.Add(new UEdge2i(origin, rightPosition));
                }
            }
            if (maze.Q_Cell_Directions(origin).HasFlag(Directions_Ortho_2D.D) == false)
            {
                Vec2i downPosition = origin.To_Down();
                if (maze.Q_Is_Cell_InBounds(downPosition) && maze.Q_Is_Cell_Unconnected(downPosition))
                {
                    possibleConnections.Add(new UEdge2i(origin, downPosition));
                }
            }
            if (maze.Q_Cell_Directions(origin).HasFlag(Directions_Ortho_2D.U) == false)
            {
                Vec2i upPosition = origin.To_Up();
                if (maze.Q_Is_Cell_InBounds(upPosition) && maze.Q_Is_Cell_Unconnected(upPosition))
                {
                    possibleConnections.Add(new UEdge2i(origin, upPosition));
                }
            }
            return possibleConnections;
        }

        public static List<UEdge2i> Cell_ExpansionEdges_List(
            this PM_Maze maze,
            int x,
            int y
            )
        {
            return maze.Cell_ExpansionEdges_List(new Vec2i(x, y));
        }

        public static HashSet<Vec2i> Cell_GeometricNeighbors_Set(
            this PM_Maze maze,
            Vec2i pos
            )
        {
            HashSet<Vec2i> geometricNeighbors = new HashSet<Vec2i>();

            if (maze.Q_Is_Cell_InBounds(pos.To_Up()))
            {
                geometricNeighbors.Add(pos.To_Up());
            }
            if (maze.Q_Is_Cell_InBounds(pos.To_Down()))
            {
                geometricNeighbors.Add(pos.To_Down());
            }
            if (maze.Q_Is_Cell_InBounds(pos.To_Left()))
            {
                geometricNeighbors.Add(pos.To_Left());
            }
            if (maze.Q_Is_Cell_InBounds(pos.To_Right()))
            {
                geometricNeighbors.Add(pos.To_Right());
            }

            return geometricNeighbors;
        }

        public static List<Vec2i> Cell_GeometricNeighbors_List(
            this PM_Maze maze,
            Vec2i pos
            )
        {
            List<Vec2i> geometricNeighbors = new List<Vec2i>();

            if (maze.Q_Is_Cell_InBounds(pos.To_Up()))
            {
                geometricNeighbors.Add(pos.To_Up());
            }
            if (maze.Q_Is_Cell_InBounds(pos.To_Down()))
            {
                geometricNeighbors.Add(pos.To_Down());
            }
            if (maze.Q_Is_Cell_InBounds(pos.To_Left()))
            {
                geometricNeighbors.Add(pos.To_Left());
            }
            if (maze.Q_Is_Cell_InBounds(pos.To_Right()))
            {
                geometricNeighbors.Add(pos.To_Right());
            }

            return geometricNeighbors;
        }

        public static HashSet<UEdge2i> Cell_ActiveEdges_Set(
            this PM_Maze maze,
            Vec2i pos
            )
        {
            HashSet<UEdge2i> activeEdges = new HashSet<UEdge2i>();
            if (maze.Q_Cell_Directions(pos).HasFlag(Directions_Ortho_2D.U))
            {
                activeEdges.Add(
                    new UEdge2i(pos, pos.To_Up())
                    );
            }
            if (maze.Q_Cell_Directions(pos).HasFlag(Directions_Ortho_2D.D))
            {
                activeEdges.Add(
                    new UEdge2i(pos, pos.To_Down())
                    );
            }
            if (maze.Q_Cell_Directions(pos).HasFlag(Directions_Ortho_2D.L))
            {
                activeEdges.Add(
                    new UEdge2i(pos, pos.To_Left())
                    );
            }
            if (maze.Q_Cell_Directions(pos).HasFlag(Directions_Ortho_2D.R))
            {
                activeEdges.Add(
                    new UEdge2i(pos, pos.To_Right())
                    );
            }
            return activeEdges;
        }

        public static List<UEdge2i> Cell_ActiveEdges_List(
            this PM_Maze maze,
            Vec2i pos
            )
        {
            List<UEdge2i> activeEdges = new List<UEdge2i>();
            if (maze.Q_Cell_Directions(pos).HasFlag(Directions_Ortho_2D.U))
            {
                activeEdges.Add(
                    new UEdge2i(pos, pos.To_Up())
                    );
            }
            if (maze.Q_Cell_Directions(pos).HasFlag(Directions_Ortho_2D.D))
            {
                activeEdges.Add(
                    new UEdge2i(pos, pos.To_Down())
                    );
            }
            if (maze.Q_Cell_Directions(pos).HasFlag(Directions_Ortho_2D.L))
            {
                activeEdges.Add(
                    new UEdge2i(pos, pos.To_Left())
                    );
            }
            if (maze.Q_Cell_Directions(pos).HasFlag(Directions_Ortho_2D.R))
            {
                activeEdges.Add(
                    new UEdge2i(pos, pos.To_Right())
                    );
            }
            return activeEdges;
        }

        public static HashSet<UEdge2i> Cell_ActiveEdges_Set(
            this PM_Maze maze,
            int x,
            int y)
        {
            return maze.Cell_ActiveEdges_Set(new Vec2i(x, y));
        }

        public static List<UEdge2i> Cell_ActiveEdges_List(
            this PM_Maze maze,
            int x,
            int y)
        {
            return maze.Cell_ActiveEdges_List(new Vec2i(x, y));
        }

        public static List<Vec2i> CellsPositions_OfCentrality_List(this PM_Maze maze, int requestedCentrality)
        {
            int width = maze.Q_Width();
            int height = maze.Q_Height();

            List<Vec2i> cells_ofCentrality_list = new List<Vec2i>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (maze.Cell__Centrality(x, y) == requestedCentrality)
                    {
                        cells_ofCentrality_list.Add(new Vec2i(x, y));
                    }
                }
            }
            return cells_ofCentrality_list;
        }

        public static HashSet<Vec2i> CellsPositions_OfCentrality_Set(this PM_Maze maze, int requestedCentrality)
        {
            int width = maze.Q_Width();
            int height = maze.Q_Height();

            HashSet<Vec2i> cells_ofCentrality_set = new HashSet<Vec2i>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (maze.Cell__Centrality(x, y) == requestedCentrality)
                    {
                        cells_ofCentrality_set.Add(new Vec2i(x, y));
                    }
                }
            }
            return cells_ofCentrality_set;
        }

        public static HashSet<Vec2i> CellsPositions_All_Set(
            this PM_Maze maze
            )
        {
            int width = maze.Q_Width();
            int height = maze.Q_Height();

            HashSet<Vec2i> allCells = new HashSet<Vec2i>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    allCells.Add(new Vec2i(x, y));
                }
            }
            return allCells;
        }

        public static List<Vec2i> CellsPositions_All_List(
            this PM_Maze maze
            )
        {
            int width = maze.Q_Width();
            int height = maze.Q_Height();

            List<Vec2i> allCells = new List<Vec2i>(width*height);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int index = y * width + x;
                    //allCells[index] = new PM_Vec2(x, y);
                    allCells.Add(new Vec2i(x, y));
                }
            }
            return allCells;
        }

        public static HashSet<UEdge2i> All_ActiveEdges_Set(
            this PM_Maze maze
            )
        {
            int width = maze.Q_Width();
            int height = maze.Q_Height();

            HashSet<UEdge2i> activeEdges = new HashSet<UEdge2i>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    activeEdges.UnionWith(maze.Cell_ActiveEdges_Set(x, y));
                }
            }
            return activeEdges;
        }

        public static List<UEdge2i> All_ActiveEdges_List(
            this PM_Maze maze
            )
        {
            int width = maze.Q_Width();
            int height = maze.Q_Height();

            List<UEdge2i> activeEdges = new List<UEdge2i>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var newEdges = maze.Cell_ActiveEdges_Set(x, y);
                    foreach (var newEdge in newEdges)
                    {
                        if (activeEdges.Contains(newEdge) == false)
                        {
                            activeEdges.Add(newEdge);
                        }
                    }
                }
            }
            return activeEdges;
        }

        public static List<UEdge2i> All_Possible_OneWay_Edges_List(
            this PM_Maze maze
            )
        {
            int width = maze.Q_Width();
            int height = maze.Q_Height();

            List<UEdge2i> edges = new List<UEdge2i>();

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vec2i pos = new Vec2i(x, y);
                    Vec2i right = pos.To_Right();
                    Vec2i up = pos.To_Up();

                    if(x < width - 1) edges.Add(new UEdge2i(pos, right));
                    if(y < height - 1) edges.Add(new UEdge2i(pos, up));
                }
            }

            return edges;
        }

        public static HashSet<UEdge2i> All_InactiveEdges_InBounds_Set(
            this PM_Maze maze
            )
        {
            int width = maze.Q_Width();
            int height = maze.Q_Height();

            HashSet<UEdge2i> inactiveEdges = new HashSet<UEdge2i>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    inactiveEdges.UnionWith(maze.Cell_InactiveEdges_InBounds_Set(x, y));
                }
            }
            return inactiveEdges;
        }

        public static List<UEdge2i> All_InactiveEdges_InBounds_List(
            this PM_Maze maze
            )
        {
            int width = maze.Q_Width();
            int height = maze.Q_Height();

            List<UEdge2i> inactiveEdges = new List<UEdge2i>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var newEdges = maze.Cell_InactiveEdges_InBounds_List(x, y);
                    foreach (var newEdge in newEdges)
                    {
                        if (inactiveEdges.Contains(newEdge) == false)
                        {
                            inactiveEdges.Add(newEdge);
                        }
                    }
                }
            }
            return inactiveEdges;
        }


    }


}

