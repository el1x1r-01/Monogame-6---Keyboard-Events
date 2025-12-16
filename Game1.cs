using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Monogame_6___Keyboard_Events
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Rectangle window,
            pacLocation;

        Texture2D pacTexture,
            pacUp,
            pacDown,
            pacRight,
            pacLeft,
            pacSleep;

        Vector2 pacSpeed;

        KeyboardState keyboardState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            window = new Rectangle(0, 0, 800, 600);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;
            _graphics.ApplyChanges();

            pacLocation = new Rectangle(10, 10, 75, 75);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            pacUp = Content.Load<Texture2D>("PacUp");
            pacDown = Content.Load<Texture2D>("PacDown");
            pacRight = Content.Load<Texture2D>("PacRight");
            pacLeft = Content.Load<Texture2D>("PacLeft");
            pacSleep = Content.Load<Texture2D>("PacSleep");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            pacSpeed = new Vector2();

            keyboardState = Keyboard.GetState();

            pacSpeed = Vector2.Zero;

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                pacTexture = pacUp;
                pacSpeed.Y -= 2;
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                pacTexture = pacDown;
                pacSpeed.Y += 2;
            }
            else if (keyboardState.IsKeyDown(Keys.Left))
            {
                pacTexture = pacLeft;
                pacSpeed.X -= 2;
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                pacTexture = pacRight;
                pacSpeed.X += 2;
            }
            else
            {
                pacTexture = pacSleep;
            }

            pacLocation.Offset(pacSpeed);

                base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            _spriteBatch.Draw(pacTexture, pacLocation, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
