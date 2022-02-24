using System;

namespace KaynirGames.FSM
{
    public class CustomState : IState
    {
        private readonly Action _onEnter;
        private readonly Action _onUpdate;
        private readonly Action _onExit;

        public CustomState(Action onEnter, Action onUpdate, Action onExit)
        {
            _onEnter = onEnter;
            _onUpdate = onUpdate;
            _onExit = onExit;
        }

        public void OnStateEnter() => _onEnter?.Invoke();

        public void OnStateUpdate() => _onUpdate?.Invoke();

        public void OnStateExit() => _onExit?.Invoke();
    }
}