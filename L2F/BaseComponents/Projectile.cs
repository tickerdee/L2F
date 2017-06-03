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
    class Projectile
    {
        Texture2D projectileSprite;
        Rectangle projectileHitBox;

        public Projectile(Texture2D spriteTexture, Vector2 projectilePosition) // Initialization
        {
            projectileSprite = spriteTexture;
            // insert additional code here, such as creation of hitbox
        }
    }
}
