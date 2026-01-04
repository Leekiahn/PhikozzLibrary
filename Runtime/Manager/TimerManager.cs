using System;
using System.Collections;
using UnityEngine;

namespace PhikozzLibrary.Runtime.Manager
{
    public class TimerManager : GenericSingleton<TimerManager>
    {
        #region >--------------------------------------------- Methods

        // 코루틴 시작, Coroutine 객체 반환
        public Coroutine StartTimer(float delay, Action callback, bool repeat = false)
        {
            return StartCoroutine(StartTimerCoroutine(delay, callback, repeat));
        }

        // 일정 시간 후 코루틴 중단 메서드
        // 0f일 경우 즉시 중단
        public Coroutine StopTimer(Coroutine timerCoroutine, float delayAfterStop)
        {
            return StartCoroutine(StopTimerCoroutine(timerCoroutine, delayAfterStop));
        }

        #endregion

        #region >--------------------------------------------- Coroutines

        private IEnumerator StopTimerCoroutine(Coroutine timerCoroutine, float delayAfterStop)
        {
            yield return new WaitForSeconds(delayAfterStop);
            StopCoroutine(timerCoroutine);
        }

        private IEnumerator StartTimerCoroutine(float delay, Action callback, bool repeat)
        {
            do
            {
                yield return new WaitForSeconds(delay);
                callback?.Invoke();
            } while (repeat);
        }

        #endregion
    }
}
