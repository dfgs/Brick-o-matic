using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brick_o_matic.Viewer
{
    public struct VertexPositionNormalColor:IVertexType
    {
        public Vector3 Position
		{
            get;
            set;
		}
        public Vector3 Normal
        {
            get;
            set;
        }

        public Color Color
        {
            get;
            set;
        }

        public static VertexDeclaration vertexDeclaration= new VertexDeclaration(
                new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
                new VertexElement(sizeof(float) * 3, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
                new VertexElement(sizeof(float) * 6, VertexElementFormat.Color, VertexElementUsage.Color, 0)
            );

        public VertexDeclaration VertexDeclaration => vertexDeclaration;

		public VertexPositionNormalColor(Vector3 Position, Vector3 Normal, Color Color)
		{
            this.Position = Position;this.Normal = Normal;this.Color = Color;
		}


    }
}
