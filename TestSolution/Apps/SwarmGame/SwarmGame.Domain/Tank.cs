using Sharpex2D.Input;
using SwarmGame.Infrastructure;

namespace SwarmGame.Domain
{
    public class Tank : IGameObject
    {

        public float X { get; private set; }
        public float Y { get; private set; }
        public int Rotation { get; private set; }

        public Tank()
        {
            X = 0;
            Y = 0;
            Rotation = 0;
        }

        public Tank(float x, float y, int rotation)
        {
            X = 0;
            Y = 0;
            Rotation = 0;
        }

        public void Update()
        {
            if (KeyboardService.Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                X += 10;
                Rotation = 90;
            }
            else if (KeyboardService.Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                X -= 10;
                Rotation = 270;
            }
        }
    }
}
