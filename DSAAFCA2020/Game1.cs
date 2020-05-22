using ActivityTracker;
using DSAAFCA2020.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Linq;

namespace DSAAFCA2020
{
    
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        string ID = Activity.StudentID;
        string Name = Activity.Name;
        private SpriteFont font;
        string Message = "static background";

        private State _currentState;
        private State _nextState;
        private Camera _camera;
        private Texture2D _playerTexture;
        private Texture2D _enemyTexture;
        private Texture2D _backgroundTexture;
        private Vector2 _playerPosition;
        //private Button _button;
        //private SpriteFont _font;
        //private int _score;
        //private ScoreManager _scoreManager;
        //private float _timer;
        //public static Random Random;

        public void ChangeState(State state)
        {
            _nextState = state;
        }
        Texture2D shoot;
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
            //Random = new Random();
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
            _camera = new Camera();
            _playerTexture = Content.Load<Texture2D>("monoShip");
            _enemyTexture = Content.Load<Texture2D>("monoEn2Ship");
            _backgroundTexture = Content.Load<Texture2D>("background");

            //_scoreManager = ScoreManager.Load();
            //_font = Content.Load<SpriteFont>("Fonts/File");
            //_button = new Button(Content.Load<Texture2D>("button"), _font)
            //{
            //    Text = "Enemy",
            //};

            //_button.Click += Button_Click;
            //_timer = 5;

            //ship = Content.Load<Texture2D>("monoShip");
            //background = Content.Load<Texture2D>("background");
            shoot = Content.Load<Texture2D>("bullet");

            shootOffset = new Vector2(_playerTexture.Width / 15, _playerTexture.Height / 10);
            shootOffset += new Vector2(-5, 0);

            shootSound = Content.Load<SoundEffect>("laser1");
            backgroundMusic = Content.Load<Song>("themeSong");
            MediaPlayer.Play(backgroundMusic);
        }

        //private void Button_Click(object sender, EventArgs e)
        //{
        //    SetButtonPosition((Button)sender);
        //}

        //private void SetButtonPosition (Button button)
        //{
        //    var x = Random.Next(0, graphics.PreferredBackBufferWidth - button.Rectangle.Width);
        //    var y = Random.Next(0, graphics.PreferredBackBufferHeight - button.Rectangle.Height);

        //    button.Position = new Vector2(x, y);
        //}
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

            //_timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            //if (_timer <= 0)
            //{
            //    SetButtonPosition(_button);

            //    _scoreManager.Add(new Models.Score()
            //     {
            //        PlayerName = "Name",
            //        Value = _score,
            //     }
            //    );

            //    ScoreManager.Save(_scoreManager);

            //    _timer = 5;
            //    _score = 0;
            //}
            //_button.Update(gameTime);

            Vector2 movement = Vector2.Zero;
            KeyboardState keystate = Keyboard.GetState();

            //movement
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                _playerPosition.Y -= 3f;
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                _playerPosition.Y += 3f;
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                _playerPosition.X -= 3f;
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                _playerPosition.X += 3f;

            _camera.Follow(_playerPosition);
            //if (keystate.IsKeyDown(Keys.Left))
            //{
            //    movement.X -= 1;
            //}
            //if (keystate.IsKeyDown(Keys.Up))
            //{
            //    movement.Y -= 1;
            //}
            //if (keystate.IsKeyDown(Keys.Down))
            //{
            //    movement.Y += 1;
            //}
            if (Keyboard.GetState().IsKeyDown(Keys.Space) )// && !hasShoot
            {
                shootPosition = _playerPosition + shootOffset;
                hasShoot = true;
                shootSound.Play();
            }
            _playerPosition += movement;

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
            spriteBatch.Draw(_backgroundTexture, GraphicsDevice.Viewport.Bounds, Color.White);
            spriteBatch.DrawString(font, Message + " s00187820" + ID +
                "CianOReilly " + Name, new Vector2(10, 10), Color.White);
            spriteBatch.End();

            spriteBatch.Begin(transformMatrix:_camera.Transform);
            //_button.Draw(gameTime, spriteBatch);
            //spriteBatch.DrawString(_font, "Score:" + _score, new Vector2(20, 10), Color.Red);
            //spriteBatch.DrawString(_font, "Time:" + _timer.ToString("N2"), new Vector2(10, 30), Color.Red);
            //spriteBatch.DrawString(_font, "Highscores:" + string.Join("\n", _scoreManager.Highscores.Select(c=> c.PlayerName + ": "+ c.Value).ToArray()), new Vector2(20, 10), Color.Red);
            if (hasShoot) spriteBatch.Draw(shoot, shootPosition, Color.White);
            spriteBatch.Draw(_playerTexture, _playerPosition, Color.White);
            spriteBatch.Draw(_enemyTexture, new Vector2(0,0), Color.White);
            spriteBatch.Draw(_enemyTexture, new Vector2(5, 5), Color.White);
            spriteBatch.Draw(_enemyTexture, new Vector2(-3, -6), Color.White);
            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
