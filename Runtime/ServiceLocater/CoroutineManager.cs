using System;
using System.Collections;
using UnityEngine;
using PhikozzLibrary;

public class CoroutineManager : GenericSingleton<CoroutineManager>
{
    #region >--------------------------------------------- Methods

    // 코루틴 시작, Coroutine 객체 반환
    public Coroutine Run(float delay, Action callback, bool repeat = false,
        Action onStart = null, Action onComplete = null,
        Action<float, float> onProgress = null)
    {
        return StartCoroutine(StartRoutine(delay, callback, repeat, onStart, onComplete, onProgress));
    }

    // 일정 시간 후 코루틴 중단 메서드
    // 0f일 경우 즉시 중단
    public void Stop(Coroutine timerCoroutine, float delayAfterStop, Action onStop = null)
    {
        StartCoroutine(StopRoutine(timerCoroutine, delayAfterStop, onStop));
    }

    #endregion

    #region >--------------------------------------------- Coroutines

    /// <summary>
    /// 코루틴 시작
    /// </summary>
    /// <param name="delay">지연 시간</param>
    /// <param name="callback">지연 후 실행할 콜백 함수</param>
    /// <param name="repeat">반복 여부</param>
    /// <param name="onStart">코루틴 시작 시 호출되는 콜백 함수</param>
    /// <param name="onComplete">코루틴 완료 시 호출되는 콜백 함수</param>
    /// <param name="onProgress">진행 상황을 나타내는 콜백 함수 (경과 시간, 진행률)</param>
    /// <returns></returns>
    private IEnumerator StartRoutine(float delay, Action callback, bool repeat,
        Action onStart, Action onComplete, Action<float, float> onProgress)
    {
        onStart?.Invoke();
        do
        {
            float elapsedTime = 0f;
            while (elapsedTime < delay)
            {
                elapsedTime += Time.deltaTime;
                float progress = Mathf.Clamp01(elapsedTime / delay);
                onProgress?.Invoke(elapsedTime, progress);
                yield return null;
            }

            callback?.Invoke();
        } while (repeat);

        onComplete?.Invoke();
    }

    /// <summary>
    /// 코루틴 중단
    /// </summary>
    /// <param name="timerCoroutine">중단할 코루틴</param>
    /// <param name="delayAfterStop">중단 전 지연 시간</param>
    /// <param name="onStop">중단 후 호출되는 콜백 함수</param>
    /// <returns></returns>
    private IEnumerator StopRoutine(Coroutine timerCoroutine, float delayAfterStop, Action onStop)
    {
        yield return new WaitForSeconds(delayAfterStop);
        StopCoroutine(timerCoroutine);
        onStop?.Invoke();
    }

    #endregion
}