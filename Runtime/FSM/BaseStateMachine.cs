using System;
using System.Collections.Generic;
using UnityEngine;

namespace PhikozzLibrary
{
    public abstract class BaseStateMachine<TState> where TState : IState
    {
        private TState _currentState;   // 현재 상태
        private Dictionary<Type, TState> _states = new Dictionary<Type, TState>(); // 상태 캐싱

        public virtual void Update()
        {
            _currentState?.Tick();
        }
        
        protected void AddState(TState state)
        {
            Type stateType = state.GetType();
        }

        public TState GetState<T>() where T : TState
        {
            Type stateType = typeof(T);
            if (_states.TryGetValue(stateType, out TState state))
            {
                return state;
            }
            else
            {
                Debug.LogError($"State {stateType.Name} not found in state machine.");
                return default;
            }
        }
    
        public void SetState<T>() where T : TState
        {
            Type stateType = typeof(T);
            if (_states.TryGetValue(stateType, out TState newState))
            {
                TransitionToState(newState);
            }
            else
            {
                Debug.LogError($"State {stateType.Name} not found in state machine.");
            }
        }

        private void TransitionToState(TState newState)
        {
            if (EqualityComparer<TState>.Default.Equals(_currentState, newState))
                return;

            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }
    }
}
