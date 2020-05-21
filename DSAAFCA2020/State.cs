using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSAAFCA2020
{
   public abstract class State
    {
        protected ContentManager _content;
        protected GraphicsDevice _graphicsdevice;
        protected Game1 _game;


        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void PostUpdate(GameTime gameTime);

        public State(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
        {
            _game = game;
            _graphicsdevice = graphicsDevice;
            _content = content;
        }

        public abstract void Update(GameTime gameTime);


    }
}
