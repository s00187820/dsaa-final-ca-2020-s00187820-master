using ActivityTracker;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DSAAFCA2020
{
    
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        string ID = Activity.StudentID;
        string Name = Activity.Name;
        private SpriteFont font;
        string Message = "Basic Movement";

        private State _currentState;
        private State _nextState;

        public void ChangeState(State state)
        {
            _nextState = state;
        }

        Texture2D background;
        Texture2D ship;
        Texture2D shoot;

        Vector2 shipPosition;
        Vector2 shootPosition;
        Vector2 shootOffset;
        bool hasShoot = false;

        SoundEffect shootSound;
        //SoundEffectInstance instance;

        Song backgroundMusic;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

       
        protected override void Initialize()
        {
            IsMouseVisible = true;
            Activity.Track(Message + "s00187820" + ID + "CianOReilly " + Name);
            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("TextFont");

            _currentState = new MenuState(this, graphics.GraphicsDevice, Content);

            ship = Content.Load<Texture2D>("monoShip");
            background = Content.Load<Texture2D>("background");
            shoot = Content.Load<Texture2D>("bullet");

            shootOffset = new Vector2(ship.Width / 15, ship.Height / 10);
            shootOffset += new Vector2(-5, 0);

            shootSound = Content.Load<SoundEffect>("laser1");
            backgroundMusic = Content.Load<Song>("themeSong");
            MediaPlayer.Play(backgroundMusic);
        }

      
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

       
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if(_nextState !=null)
            {
                _currentState = _nextState;
                _nextState = null;
            }
            _currentState.Update(gameTime);
            _currentState.PostUpdate(gameTime);

            Vector2 movement = Vector2.Zero;
            KeyboardState keystate = Keyboard.GetState();
            
            if (keystate.IsKeyDown(Keys.Right))
            {
                movement.X += 1;
            }
            if (keystate.IsKeyDown(Keys.Left))
            {
                movement.X -= 1;
            }
            if (keystate.IsKeyDown(Keys.Up))
            {
                movement.Y -= 1;
            }
            if (keystate.IsKeyDown(Keys.Down))
            {
                movement.Y += 1;
            }
            if (keystate.IsKeyDown(Keys.Space) )// && !hasShoot
            {
                shootPosition = shipPosition + shootOffset;
                hasShoot = true;
                shootSound.Play();
            }

            shipPosition += movement;

            if (hasShoot)
            {
                shootPosition.Y -= 5;
                if (shootPosition.Y > GraphicsDevice.Viewport.Width)
                {
                    hasShoot = false;
                }
               
            }

            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _currentState.Draw(gameTime, spriteBatch);
            spriteBatch.Begin();
            spriteBatch.Draw(background, GraphicsDevice.Viewport.Bounds, Color.White);
            spriteBatch.DrawString(font, Message + " s00187820" + ID +
                "CianOReilly " + Name, new Vector2(10, 10), Color.White);
            if (hasShoot) spriteBatch.Draw(shoot, shootPosition, Color.White);
            spriteBatch.Draw(ship, shipPosition, Color.White);
            
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
