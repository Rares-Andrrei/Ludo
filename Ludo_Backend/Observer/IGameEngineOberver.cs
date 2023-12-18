namespace Ludo_Backend.Observer
{
    public interface IGameEngineOberver
    {
        void NotifyDiceRolled(byte diceValue);
    }
}
