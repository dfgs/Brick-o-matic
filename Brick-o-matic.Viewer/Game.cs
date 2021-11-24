using Brick_o_matic.Parsing;
using Brick_o_matic.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using System.Collections.Generic;
using Brick_o_matic.Viewer.Cameras;
using Brick_o_matic.Math;
using System.IO;

namespace Brick_o_matic.Viewer
{
	public class Game : Microsoft.Xna.Framework.Game
	{
        private GraphicsDeviceManager graphics;

        private Effect effect;

        //Camera
        private Matrix projectionMatrix;
        private Matrix worldMatrix;

        private BrickMesh[] meshes;

        private OrbitCamera camera;

        private string fileName;

        private Scene scene;

        private DateTime updateTime;
        private TimeSpan elapsedTime;
        private TimeSpan checkInterval = TimeSpan.FromSeconds(2);

        public Game(string FileName)
        {
            if (FileName == null) throw new ArgumentNullException(nameof(FileName));
            
            this.fileName = FileName;
            if (File.Exists(fileName))
			{
                updateTime = File.GetLastWriteTime(fileName);
            }
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Window.AllowUserResizing = true;
            
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;

            camera = new OrbitCamera();
            camera.Distance = 0;

            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(60f), graphics.GraphicsDevice.Viewport.AspectRatio,0.01f, 1000f);
            worldMatrix = Matrix.CreateWorld(new Vector3(), Vector3.Forward, Vector3.Up);

            

            base.Initialize();
        }

        protected override void LoadContent()
        {

            effect = Content.Load<Effect>("Effect");

            LoadMeshes();

        }

        
        protected override void UnloadContent()
        {
        }

        private void LoadMeshes()
        {
            IBox boundingBox;

            try
            {
                scene = SceneReader.ReadFromFile(fileName);
            }
            catch(Exception ex)
            {
                File.WriteAllText("debug.log", ex.Message);
                Console.WriteLine(ex.Message);
            }

            if (scene != null)
            {
                try
                {
                    boundingBox = scene.GetBoundingBox(null);
                    meshes = scene.Build(null).Select(item => new BrickMesh(scene, item)).ToArray();

                    if (camera.Distance == 0)
                    {
                        camera.Distance = 2.5f * boundingBox.Size.Y;
                    }
                    Array.ForEach(meshes, item => item.LoadContent(GraphicsDevice));
                }
                catch (Exception ex)
                {
                    File.WriteAllText("debug.log", ex.Message);
                    Console.WriteLine(ex.Message);
                }
            }
  
        }

        protected override void Update(GameTime gameTime)
        {
            DateTime newUpdateTime;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            elapsedTime += gameTime.ElapsedGameTime;
            if (elapsedTime > checkInterval)
            {
                elapsedTime -= checkInterval;
                if (File.Exists(fileName))
                {
                    newUpdateTime = File.GetLastWriteTime(fileName);
                    if (newUpdateTime != updateTime)
                    {
                        LoadMeshes();
                        updateTime = newUpdateTime;
                    }
                }
            }


            camera.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Microsoft.Xna.Framework.Color.CornflowerBlue);

         
            effect.Parameters["World"].SetValue(worldMatrix);
            effect.Parameters["View"].SetValue(camera.ViewMatrix);
            effect.Parameters["Projection"].SetValue(projectionMatrix);

            effect.Parameters["DiffuseIntensity"].SetValue(0.5f);
            effect.Parameters["AmbiantIntensity"].SetValue(0.5f);
            effect.Parameters["DiffuseLightDirection"].SetValue(Vector3.Normalize(new Vector3(0.5f, -.75f, -1)));


            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.CullClockwiseFace;
            
            GraphicsDevice.RasterizerState = rasterizerState;

            if (meshes != null)
            {
                foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    Array.ForEach(meshes, item => item.Draw(GraphicsDevice));
                }
            }

            base.Draw(gameTime);
        }
    }




}
