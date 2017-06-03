using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace L2F
{
    class Camera
    {
        public Matrix transform;
        Viewport view;
        Vector2 center;

        public Camera(Viewport newView)
        {
            view = newView;
        }

        public void update(GameTime GameTime, Vector2 CameraLocation, Vector2 Scale)
        {
            //TODO: replace mouse location with actual sprite data
            center = CameraLocation;
            transform = Matrix.CreateScale(new Vector3(Scale.X, Scale.Y, 0)) * Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0));
        }
    }
}
