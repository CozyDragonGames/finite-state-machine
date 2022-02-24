using System.Collections.Generic;

namespace KaynirGames.FSM
{
    public class KeyStateMachine<TStateKey>
    {
        private readonly Dictionary<TStateKey, IState> _states;

        private IState _currentState;

        public KeyStateMachine()
        {
            _states = new Dictionary<TStateKey, IState>();
        }

        public void AddState(TStateKey stateKey, IState state)
        {
            if (!_states.ContainsKey(stateKey))
            {
                _states.Add(stateKey, state);
            }
        }

        public void SetState(TStateKey stateKey)
        {
            if (!TryGetState(stateKey, out IState state)) return;

            _currentState?.OnStateExit();
            _currentState = state;

            _currentState.OnStateEnter();
        }

        public void Update() => _currentState?.OnStateUpdate();

        private bool TryGetState(TStateKey stateKey, out IState state)
        {
            if (_states.ContainsKey(stateKey))
            {
                state = _states[stateKey];
                return true;
            }

            state = null;
            return false;
        }
    }
}