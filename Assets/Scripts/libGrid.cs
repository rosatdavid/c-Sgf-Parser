using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DataStructures;
using Grid = LibFever.Grid; 

namespace LibFever
{

    public class Node
    {
        public List<Node> neighbours;
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Cost { get; private set; }

        public Node(int x, int y,int c=1)
        {
            X = x;
            Y = y;
            neighbours = new List<Node>();
            Cost = c;
    }
        
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            var item = obj as Node;

            if (item == null)
            {
                return false;
            }

            return this.X.Equals(item.X) && this.Y.Equals(item.Y);
        }

        public override int GetHashCode()
        {
            int hash = 13;
            hash = (hash * 7) + X.GetHashCode();
            hash = (hash * 7) + Y.GetHashCode();
            return hash;
        }
        public static bool operator !=(Node n1, Node n2)
        {
            return !n1.Equals(n2);
        }

        public static bool operator ==(Node n1, Node n2)
        {
            return n1.Equals(n2);
        }

    


        public static Node[,] GenerateGraph(Grid grid, Dictionary<Point, int> obstacles)
        {
            int graphsize_x = (int)grid.nb_tiles.x;
            int graphsize_y = (int)grid.nb_tiles.y;

            Node[,] graph = new Node[graphsize_x, graphsize_y];
            for (int x = 0; x < graphsize_x; x++)
            {
                for (int y = 0; y < graphsize_y; y++)
                {
                    int value = 1;
                    if (obstacles.TryGetValue(new Point((int)x, (int)y), out value))
                    { 
                        graph[x, y] = new Node(x, y, value);
                    }
                    else
                    { 
                         graph[x, y] = new Node(x, y);
                    }
                }
            }
            for (int x = 0; x < graphsize_x; x++)
            {
                for (int y = 0; y < graphsize_y; y++)
                {
                    if (x > 0)
                        graph[x, y].neighbours.Add(graph[x - 1, y]);
                    if (x < graphsize_x - 1)
                        graph[x, y].neighbours.Add(graph[x + 1, y]);
                    if (y > 0)
                        graph[x, y].neighbours.Add(graph[x, y - 1]);
                    if (y < graphsize_y - 1)
                        graph[x, y].neighbours.Add(graph[x, y + 1]);
                }
            }
            return graph;
        }

    }
    public class PathFinding
    {
        public static int Heuristic(Node goal, Node next)
        {
            //Manhattan distance on a square grid
            return Math.Abs(goal.X - next.X) + Math.Abs(goal.Y - next.Y);

        }
        public static List<Node> Astar(Node[,] graph, int x, int y,int goal_x,int goal_y)
        {
            Node start = graph[x,y];
            Node goal = graph[goal_x, goal_y];

            List<Node> return_path = new List<Node>();
            DataStructures.PriorityQueue<Node> frontiere = new DataStructures.PriorityQueue<Node>();
            frontiere.Enqueue(start, 0);
            //List<Node> came_from = new List<Node>();
            Dictionary<Node, Node> came_from = new Dictionary<Node, Node>();
            Dictionary<Node, int> cost_so_far = new Dictionary<Node, int>();

            came_from[start] = null;
            cost_so_far[start] = 0;

            while (frontiere.Count() > 0)
            {
                Node current = frontiere.Dequeue();
                if (current.Equals(goal))
                {
                    //reconstruct Path
                   
                    Debug.Log("goal found:" + cost_so_far[current]);
                    Node p = current;
                    while (p != start)
                    {

                        return_path.Add(p);
                        p = came_from[p];
                    }
                    return return_path;
                }
                foreach(Node child in current.neighbours)
                {
                    int new_cost = cost_so_far[current] + child.Cost;
                    int value = -1;
                    if (!cost_so_far.TryGetValue(child, out value) || (value > -1 && value< cost_so_far[child]))
                    {
                        cost_so_far[child] = new_cost;
                        int priority = new_cost + Heuristic(goal, child);
                        frontiere.Enqueue(child, priority);
                        came_from[child] = current;
                    }
                }
            }
            return null;
        }
    }
    public class Misc
    {

        

    }

    
    public class Plane
    {
        public Vector2 min { get; private set; }
        public Vector2 max { get; private set; }
        public Vector2 lenght { get; private set; }
        public Vector2 center { get; private set; }

        public Plane(Vector2 vmin, Vector2 vmax)
        {
            Init(vmin, vmax);

        }
        public Plane(Collider cmin)
        {
            Init(new Vector2(cmin.bounds.min.x, cmin.bounds.min.z), new Vector2(cmin.bounds.max.x, cmin.bounds.max.z));
        }
        public Plane(SpriteRenderer sr)
        {
            Init(new Vector2(sr.bounds.min.x, sr.bounds.min.y), new Vector2(sr.bounds.max.x, sr.bounds.max.y));
        }
        public Plane(float min_x, float min_y, float max_x, float max_y)
        {
            Init(new Vector2(min_x, min_y), new Vector2(max_x, max_y));
        }


        private void Init(Vector2 vmin, Vector2 vmax)
        {
            min = vmin;
            max = vmax;
            lenght = new Vector2(Mathf.Abs(max.x - min.x), Mathf.Abs(max.y - min.y));
            center = new Vector2 (min.x +(lenght.x / 2.0f), min.y + (lenght.y / 2.0f));
        }

        public static bool IsPosInPlan(Plane plan, Vector2 pos)
        {
            if (pos.x >= plan.min.x && pos.y >= plan.min.y && pos.x <= plan.max.x && pos.y <= plan.max.y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static Vector2 posPlanInOther(Plane plan, Vector2 pos, Plane other)
        {
            float largeur = Mathf.Abs(plan.min.x - plan.max.x);
            float hauteur = Mathf.Abs(plan.min.y - plan.max.y);

            float distanceMinToX = Mathf.Abs(pos.x -plan.min.x);
            float distanceMinToY = Mathf.Abs(pos.y -plan.min.y);
            float posRatioX = distanceMinToX /largeur;
            float posRatioY = distanceMinToY /hauteur;


            float otherW = Mathf.Abs(other.min.x - other.max.x);
            float otherH = Mathf.Abs(other.min.y - other.max.y);

            float posInWidthOther = otherW * posRatioX;
            float posInHeightOther = otherH * posRatioY;

            float posInOtherX = posInWidthOther + other.min.x;
            float posInOtherY = posInHeightOther + other.min.y;

            return new Vector2(posInOtherX, posInOtherY);
        }




        }
    public class Grid
    {
        public Plane plane { get; private set; }
        public Vector2 nb_tiles { get; private set; }
        public Vector2 tile_size { get; private set; }
        

        public Grid(Plane iplane, float inb_tiles_largeur, float inb_tiles_hauteur)
        {
            plane = iplane;
            nb_tiles= new Vector2(inb_tiles_largeur, inb_tiles_largeur);
            tile_size = new Vector2(plane.lenght.x / nb_tiles.x, plane.lenght.y / nb_tiles.y);
            
        }
        public static Vector3 GetPosInWorld(Grid grid,Point index,float z = 0f)
        {
            Vector2 pos2D = GetCaseInWorld(grid, index);
           Vector3 pos3D = new Vector3(pos2D.x, pos2D.y,z);
            return pos2D;
        }

        public static Vector2 GetCaseInWorld(Grid grid, Point p_index)
        {
            Vector2 index = new Vector2(p_index.x, p_index.y);
            if (index.x < grid.nb_tiles.x && index.y < grid.nb_tiles.y)
            {
                
                Vector2 min = grid.plane.min;
                Vector2 max = grid.plane.max;

                Vector2 pos_cell_center = new Vector2((grid.plane.min.x+(index.x * grid.tile_size.x))+ grid.tile_size.x/2.0f,
                                                      (grid.plane.min.y+(index.y * grid.tile_size.y)) + grid.tile_size.y / 2.0f);
                

                return pos_cell_center;
            }
            else
            {
                throw new Exception("Index Greather or Egal than nb_tiles"+ p_index+" "+ grid.nb_tiles);
            }


        }


        public static DataStructures.Point getCaseIndex(Grid grid, Vector3 targ)
        {
            return getCaseIndex(grid, new Vector2(targ.x, targ.y));
        }
        public static DataStructures.Point getCaseIndex(Grid grid, Vector2 targ)
        {
            Vector2 min = grid.plane.min;
            Vector2 max = grid.plane.max;

            int current_case_x = (int)Mathf.Floor(Mathf.Abs(max.x - targ.x) / grid.tile_size.x);
            int current_case_y = (int)Mathf.Floor(Mathf.Abs(max.y - targ.y) / grid.tile_size.y);
            Plane p = new Plane(0f, 0f, grid.plane.lenght.x, grid.plane.lenght.y);
           


            int retour_x = current_case_x;
            int retour_y = current_case_y;
            if (retour_x == grid.nb_tiles.x)
            {
                retour_x = (int)grid.nb_tiles.x - 1;
            }
            if (retour_y == grid.nb_tiles.y)
            {
                retour_y = (int)grid.nb_tiles.y - 1;
            }
            retour_x = Math.Abs(((int)grid.nb_tiles.x - 1) - retour_x);
            retour_y = Math.Abs(((int)grid.nb_tiles.y - 1) - retour_y);
            return new DataStructures.Point(retour_x, retour_y);
        }
    }
}
