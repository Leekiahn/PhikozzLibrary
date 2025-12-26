using System;
using PhikozzLibrary;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 입력 관리를 위한 매니저
/// </summary>
public class InputManager : GenericSingleton<InputManager>
{
    #region >--------------------------------------------- fields & Properties

    // Player Input & Action Maps
    private PlayerInput _playerInput;

    // 한 번 입력되는 것은 이벤트로, 지속 입력되는 것은 프로퍼티로 처리
    // Properties
    public Vector2 moveInput { get; private set; }

    // Events
    public event Action OnESCAction;

    #endregion

    #region >--------------------------------------------- Unity

    protected override void Awake()
    {
        base.Awake();
        _playerInput = new PlayerInput();
    }

    /// <summary>
    /// 액션 맵 추가 및 이벤트 등록은 OnEnable에서 처리
    /// </summary>
    private void OnEnable()
    {
        _playerInput.Player.Move.performed += OnMovePerformed;
        _playerInput.Player.Move.canceled += OnMoveCanceled;
        _playerInput.UI.ESC.started += OnESCStarted;
    }

    /// <summary>
    /// 액션 맵 해제 및 이벤트 해제는 OnDisable에서 처리
    /// </summary>
    private void OnDisable()
    {
        _playerInput.Player.Move.performed -= OnMovePerformed;
        _playerInput.Player.Move.canceled -= OnMoveCanceled;
        _playerInput.UI.ESC.started -= OnESCStarted;
    }

    private void OnDestroy()
    {
        _playerInput.Dispose();
    }

    #endregion

    #region >--------------------------------------------- Set

    /// <summary>
    /// 액션맵 전환 - 외부에서 호출
    /// </summary>
    /// <param name="actionMapName">액션맵 캐싱 이름</param>
    public void SetActionMap(string actionMapName)
    {
        if (_playerInput == null) return;

        // 모든 액션맵 비활성화
        _playerInput.Player.Disable();
        _playerInput.UI.Disable();

        // 원하는 액션맵만 활성화
        switch (actionMapName)
        {
            case "Player":
                _playerInput.Player.Enable();
                break;
            case "UI":
                _playerInput.UI.Enable();
                break;
            // 필요시 다른 액션맵 추가
        }
    }

    #endregion

    #region >--------------------------------------------- On Action

    /// <summary>
    /// 지속적인 입력은 프로퍼티로 값을 갱신
    /// </summary>
    /// <param name="context">콜백 컨텍스트</param>
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        Debug.Log(moveInput);
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector3.zero;
    }

    /// <summary>
    /// 일회성 입력은 이벤트로 처리
    /// </summary>
    /// <param name="context">콜백 컨텍스트</param>
    private void OnESCStarted(InputAction.CallbackContext context)
    {
        OnESCAction?.Invoke();
    }

    #endregion
}