using Brick_o_matic.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brick_o_matic.Viewer
{
    public class BrickMesh
    {

		private VertexBuffer vertexBuffer;
        private IndexBuffer indexBuffer;
        private Brick brick;
		private Scene scene;

		
		public BrickMesh(Scene Scene, Brick Brick)
		{
			if (Scene == null) throw new ArgumentNullException(nameof(Scene));
			if (Brick == null) throw new ArgumentNullException(nameof(Brick));
			this.scene = Scene;
			this.brick = Brick;
		}
		public void LoadContent(GraphicsDevice GraphicsDevice)
		{
			Microsoft.Xna.Framework.Color color;

			color = brick.Color.ToXNAColor(scene);
			VertexPositionNormalColor[] vertices = new VertexPositionNormalColor[24];
			

			// vertex position and color information 

			// front face
			vertices[0] = new VertexPositionNormalColor(new Vector3(brick.Position.X, brick.Position.Y, brick.Position.Z + brick.Size.Z),Vector3.Backward, color);
			vertices[1] = new VertexPositionNormalColor(new Vector3(brick.Position.X + brick.Size.X, brick.Position.Y, brick.Position.Z + brick.Size.Z), Vector3.Backward, color);
			vertices[2] = new VertexPositionNormalColor(new Vector3(brick.Position.X + brick.Size.X, brick.Position.Y + brick.Size.Y, brick.Position.Z + brick.Size.Z), Vector3.Backward, color);
			vertices[3] = new VertexPositionNormalColor(new Vector3(brick.Position.X, brick.Position.Y + brick.Size.Y, brick.Position.Z + brick.Size.Z), Vector3.Backward, color);
			// back face
			vertices[4] = new VertexPositionNormalColor(new Vector3(brick.Position.X, brick.Position.Y, brick.Position.Z), Vector3.Forward, color);
			vertices[5] = new VertexPositionNormalColor(new Vector3(brick.Position.X + brick.Size.X, brick.Position.Y, brick.Position.Z), Vector3.Forward, color);
			vertices[6] = new VertexPositionNormalColor(new Vector3(brick.Position.X + brick.Size.X, brick.Position.Y + brick.Size.Y, brick.Position.Z), Vector3.Forward, color);
			vertices[7] = new VertexPositionNormalColor(new Vector3(brick.Position.X, brick.Position.Y + brick.Size.Y, brick.Position.Z), Vector3.Forward, color);
			// left face
			vertices[8] = new VertexPositionNormalColor(new Vector3(brick.Position.X, brick.Position.Y, brick.Position.Z), Vector3.Left, color);
			vertices[9] = new VertexPositionNormalColor(new Vector3(brick.Position.X, brick.Position.Y, brick.Position.Z + brick.Size.Z), Vector3.Left, color);
			vertices[10] = new VertexPositionNormalColor(new Vector3(brick.Position.X, brick.Position.Y + brick.Size.Y, brick.Position.Z + brick.Size.Z), Vector3.Left, color);
			vertices[11] = new VertexPositionNormalColor(new Vector3(brick.Position.X, brick.Position.Y + brick.Size.Y, brick.Position.Z), Vector3.Left, color);
			// right face
			vertices[12] = new VertexPositionNormalColor(new Vector3(brick.Position.X + brick.Size.X, brick.Position.Y, brick.Position.Z), Vector3.Right, color);
			vertices[13] = new VertexPositionNormalColor(new Vector3(brick.Position.X + brick.Size.X, brick.Position.Y, brick.Position.Z + brick.Size.Z), Vector3.Right, color);
			vertices[14] = new VertexPositionNormalColor(new Vector3(brick.Position.X + brick.Size.X, brick.Position.Y + brick.Size.Y, brick.Position.Z + brick.Size.Z), Vector3.Right, color);
			vertices[15] = new VertexPositionNormalColor(new Vector3(brick.Position.X + brick.Size.X, brick.Position.Y + brick.Size.Y, brick.Position.Z), Vector3.Right, color);
			// top face
			vertices[16] = new VertexPositionNormalColor(new Vector3(brick.Position.X, brick.Position.Y + brick.Size.Y, brick.Position.Z), Vector3.Up, color);
			vertices[17] = new VertexPositionNormalColor(new Vector3(brick.Position.X + brick.Size.X, brick.Position.Y + brick.Size.Y, brick.Position.Z), Vector3.Up, color);
			vertices[18] = new VertexPositionNormalColor(new Vector3(brick.Position.X + brick.Size.X, brick.Position.Y + brick.Size.Y, brick.Position.Z + brick.Size.Z), Vector3.Up, color);
			vertices[19] = new VertexPositionNormalColor(new Vector3(brick.Position.X, brick.Position.Y + brick.Size.Y, brick.Position.Z + brick.Size.Z), Vector3.Up, color);
			// bottom face
			vertices[20] = new VertexPositionNormalColor(new Vector3(brick.Position.X, brick.Position.Y, brick.Position.Z), Vector3.Down, color);
			vertices[21] = new VertexPositionNormalColor(new Vector3(brick.Position.X + brick.Size.X, brick.Position.Y, brick.Position.Z), Vector3.Down, color);
			vertices[22] = new VertexPositionNormalColor(new Vector3(brick.Position.X + brick.Size.X, brick.Position.Y, brick.Position.Z + brick.Size.Z), Vector3.Down, color);
			vertices[23] = new VertexPositionNormalColor(new Vector3(brick.Position.X, brick.Position.Y, brick.Position.Z + brick.Size.Z), Vector3.Down, color);

			// Set up the vertex buffer
			vertexBuffer = new VertexBuffer(GraphicsDevice,typeof(VertexPositionNormalColor), vertices.Length, BufferUsage.WriteOnly);
			vertexBuffer.SetData<VertexPositionNormalColor>(vertices);
			
			// vertex indices 
			short[] indices = new short[36];

			// front face
			indices[0]=0; indices[1]=1; indices[2]=3;
			indices[3]=1; indices[4]=2; indices[5]=3;
			// back face
			indices[6]=4; indices[7]=7; indices[8]=5;
			indices[9]=5; indices[10]=7; indices[11]=6;
			// left face
			indices[12]=8; indices[13]=9; indices[14]=11;
			indices[15]=9; indices[16]=10; indices[17]=11;
			// right face
			indices[18]=12; indices[19]=15; indices[20]=13;
			indices[21]=13; indices[22]=15; indices[23]=14;
			// top face
			indices[24]=16; indices[25]=19; indices[26]=17;
			indices[27]=17; indices[28]=19; indices[29]=18;
			// bottom face
			indices[30]=20; indices[31]=21; indices[32]=23;
			indices[33]=21; indices[34]=22; indices[35]=23;
			

            indexBuffer = new IndexBuffer(GraphicsDevice, typeof(short), indices.Length, BufferUsage.WriteOnly);
            indexBuffer.SetData(indices);
        }

        public void Draw(GraphicsDevice GraphicsDevice)
		{
			
			GraphicsDevice.SetVertexBuffer(vertexBuffer);
			GraphicsDevice.Indices = indexBuffer;
			
			
			GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 12);
        }


    }
}
