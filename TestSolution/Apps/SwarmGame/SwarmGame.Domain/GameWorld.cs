using System;
using System.Collections.Generic;
using System.Threading;

namespace SwarmGame.Domain
{
    public class GameWorld
    {

        private Timer _timer;
        private List<IGameObject> _gameObjects;

        public List<IGameObject> GameObjects { get { return _gameObjects; } }

        public GameWorld()
        {
            _gameObjects = new List<IGameObject>();
            _gameObjects.Add(new Tank());
            _gameObjects.Add(new TankFactory(this, 50, 50));
            _gameObjects.Add(new TankFactory(this, 400, 400));
        }

        public void Start()
        {
            _timer = new Timer(Callback);
            _timer.Change(TimeSpan.FromSeconds(1), TimeSpan.FromMilliseconds(100));
        }

        private void Callback(object state)
        {
            Update();
        }

        public void Update()
        {
            foreach (IGameObject gameObject in _gameObjects)
            {
                gameObject.Update();
            }
        }


    }
}
