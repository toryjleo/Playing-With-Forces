using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playing_With_Forces
{
    class Mover
    {
        private float scale;
        private Vector2 position;
        private Vector2 velocity;
        private Vector2 acceleration;
        private SpriteBatch spriteBatch;
        private GraphicsDevice graphicsDevice;
        private Texture2D vanillaCircle;
        private Color color;
        

        public Mover(float scale, int x, int y, SpriteBatch spriteBatch, GraphicsDevice graphicsDevice, Texture2D texture, Color color) {
            this.scale = scale;
            position = new Vector2(x, y);
            velocity = new Vector2(0, 0);
            acceleration = new Vector2(0, 0);
            this.spriteBatch = spriteBatch;
            this.graphicsDevice = graphicsDevice;
            vanillaCircle = texture;
            this.color = color;
        }

        public void Draw()
        {
            spriteBatch.Draw(vanillaCircle, new Vector2(position.X - (int)(vanillaCircle.Width * scale / 2), position.Y - (int)(vanillaCircle.Width * scale / 2)), null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            
        }

        public void AddForce(Vector2 force)
        {
            
            acceleration += force / scale;
        }

        public void ApplyFriction(float mew)
        {
            if (velocity.LengthSquared() != 0)
            {
                Vector2 friction = new Vector2(-velocity.X, -velocity.Y);
                friction.Normalize();
                friction *= mew;

                this.acceleration += friction;
            }
        }

        public void FinalizeMovement()
        {
            // Update the movement variables
            velocity += acceleration;
            position += velocity;
            acceleration = Vector2.Zero;

            // Bounce off the bounds of the screen
            if (position.X + (vanillaCircle.Width * scale / 2) >= graphicsDevice.Viewport.Width ||
                position.X - (vanillaCircle.Width * scale / 2) <= 0)
            {
                velocity.X *= -1;
            }
            if (position.Y + (vanillaCircle.Height * scale / 2) >= graphicsDevice.Viewport.Height ||
                position.Y - ( vanillaCircle.Height * scale / 2) <= 0)
            {
                velocity.Y *= -1;
            }

        }

        public Vector2 GetPosition() {
            return position;
        }

    }
}
