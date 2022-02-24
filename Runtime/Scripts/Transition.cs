using System;

namespace KaynirGames.FSM
{
    public class Transition
    {
        public IState ToState { get; }
        public Func<bool> Condition { get; }

        public Transition(IState toState, Func<bool> condition)
        {
            ToState = toState;
            Condition = condition;
        }
    }
}