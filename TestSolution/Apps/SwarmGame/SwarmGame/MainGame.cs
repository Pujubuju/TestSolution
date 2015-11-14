using Sharpex2D;
using Sharpex2D.Audio.WaveOut;
using Sharpex2D.GameService;
using Sharpex2D.Math;
using Sharpex2D.Rendering;
using Sharpex2D.Rendering.GDI;
using SwarmGame.Infrastructure;
using SwarmGame.Scenes;

namespace SwarmGame
{
    public class MainGame : Game
    {
        private RenderDevice _renderDevice;

        public override EngineConfiguration OnInitialize(LaunchParameters launchParameters)
        {
            TargetFrameTime = 100;

            Input.Keyboard.InitializeDevice();
            KeyboardService.Keyboard = Input.Keyboard;
            
            GameComponentManager.Add(SceneManager);

            _renderDevice = new GDIRenderDevice(InterpolationMode.Linear, SmoothingMode.None);         
            return new EngineConfiguration(_renderDevice, new WaveOutInitializer());
        }

        public override void OnLoadContent()
        {
            SceneManager.AddScene(new IntroScene());
            SceneManager.AddScene(new MenuScene());
            SceneManager.AddScene(new GameScene());
            SceneManager.ActiveScene = SceneManager.Get<IntroScene>();
        }
    }
}
