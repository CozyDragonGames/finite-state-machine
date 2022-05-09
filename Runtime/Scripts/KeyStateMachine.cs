using System.Collections.Generic;

namespace CozyDragon.FSM
{
    public class KeyStateMachine<TStateKey>
    {
        private readonly Dictionary<TStateKey, IState> _states;

        private IState _currentState;

        public KeyStateMachine() => _states = new Dictionary<TStateKey, IState>();

        public void AddState(TStateKey stateKey, IState state)
        {
            if (_states.ContainsKey(stateKey)) return;
            _states.Add(stateKey, state);
        }

        public void SetState(TStateKey stateKey)
        {
            if (!TryGetState(stateKey, out IState state)) return;

            _currentState?.ExitState();
            _currentState = state;
            _currentState.EnterState();
        }

        public void Update() => _currentState?.UpdateState();

        private bool TryGetState(TStateKey stateKey, out IState state)
        {
            state = null;

            if (!_states.ContainsKey(stateKey)) return false;

            state = _states[stateKey];
            return true;
        }
    }
}