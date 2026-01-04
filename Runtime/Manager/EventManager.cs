using PhikozzLibrary;
using System;

public class EventManager : GenericSingleton<EventManager>
{
    #region >--------------------------------------------- Events Fields

    // 이벤트 추가
    public event Action OnCustomEvent;

    #endregion

    #region >--------------------------------------------- Events Methods

    // 이벤트 호출 메서드 추가
    public void CustomEvent()
    {
        OnCustomEvent?.Invoke();
    }

    #endregion
    
    
}
