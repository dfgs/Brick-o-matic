using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brick_o_matic.Viewer.Cameras
{
    public class OrbitCamera:Camera
    {
        private Matrix viewMatrix;
        public override Matrix ViewMatrix
		{
            get => viewMatrix;
		}

        public Vector3 Target
		{
            get;
            set;
		}

        public float Distance
        {
            get;
            set;
        }
        public float MaxDistance
		{
            get;
            set;
		}

        public float MinDistance
		{
            get;
            set;
		}

        private Matrix rotation;
        private MouseState previousState;
        private float yaw, pitch;

        public OrbitCamera()
		{
            previousState = Mouse.GetState();yaw = 0;pitch = 0;
            MinDistance = 1;Distance = 1;MaxDistance = 10000;
            rotation = Matrix.Identity;
		}

        public void Update()
        {
            Quaternion rotation;
            Vector3 negDistance;
            Vector3 position;
            MouseState state;

            state = Mouse.GetState();
            if (state.LeftButton==ButtonState.Pressed)
            {
                yaw += (previousState.Position.X - state.Position.X) * 0.01f;
                pitch += (previousState.Position.Y - state.Position.Y) * 0.01f;
            }
            Distance = MathHelper.Clamp(Distance - (state.ScrollWheelValue - previousState.ScrollWheelValue) * 0.01f, MinDistance, MaxDistance);
 
            rotation = Quaternion.CreateFromYawPitchRoll(yaw, pitch, 0);
            previousState = state;

  
            negDistance = new Vector3(0.0f, 0.0f, -Distance);
            position =  Vector3.Transform( negDistance ,rotation )+ Target;


            //Calculate the relative position of the camera
            position = Vector3.Transform(Vector3.Backward, rotation);
            //Convert the relative position to the absolute position
            position *= Distance;
            position += Target;


            
            viewMatrix= Matrix.CreateLookAt(position, Target, Vector3.Up);// Y up

        }

        
    }
}
