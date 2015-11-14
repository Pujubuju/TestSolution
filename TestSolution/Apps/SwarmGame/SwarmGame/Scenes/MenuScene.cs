using Sharpex2D;
using Sharpex2D.Content;
using Sharpex2D.Math;
using Sharpex2D.Rendering;
using Sharpex2D.Rendering.Scene;

namespace SwarmGame.Scenes
{
    public class MenuScene : Scene
    {
        private Font _font;

        public override void Update(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.Seconds > 2)
            {
                SceneManager sceneManager = SGL.Components.Get<SceneManager>();
                sceneManager.ActiveScene = sceneManager.Get<GameScene>();
            }
        }

        public override void Render(RenderDevice renderer, GameTime gameTime)
        {
            renderer.GraphicsDevice.ClearColor = Color.CornflowerBlue;
            renderer.DrawString("Menu", _font, new Vector2(100, 100), Color.White);
        }

        public override void Initialize()
        {
        }

        public override void LoadContent(ContentManager content)
        {
            _font = new Font("Helvetica", 25f, TypefaceStyle.Bold);
        }
    }
}
