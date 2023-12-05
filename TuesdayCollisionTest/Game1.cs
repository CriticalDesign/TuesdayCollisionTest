using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TuesdayCollisionTest
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _hero, _bullet;
        private float _heroX, _heroY, _bulletX, _heroScale, _heroSpeed;
        private bool _heroAlive;
        private float _mouseX, _mouseY;
        private Vector2 _leftStick;
        private SpriteFont _gameFont;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _heroX = 50;
            _bulletX = 800;
            _heroScale = 0.3f;
            _heroAlive = true;
            _heroSpeed = 5;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _hero = Content.Load<Texture2D>("hero");
            _bullet = Content.Load<Texture2D>("bullet");
            _gameFont = Content.Load<SpriteFont>("GameFont");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            _mouseX = Mouse.GetState().X;
            _mouseY = Mouse.GetState().Y;

            /*
            if(Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                _heroX = _mouseX;
                _heroY = _mouseY;
            }
            */

            _leftStick = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left;
            if(GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
                    _heroScale *= 1.01f;
            if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed)
                _heroScale *= 0.98f;


            _heroX += _leftStick.X * _heroSpeed;
            _heroY -= _leftStick.Y * _heroSpeed;

            _bulletX--;


            Rectangle _heroHitBox = new Rectangle((int)_heroX, (int)_heroY, (int)(_hero.Width*_heroScale), (int)(_hero.Height*_heroScale));
            Rectangle _bulletHitBox = new Rectangle((int)_bulletX, 240, (int)(_bullet.Width*0.3f), (int)(_bullet.Height*0.3f));
            if (_heroHitBox.Intersects(_bulletHitBox))
            {
                _heroAlive = false;
                //_heroScale = 0.5f;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);

            _spriteBatch.DrawString(_gameFont, "Mouse X, Y = " + _mouseX + ", " + _mouseY, new Vector2(10, 440), Color.White);

            _spriteBatch.DrawString(_gameFont, "Left Stick X, Y = " + _leftStick.X + ", " + _leftStick.Y, new Vector2(10, 410), Color.White);




            if (_heroAlive)
            {
                _spriteBatch.Draw(_hero, new Vector2(_heroX, _heroY), null, Color.White, 0, new Vector2(_hero.Width / 2, _hero.Height / 2), _heroScale, SpriteEffects.None, 0);

                _spriteBatch.Draw(_bullet, new Vector2(_bulletX, 240), null, Color.White, 0, new Vector2(_bullet.Width / 2, _bullet.Height / 2), 0.3f, SpriteEffects.FlipHorizontally, 0);
            }
            else
            {
                _spriteBatch.Draw(_hero, new Vector2(_heroX, 240), null, Color.Red, 0, new Vector2(_hero.Width / 2, _hero.Height / 2), _heroScale, SpriteEffects.FlipVertically, 0);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
