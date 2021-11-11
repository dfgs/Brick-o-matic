using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brick_o_matic.Viewer.Cameras
{
    public abstract class Camera:ICamera
    {
        public abstract Matrix ViewMatrix
        {
            get;
        }
    }
}
