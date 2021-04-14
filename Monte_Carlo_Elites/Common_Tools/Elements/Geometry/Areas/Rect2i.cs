using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace Common_Tools.Elements
{
    public struct Rect2i
    {
        public readonly Vec2i min_coords;
        public readonly Vec2i max_coords;

        public Rect2i(
            int width,
            int height
            )
        {
            if (width < 0 || height < 0)
            {
                throw new ArgumentException("width and height must be > 0");
            }
            min_coords = new Vec2i(0, 0);
            max_coords = new Vec2i(width, height);
        }

        public Rect2i(
            int size
            )
        {
            if (size < 0)
            {
                throw new ArgumentException("size must be > 0");
            }
            min_coords = new Vec2i(0, 0);
            max_coords = new Vec2i(size, size);
        }

        public Rect2i(
            Vec2i p1,
            Vec2i p2
            )
        {
            // just in case...
            Vec2i min = Vec2i.MinCoords(p1, p2);
            Vec2i max = Vec2i.MaxCoords(p1, p2);
            min_coords = min;
            max_coords = max;
        }

        #region queries

        public static HashSet<Vec2i> GeometricNeighbors_WithinRect(
            Rect2i rect,
            Vec2i point,
            Directions_Ortho_2D directions
            )
        {
            var neighbors = point.Geometric_Neighbors(directions);

            neighbors.RemoveWhere(
                x =>
                rect.Contains(x) == false
                );

            return neighbors;
        }

        public static HashSet<Vec2i> GeometricNeighbors_WithinRect(
            Rect2i rect,
            HashSet<Vec2i> points,
            Directions_Ortho_2D directions
            )
        {
            var neighbors = new HashSet<Vec2i>();

            foreach (var point in points)
            {
                neighbors.UnionWith(GeometricNeighbors_WithinRect(rect, point, directions));
            }

            neighbors.ExceptWith(points);

            return neighbors;
        }



        public int Left()
        {
            return min_coords.x;
        }

        public int Right()
        {
            return max_coords.x;
        }

        public int Down()
        {
            return min_coords.y;
        }

        public int Up()
        {
            return max_coords.y;
        }


        public Vec2i Up_Left_Corner()
        {
            return new Vec2i(Min_X(), Max_Y());
        }
        public Vec2i Up_Right_Corner()
        {
            return new Vec2i(Max_X(), Max_Y());
        }
        public Vec2i Down_Left_Corner()
        {
            return new Vec2i(Min_X(), Min_Y());
        }
        public Vec2i Down_Right_Corner()
        {
            return new Vec2i(Max_X(), Min_Y());
        }
        public HashSet<Vec2i> Corners()
        {
            return new HashSet<Vec2i>() {
                Up_Left_Corner(),
                Up_Right_Corner(),
                Down_Left_Corner(),
                Down_Right_Corner()
            };
        }

        

        

        public bool ContainsAll(
            HashSet<Vec2i> points
            )
        {
            foreach (var point in points)
            {
                if (this.Contains(point) == false)
                {
                    return false;
                }
            }
            return true;
        }

        public bool ContainsAny(
            HashSet<Vec2i> points
            )
        {
            foreach (var point in points)
            {
                if (this.Contains(point))
                {
                    return true;
                }
            }
            return false;
        }


        public bool Intersects(Rect2i otherRect)
        {
            return ContainsAny(otherRect.Corners());
        }

        public Rect2i Random_ContainedRect(
            System.Random rand
            )
        {
            Vec2i p1 = Random_ContainedPoint(rand);
            Vec2i p2 = Random_ContainedPoint(rand);
            return new Rect2i(p1, p2);
        }

        public Rect2i Random_ContainedRect(
            System.Random rand,
            int max_width,
            int max_height
            )
        {
            int x0 = rand.Next(X_Dots()) + Min_X();
            int y0 = rand.Next(Y_Dots()) + Min_Y();

            int x_remaining = Max_X() - x0;
            int y_remaining = Max_Y() - y0;

            int x1;
            int y1;
            if (x_remaining > max_width)
            {
                x1 = x0 + rand.Next(max_width);
            }
            else
            {
                x1 = x0 + rand.Next(x_remaining);
            }

            if (y_remaining > max_width)
            {
                y1 = y0 + rand.Next(max_height);
            }
            else
            {
                y1 = y0 + rand.Next(y_remaining);
            }

            Vec2i p1 = new Vec2i(x0, y0);
            Vec2i p2 = new Vec2i(x1, y1);
            return new Rect2i(p1, p2);
        }

        public Vec2i Random_ContainedPoint(System.Random rand)
        {
            return new Vec2i(
                rand.Next(Min_X(), Max_X() + 1),
                rand.Next(Min_Y(), Max_Y() + 1)
                );
        }

        public int Min_X()
        {
            return min_coords.x;
        }
        public int Max_X()
        {
            return max_coords.x;
        }
        public int Min_Y()
        {
            return min_coords.y;
        }
        public int Max_Y()
        {
            return max_coords.y;
        }
        public int Width()
        {
            return Max_X() - Min_X();
        }
        public int Height()
        {
            return Max_Y() - Min_Y();
        }
        public int X_Dots()
        {
            return Width() + 1;
        }
        public int Y_Dots()
        {
            return Height() + 1;
        }
        #endregion


        


        //public HashSet<PM_Edge> 
        public override string ToString()
        {
            return "[" + min_coords.ToString() + "," + max_coords.ToString() + "]";
        }

        #region equality overrides && operator overloadings
        public override bool Equals(object otherObject)
        {
            if (otherObject.GetType() != this.GetType())
            {
                return false;
            }
            Rect2i other = (Rect2i)otherObject;
            if (
                this.min_coords == other.min_coords
                &&
                this.max_coords == other.max_coords
                )
            {
                return true;
            }
            return false;
        }
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + min_coords.GetHashCode();
            hash = hash * 31 + max_coords.GetHashCode();
            return hash;
        }
        public static bool operator ==(Rect2i b1, Rect2i b2)
        {
            return b1.Equals(b2);
        }
        public static bool operator !=(Rect2i b1, Rect2i b2)
        {
            return !(b1 == b2);
        }
        #endregion

    }
}
