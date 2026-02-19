using System;
using System.Collections.Generic;
using UnityEngine;

namespace PhikozzLibrary
{
    public abstract class BaseStateMachine<TState> where TState : IState
    {
        #region >---------------------------------------------- Properties & Fields

        private TState _currentState;   // 현재 상태
        private Dictionary<Type, TState> _states = new Dictionary<Type, TState>(); // 상태 캐싱

        #endregion

        #region >---------------------------------------------- Unity

        public virtual void Update()
        {
            _currentState?.Tick();
        }

        #endregion

        #region >---------------------------------------------- Methods

        /// <summary>
        /// 상태 추가
        /// </summary>
        /// <param name="state">추가할 상태</param>
        protected void AddState(TState state)
        {
            Type stateType = state.GetType();
            if (!_states.TryAdd(stateType, state))
            {
                Debug.LogError($"State {stateType.Name} already exists in state machine.");
            }
        }

        /// <summary>
        /// 상태 가져오기
        /// </summary>
        /// <typeparam name="T">가져올 상태 타입</typeparam>
        /// <returns>가져온 상태</returns>
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
    
        /// <summary>
        /// 상태 설정
        /// </summary>
        /// <typeparam name="T">설정할 상태 타입</typeparam>
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

        /// <summary>
        /// 상태 전환
        /// </summary>
        /// <param name="newState">전환할 새로운 상태</param>
        private void TransitionToState(TState newState)
        {
            if (EqualityComparer<TState>.Default.Equals(_currentState, newState))
                return;

            _currentState?.Exit();
            _currentState = newState;
            _currentState.Enter();
        }

        #endregion
    }
}
