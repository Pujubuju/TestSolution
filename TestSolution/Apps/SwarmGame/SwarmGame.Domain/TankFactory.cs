using System;
using System.Threading;

namespace SwarmGame.Domain
{
    public class TankFactory : IGameObject
    {
        public bool SpawnContinuesly { get; set; }

        private Timer _timer;
        private readonly GameWorld _gameWorld;

        public TankFactory(GameWorld gameWorld, float x, float y)
        {
            X = x;
            Y = y;
            _gameWorld = gameWorld;
            SpawnContinuesly = true;
            _timer = new Timer(Callback);
            _timer.Change(TimeSpan.FromMilliseconds(0), TimeSpan.FromSeconds(1));
        }

        private void Callback(object state)
        {
            _gameWorld.GameObjects.Add(new Tank(X, Y, Rotation));
        }

        #region IGameObject

        public float X { get; }
        public float Y { get; }
        public int Rotation { get; }

        public void Update()
        {

        }

        #endregion IGameObject

    }
}
