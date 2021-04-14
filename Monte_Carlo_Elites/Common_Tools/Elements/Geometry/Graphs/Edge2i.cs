using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Tools.Elements
{
    public struct Edge2i: IEquatable<Edge2i>
    {
        public readonly Vec2i origin;
        public readonly Vec2i exit;

        public Edge2i(Vec2i origin, Vec2i exit)
        {
            this.origin = origin;
            this.exit = exit;
        }

        public Edge2i (Edge2i edge_to_copy)
        {
            this.origin = edge_to_copy.origin;
            this.exit = edge_to_copy.exit;
        }

        public Edge2i Reverse()
        {
            return new Edge2i(exit, origin);
        }

        public static Edge2i FromDirection(Directions_Ortho_2D direction)
        {
            if (direction.IsSingle() == false)
            {
                throw new System.Exception("direction is not single!");
            }
            else if (direction == Directions_Ortho_2D.None)
            {
                throw new System.Exception("direction is None!");
            }
            if (direction == Directions_Ortho_2D.U)
            {
                return new Edge2i(new Vec2i(0, 0), Vec2i.up);
            }
            if (direction == Directions_Ortho_2D.D)
            {
                return new Edge2i(new Vec2i(0, 0), Vec2i.down);
            }
            if (direction == Directions_Ortho_2D.L)
            {
                return new Edge2i(new Vec2i(0, 0), Vec2i.left);
            }
            if (direction == Directions_Ortho_2D.R)
            {
                return new Edge2i(new Vec2i(0, 0), Vec2i.right);
            }
            return new Edge2i();
        }

        public Directions_Ortho_2D ToDirection()
        {
            Vec2i diff = exit - origin;
            if (diff == Vec2i.up)
            {
                return Directions_Ortho_2D.U;
            }
            if (diff == Vec2i.down)
            {
                return Directions_Ortho_2D.D;
            }
            if (diff == Vec2i.left)
            {
                return Directions_Ortho_2D.L;
            }
            if (diff == Vec2i.right)
            {
                return Directions_Ortho_2D.R;
            }
            return Directions_Ortho_2D.None;
        }

        public override string ToString()
        {
            return "[" + origin.ToString() + "," + exit.ToString() + "]";
        }

        #region equality override
        public bool Equals(Edge2i other_edge) {
            if (
                this.origin == other_edge.origin
                &&
                this.exit == other_edge.exit
                )
            {
                return true;
            }
            else {
                return false;
            }
        }
        public override bool Equals(object otherObject)
        {
            if (otherObject is Edge2i other_edge) {
                return Equals(other_edge);
            }

            return false;
        }
        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + origin.GetHashCode();
            hash = hash * 31 + exit.GetHashCode();
            return hash;
        }
        public static bool operator ==(Edge2i c1, Edge2i c2)
        {
            if (Object.ReferenceEquals(c1, null))
            {
                if (Object.ReferenceEquals(c2, null))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else // c1 is not null
            {
                if (Object.ReferenceEquals(c2, null)) // c2 is null
                {
                    return false;
                }
            }
            return c1.Equals(c2);
        }
        public static bool operator !=(Edge2i c1, Edge2i c2)
        {
            return !(c1 == c2);
        }
        #endregion
    }
}
