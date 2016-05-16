using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace cryingpants
{

    public class FSMBehaviour: MonoBehaviour
    {

        public class FiniteState
        {
            public string name { get { return name; } internal set { name = value; } }
            public FiniteState(string s)
            {
                name = s;
                StateListeners = new List<FiniteStateHandler<string>>();
            }

            //list of all objects that will be called when this state (1) becomes current, (2) is current, or (3) stops being current state
            internal List<FiniteStateHandler<string>> StateListeners;

            //all the possible new states accessible from this current state:
            internal HashSet<FiniteState> Transitions;
        }

        private FiniteState currentState;

        public void Start()
        {
            Debug.Log("no initing done (yet)");
        }

        public void update()
        {
            string next = currentState.name;
            foreach (FiniteStateHandler<string> l in currentState.StateListeners)
            {
                string returned = l.OnStateUpdate(currentState.name);
                //status change requested?
                if (!returned.Equals(currentState.name))
                {
                    //if somebody else already requested a status change, we have a conflict
                    if (!next.Equals(currentState.name) && !next.Equals(returned))
                    {
                        throw new RequestedTransitionConflictException<string>("FSM: 2 different transitions requested in one update", currentState.name, next, returned);
                    }
                    next = returned;
                }
            }
            handleTransition(next);
        }

        private void handleTransition(string newState)
        {
            if (newState.Equals(currentState.name)) return;
            foreach (FiniteStateHandler<string> l in currentState.StateListeners)
            {
                l.OnStateExit(currentState.name);
            }
            currentState = allStates[newState];
            foreach (FiniteStateHandler<string> l in currentState.StateListeners)
            {
                l.OnStateEnter(currentState.name);
            }
        }
    }
}
