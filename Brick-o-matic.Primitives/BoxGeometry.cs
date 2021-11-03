using Brick_o_matic.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Brick_o_matic.Primitives
{
    public struct BoxGeometry
    {
       
        public Plane PositiveX
        {
            get;
            set;
        }
        public Plane NegativeX
        {
            get;
            set;
        }
        public Plane PositiveY
        {
            get;
            set;
        }
        public Plane NegativeY
        {
            get;
            set;
        }
        public Plane PositiveZ
        {
            get;
            set;
        }
        public Plane NegativeZ
        {
            get;
            set;
        }

        public IEnumerable<Plane> Planes
		{
            get
			{
                yield return NegativeX;
                yield return PositiveX;
                yield return NegativeY;
                yield return PositiveY;
                yield return NegativeZ;
                yield return PositiveZ;
            }
        }

        public bool IsFlat
		{
            get
			{
                return ((PositiveX.Position <= NegativeX.Position) || (PositiveY.Position <= NegativeY.Position) || (PositiveZ.Position <= NegativeZ.Position));
            }
		}
        public BoxGeometry(Position Position, Size Size,Color Color)
        {
            this.NegativeX = new Plane(Position.X, PlaneDirections.NegativeX, Color);
            this.PositiveX = new Plane(Position.X + Size.X, PlaneDirections.PositiveX, Color);
            this.NegativeY = new Plane(Position.Y, PlaneDirections.NegativeY, Color);
            this.PositiveY = new Plane(Position.Y + Size.Y, PlaneDirections.PositiveY, Color);
            this.NegativeZ = new Plane(Position.Z, PlaneDirections.NegativeZ, Color);
            this.PositiveZ = new Plane(Position.Z + Size.Z, PlaneDirections.PositiveZ, Color);
        }
       
        public BoxGeometry(Plane NegativeX,Plane PositiveX, Plane NegativeY, Plane PositiveY, Plane NegativeZ, Plane PositiveZ)
        {
            this.NegativeX = NegativeX;
            this.PositiveX = PositiveX;
            this.NegativeY = NegativeY;
            this.PositiveY = PositiveY;
            this.NegativeZ = NegativeZ;
            this.PositiveZ = PositiveZ;
        }


        public bool IntersectWith(BoxGeometry Other)
        {
            if ((this.IsFlat) || (Other.IsFlat)) return false;
            if (
                (this.NegativeX.Position >= Other.PositiveX.Position) ||
                (this.PositiveX.Position <= Other.NegativeX.Position) ||
                (this.NegativeY.Position >= Other.PositiveY.Position) ||
                (this.PositiveY.Position <= Other.NegativeY.Position) ||
                (this.NegativeZ.Position >= Other.PositiveZ.Position) ||
                (this.PositiveZ.Position <= Other.NegativeZ.Position)) return false;
            return true;
        }


        public bool SplitWithPlane(Plane Plane, out BoxGeometry PartA,out BoxGeometry PartB)
        {
            Plane minPlane, maxPlane;


            PartA = this;PartB = this;            
            switch(Plane.Direction)
			{
                case PlaneDirections.NegativeX:
                    minPlane = this.NegativeX;
                    maxPlane = this.PositiveX;
                    break;
                case PlaneDirections.PositiveX:
                    minPlane = this.NegativeX;
                    maxPlane = this.PositiveX;
                    break;
                case PlaneDirections.NegativeY:
                    minPlane = this.NegativeY;
                    maxPlane = this.PositiveY;
                    break;
                case PlaneDirections.PositiveY:
                    minPlane = this.NegativeY;
                    maxPlane = this.PositiveY;
                    break;
                case PlaneDirections.NegativeZ:
                    minPlane = this.NegativeZ;
                    maxPlane = this.PositiveZ;
                    break;
                case PlaneDirections.PositiveZ:
                    minPlane = this.NegativeZ;
                    maxPlane = this.PositiveZ;
                    break;
                default:
                    throw new NotImplementedException($"Invalid plane direction {Plane.Direction}");
            }


            if ((Plane.Position <= minPlane.Position) || (Plane.Position >= maxPlane.Position)) return false;

            switch (Plane.Direction)
            {
                case PlaneDirections.NegativeX:
                    PartA= new BoxGeometry(NegativeX, new Plane(Plane.Position, PlaneDirections.PositiveX, Plane.Color), NegativeY, PositiveY, NegativeZ, PositiveZ);
                    PartB = new BoxGeometry(new Plane(Plane.Position, PlaneDirections.NegativeX, NegativeX.Color), PositiveX, NegativeY, PositiveY, NegativeZ, PositiveZ);
                    break;
                case PlaneDirections.PositiveX:
                    PartA = new BoxGeometry(NegativeX, new Plane(Plane.Position, PlaneDirections.PositiveX, PositiveX.Color), NegativeY, PositiveY, NegativeZ, PositiveZ);
                    PartB = new BoxGeometry(new Plane(Plane.Position, PlaneDirections.NegativeX, Plane.Color), PositiveX, NegativeY, PositiveY, NegativeZ, PositiveZ);
                    break;
                case PlaneDirections.NegativeY:
                    PartA = new BoxGeometry(NegativeX, PositiveX, NegativeY, new Plane(Plane.Position, PlaneDirections.PositiveY, Plane.Color), NegativeZ, PositiveZ);
                    PartB = new BoxGeometry(NegativeX, PositiveX, new Plane(Plane.Position, PlaneDirections.NegativeY, NegativeY.Color), PositiveY, NegativeZ, PositiveZ);
                    break;
                case PlaneDirections.PositiveY:
                    PartA = new BoxGeometry(NegativeX, PositiveX, NegativeY, new Plane(Plane.Position, PlaneDirections.PositiveY, NegativeY.Color), NegativeZ, PositiveZ);
                    PartB = new BoxGeometry(NegativeX, PositiveX, new Plane(Plane.Position, PlaneDirections.NegativeY, Plane.Color), PositiveY, NegativeZ, PositiveZ);
                    break;
                case PlaneDirections.NegativeZ:
                    PartA = new BoxGeometry(NegativeX, PositiveX, NegativeY, PositiveY, NegativeZ, new Plane(Plane.Position, PlaneDirections.PositiveZ, Plane.Color));
                    PartB = new BoxGeometry(NegativeX, PositiveX, NegativeY, PositiveY, new Plane(Plane.Position, PlaneDirections.NegativeZ, NegativeZ.Color), PositiveZ);
                    break;
                case PlaneDirections.PositiveZ:
                    PartA = new BoxGeometry(NegativeX, PositiveX, NegativeY, PositiveY, NegativeZ, new Plane(Plane.Position, PlaneDirections.PositiveZ, NegativeZ.Color));
                    PartB = new BoxGeometry(NegativeX, PositiveX, NegativeY, PositiveY, new Plane(Plane.Position, PlaneDirections.NegativeZ, Plane.Color), PositiveZ);
                    break;
                default:
                    throw new NotImplementedException($"Invalid plane direction {Plane.Direction}");
            }

            return true;
         }
 
		public IEnumerable<BoxGeometry> SplitWith(BoxGeometry Other)
        {
            BoxGeometry partA, partB;
            List<BoxGeometry> thisCandidates,otherCandidates;
            int t = 0;

            if (this.IsFlat || Other.IsFlat)
			{
                return new BoxGeometry[] { this, Other };
			}

            thisCandidates = new List<BoxGeometry>();
            thisCandidates.Add(this);
            t = 0;
            while (t<thisCandidates.Count)
			{
                foreach (Plane plane in Other.Planes)
                {
                    if (thisCandidates[t].SplitWithPlane(plane, out partA, out partB))
                    {
                        thisCandidates.RemoveAt(t);
                        thisCandidates.Add(partA);
                        thisCandidates.Add(partB);
                        if (!thisCandidates[t].IntersectWith(Other)) break;
                    }
                }
                t++;
			}

            otherCandidates = new List<BoxGeometry>();
            otherCandidates.Add(Other);
            t = 0;
            while (t < otherCandidates.Count)
            {
                foreach (Plane plane in this.Planes)
                {
                    if (otherCandidates[t].SplitWithPlane(plane, out partA, out partB))
                    {
                        otherCandidates.RemoveAt(t);
                        otherCandidates.Add(partA);
                        otherCandidates.Add(partB);
                        if (!otherCandidates[t].IntersectWith(this)) break;
                    }
                }
                t++;
            }

            return thisCandidates.Union(otherCandidates) ;
        }


    }
}
