using Sharpex2D;
using Sharpex2D.Content;
using Sharpex2D.Math;
using Sharpex2D.Rendering;
using Sharpex2D.Rendering.Scene;

namespace SwarmGame.Scenes
{
    public class IntroScene : Scene
    {
        Font font;
        string greeting = "Hello, World!";

        public override void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.Seconds > 1)
            {
                SceneManager sceneManager = SGL.Components.Get<SceneManager>();
                sceneManager.ActiveScene = sceneManager.Get<MenuScene>();
            }
        }

        public override void Render(RenderDevice renderer, GameTime gameTime)
        {
            GraphicsDevice graphicsDevice = renderer.GraphicsDevice;
            graphicsDevice.ClearColor = Color.Black;
            Vector2 screenCenter = new Vector2(renderer.GraphicsDevice.BackBuffer.Width,
                graphicsDevice.BackBuffer.Height) / 2;
            Vector2 textPosition = screenCenter - (renderer.MeasureString(greeting, font) / 2);
            renderer.DrawString(greeting, font, textPosition, Color.White);
        }

        public override void Initialize()
        {

        }

        public override void LoadContent(ContentManager content)
        {
            font = new Font("Arial", 20f, TypefaceStyle.Bold);
        }

    }
}