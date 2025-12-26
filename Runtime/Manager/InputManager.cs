using System;
using PhikozzLibrary;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 입력 관리를 위한 매니저
/// </summary>
[RequireComponent(typeof(PlayerInput))]
public class InputManager : GenericSingleton<InputManager>
{
    #region >--------------------------------------------- fields & Properties

    // Player Input & Action Maps
    private PlayerInput _playerInput;
    private InputActionMap _playerActionMap;
    private InputActionMap _uiActionMap;

    // Action Map Cashes
    public static readonly string PLAYER_ACTION_MAP = "Player";
    public static readonly string UI_ACTION_MAP = "UI";

    // Actions Cashes
    private static readonly string MOVE_ACTION = "Move";
    private static readonly string ESC_ACTION = "ESC";

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
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        // 모든 액션맵 비활성화
        foreach (var map in _playerInput.actions.actionMaps)
            map.Disable();

        // 원하는 액션맵만 활성화 및 캐싱
        _playerActionMap = _playerInput.actions.FindActionMap(PLAYER_ACTION_MAP);
        _uiActionMap = _playerInput.actions.FindActionMap(UI_ACTION_MAP);

        _playerActionMap.Enable();

        // 이벤트 등록
        _playerActionMap.FindAction(MOVE_ACTION).performed += OnMovePerformed;
        _playerActionMap.FindAction(MOVE_ACTION).canceled += OnMoveCanceled;
        _uiActionMap.FindAction(ESC_ACTION).started += OnESCStarted;
    }

    private void OnDisable()
    {
        // 이벤트 해제
        if (_playerActionMap != null)
        {
            _playerActionMap.FindAction(MOVE_ACTION).performed -= OnMovePerformed;
            _playerActionMap.FindAction(MOVE_ACTION).canceled -= OnMoveCanceled;
        }
        if (_uiActionMap != null)
        {
            _uiActionMap.FindAction(ESC_ACTION).started -= OnESCStarted;
        }
    }
    
    #endregion

    #region >--------------------------------------------- Set

    /// <summary>
    /// 액션맵 전환
    /// </summary>
    /// <param name="actionMapName">액션맵 캐싱 이름</param>
    public void SetActionMap(string actionMapName)
    {
        if (_playerInput == null) return;

        // 기존 이벤트 해제
        _playerActionMap.asset.FindAction(MOVE_ACTION).performed -= OnMovePerformed;
        _playerActionMap.asset.FindAction(MOVE_ACTION).canceled -= OnMoveCanceled;
        _uiActionMap.asset.FindAction(ESC_ACTION).started -= OnESCStarted;

        _playerInput.SwitchCurrentActionMap(actionMapName);

        // 새 액션맵에 맞는 이벤트만 등록
        if (actionMapName == PLAYER_ACTION_MAP)
        {
            _playerActionMap.asset.FindAction(MOVE_ACTION).performed += OnMovePerformed;
            _playerActionMap.asset.FindAction(MOVE_ACTION).canceled += OnMoveCanceled;
        }
        else if (actionMapName == UI_ACTION_MAP)
        {
            _uiActionMap.asset.FindAction(ESC_ACTION).started += OnESCStarted;
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
        moveInput= Vector3.zero;
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