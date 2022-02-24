namespace KaynirGames.FSM
{
    public interface IState
    {
        void OnStateEnter();

        void OnStateUpdate();

        void OnStateExit();
    }
}