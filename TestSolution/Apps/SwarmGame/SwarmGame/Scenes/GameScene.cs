using Sharpex2D;
using Sharpex2D.Content;
using Sharpex2D.Math;
using Sharpex2D.Rendering;
using Sharpex2D.Rendering.Scene;
using SwarmGame.Domain;
using SwarmGame.Infrastructure;

namespace SwarmGame.Scenes
{
    public class GameScene : Scene
    {

        Texture2D _nuke;

        Texture2D _tank0;
        Texture2D _tank90;
        Texture2D _tank180;
        Texture2D _tank270;

        public GameWorld GameWorld { get; set; }

        public override void Update(GameTime gameTime)
        {
            KeyboardService.Keyboard.Update(gameTime);
        }

        public override void Render(RenderDevice renderer, GameTime gameTime)
        {
            renderer.FillRectangle(Color.Red, new Rectangle(10, 10, 100, 50));
            renderer.DrawRectangle(new Pen(Color.DarkGreen, 3f), new Rectangle(400, 180, 30, 30));
            var elipse1 = new Ellipse(50, 75) {Position = new Vector2(320, 240)};
            renderer.FillEllipse(Color.Blue, elipse1);
            var elipse2 = new Ellipse(60, 60) { Position = new Vector2(500, 400) };
            renderer.DrawEllipse(new Pen(Color.Black, 2f), elipse2);

            renderer.DrawPolygon(new Pen(Color.Green, 7f), new Polygon(
                    new Vector2(50, 50),
                    new Vector2(300, 50),
                    new Vector2(200, 140),
                    new Vector2(300, 240),
                    new Vector2(140, 140)
                ));

            renderer.FillPolygon(Color.Lime, new Polygon(
                    new Vector2(400, 10),
                    new Vector2(500, 30),
                    new Vector2(600, 110),
                    new Vector2(300, 160),
                    new Vector2(300, 110)
                ));

            renderer.DrawTexture(_nuke, new Vector2(50, 50));

            foreach (IGameObject gameObject in GameWorld.GameObjects)
            {
                switch (gameObject.Rotation)
                {
                    case 0:
                        renderer.DrawTexture(_tank0, new Vector2(gameObject.X, gameObject.Y));
                        break;
                    case 90:
                        renderer.DrawTexture(_tank90, new Vector2(gameObject.X, gameObject.Y));
                        break;
                    case 180:
                        renderer.DrawTexture(_tank180, new Vector2(gameObject.X, gameObject.Y));
                        break;
                    case 270:
                        renderer.DrawTexture(_tank270, new Vector2(gameObject.X, gameObject.Y));
                        break;
                }          
            }         
        }

        public override void Initialize()
        {
        }

        public override void LoadContent(ContentManager content)
        {
            _nuke = content.Load<Texture2D>("Images\\nuke.png");

            _tank0 = content.Load<Texture2D>("Images\\tank0.png");
            _tank90 = content.Load<Texture2D>("Images\\tank90.png");
            _tank180 = content.Load<Texture2D>("Images\\tank180.png");
            _tank270 = content.Load<Texture2D>("Images\\tank270.png");

            GameWorld = new GameWorld();
            GameWorld.Start();
        }
    }
}
