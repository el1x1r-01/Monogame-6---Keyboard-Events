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
            pacLocation,
            plusRect,
            minusRect;

        Texture2D pacTexture,
            pacUp,
            pacDown,
            pacRight,
            pacLeft,
            pacSleep,
            plusTexture,
            minusTexture;

        Vector2 pacSpeed;

        KeyboardState keyboardState;

        MouseState mouseState,
            prevMouseState;

        SpriteFont speedFont;

        string speedText;

        int speed;

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

            plusRect = new Rectangle(10, 550, 40, 40);
            minusRect = new Rectangle(60, 550, 40, 40);

            speed = 1;

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

            plusTexture = Content.Load<Texture2D>("Plus");
            minusTexture = Content.Load<Texture2D>("Minus");
            speedFont = Content.Load<SpriteFont>("SpeedFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            pacSpeed = new Vector2();

            keyboardState = Keyboard.GetState();

            pacSpeed = Vector2.Zero;

            if (keyboardState.IsKeyDown(Keys.Up) && pacLocation.Y > 0)
            {
                pacTexture = pacUp;
                pacSpeed.Y -= speed;
            }
            else if (keyboardState.IsKeyDown(Keys.Down) && pacLocation.Y < 525)
            {
                pacTexture = pacDown;
                pacSpeed.Y += speed;
            }
            else if (keyboardState.IsKeyDown(Keys.Left) && pacLocation.X > 0)
            {
                pacTexture = pacLeft;
                pacSpeed.X -= speed;
            }
            else if (keyboardState.IsKeyDown(Keys.Right) && pacLocation.X < 725)
            {
                pacTexture = pacRight;
                pacSpeed.X += speed;
            }
            else
            {
                pacTexture = pacSleep;
            }

            speedText = "Speed: " + speed;

            prevMouseState = mouseState;
            mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed
                && prevMouseState.LeftButton == ButtonState.Released)
            {
                if (plusRect.Contains(mouseState.Position))
                {
                    speed = speed + 1;

                    if (speed > 20)
                    {
                        speed = 1;
                    }
                }
                else if (minusRect.Contains(mouseState.Position))
                {
                    speed = speed - 1;

                    if (speed <= 1)
                    {
                        speed = 1;
                    }
                }
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

            _spriteBatch.DrawString(speedFont, speedText, new Vector2(10, 515), Color.White);
            _spriteBatch.Draw(plusTexture, plusRect, Color.White);
            _spriteBatch.Draw(minusTexture, minusRect, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
