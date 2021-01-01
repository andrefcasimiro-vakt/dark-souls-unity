using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG
{

    public class StateMachine : MonoBehaviour
    {

        public IState currentState;

        private Dictionary<Type, List<Transition>> transitions = new Dictionary<Type, List<Transition>>();

        private List<Transition> currentTransitions = new List<Transition>();
        private List<Transition> anyTransitions = new List<Transition>();
        private List<Transition> EmptyTransitions = new List<Transition>(0);

        public void Tick()
        {
            var transition = GetTransition();
            if (transition != null)
            {
                SetState(transition.To);
            }

            currentState?.Tick();
        }

        public void SetState(IState nextState)
        {
            if (nextState == currentState) return;

            currentState?.OnExit();
            currentState = nextState;

            transitions.TryGetValue(currentState.GetType(), out currentTransitions);

            if (currentTransitions == null)
            {
                currentTransitions = EmptyTransitions;
            }

            currentState.OnEnter();
        }

        public void AddTransition(IState from, IState to, Func<bool> predicate)
        {
            // If state has no transitions yet, register them first
            if (transitions.TryGetValue(from.GetType(), out var result) == false)
            {
                result = new List<Transition>();
                transitions[from.GetType()] = result;
            }

            result.Add(new Transition(to, predicate));
        }

        public void AddAnyTransition(IState to, Func<bool> predicate)
        {
            anyTransitions.Add(new Transition(to, predicate));
        }

        private class Transition
        {
            public IState To { get; }
            public Func<bool> Condition { get; }
            
            public Transition(IState to, Func<bool> condition)
            {
                this.To = to;
                this.Condition = condition;
            }
        }

        private Transition GetTransition()
        {
            foreach (var transition in anyTransitions)
            {
                if (transition.Condition() == true)
                {
                    return transition;
                }
            }

            foreach (var transition in currentTransitions)
            {
                if (transition.Condition() == true)
                {
                    return transition;
                }
            }

            return null;
        }
    }

}
