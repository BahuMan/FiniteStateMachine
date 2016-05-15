using System.Collections.Generic;
using System;

namespace cryingpants
{

    public class FiniteStateMachine<StateEnum> {

        internal class FiniteState
        {
            public StateEnum name { get { return name; } private set { name = value; } }
            public FiniteState(StateEnum s)
            {
                name = s;
                StateListeners = new List<FiniteStateHandler<StateEnum>>();
            }

            //list of all objects that will be called when this state (1) becomes current, (2) is current, or (3) stops being current state
            internal List<FiniteStateHandler<StateEnum>> StateListeners;

            //all the possible new states accessible from this current state:
            internal HashSet<FiniteState> Transitions;
        }

        private FiniteState currentState;
        private Dictionary<StateEnum, FiniteState> allStates;

        public FiniteStateMachine(StateEnum initState) {
            currentState = new FiniteState(initState);
            foreach (StateEnum s in Enum.GetValues(typeof(StateEnum)))
            {
                allStates.Add(s, new FiniteState(s));
            }
        }

        public void Maintenance()
        {
            StateEnum next = currentState.name;
            foreach (FiniteStateHandler<StateEnum> l in currentState.StateListeners)
            {
                StateEnum returned = l.OnStateUpdate(currentState.name);
                //status change requested?
                if (!returned.Equals(currentState.name))
                {
                    //if somebody else already requested a status change, we have a conflict
                    if (!next.Equals(currentState.name) && !next.Equals(returned))
                    {
                        throw new RequestedTransitionConflictException<StateEnum>("FSM: 2 different transitions requested in one update", currentState.name, next, returned);
                    }
                    next = returned;
                }
            }
            handleTransition(next);
        }

        private void handleTransition(StateEnum newState)
        {
            if (newState.Equals(currentState.name)) return;
            foreach (FiniteStateHandler<StateEnum> l in currentState.StateListeners)
            {
                l.OnStateExit(currentState.name);
            }
            currentState = allStates[newState];
            foreach (FiniteStateHandler<StateEnum> l in currentState.StateListeners)
            {
                l.OnStateEnter(currentState.name);
            }
        }

        public void AddListener(StateEnum s, FiniteStateHandler<StateEnum> l) {
            allStates[s].StateListeners.Add(l);
        }

        public bool RemoveListener(StateEnum s, FiniteStateHandler<StateEnum> l) {
            return allStates[s].StateListeners.Remove(l);
        }

        public bool AddTransition(StateEnum src, StateEnum dest) {
            return allStates[src].Transitions.Add(allStates[dest]);
        }

        public bool RemoveTransition(StateEnum src, StateEnum dest) {
            return allStates[src].Transitions.Remove(allStates[dest]);
        }

    }
}
