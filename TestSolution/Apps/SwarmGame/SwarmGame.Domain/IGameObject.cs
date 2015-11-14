namespace SwarmGame.Domain
{
    public interface IGameObject
    {

        float X { get; }
        float Y { get; }
        int Rotation { get; }
        void Update();
    }
}