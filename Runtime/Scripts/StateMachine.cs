using System;
using System.Collections.Generic;

namespace CozyDragon.FSM
{
    public class StateMachine
    {
        private static readonly List<Transition> _emptyTransitions = new List<Transition>(0);

        private readonly Dictionary<Type, List<Transition>> _transitions;
        private readonly List<Transition> _anyTransitions;
        private List<Transition> _currentTransitions;

        private IState _currentState;

        public StateMachine()
        {
            _currentTransitions = new List<Transition>();
            _anyTransitions = new List<Transition>();
            _transitions = new Dictionary<Type, List<Transition>>();
        }

        public void Update()
        {
            Transition transition = GetTransition();

            if (transition != null) SetState(transition.ToState);

            _currentState?.UpdateState();
        }

        public void SetState(IState state)
        {
            if (_currentState == state) return;

            _currentState?.ExitState();
            _currentState = state;

            if (!_transitions.TryGetValue(_currentState.GetType(), out _currentTransitions))
            {
                _currentTransitions = _emptyTransitions;
            }

            _currentState.EnterState();
        }

        public void AddTransition(IState from, IState to, Func<bool> condition)
        {
            if (!_transitions.TryGetValue(from.GetType(), out List<Transition> transitions))
            {
                transitions = new List<Transition>();
                _transitions[from.GetType()] = transitions;
            }

            transitions.Add(new Transition(to, condition));
        }

        public void AddAnyTransition(IState state, Func<bool> condition)
        {
            _anyTransitions.Add(new Transition(state, condition));
        }

        private Transition GetTransition()
        {
            foreach (Transition tr in _anyTransitions)
            {
                if (tr.Condition()) return tr;
            }

            foreach (Transition tr in _currentTransitions)
            {
                if (tr.Condition()) return tr;
            }

            return null;
        }
    }
}