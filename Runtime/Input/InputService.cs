using UnityEngine;
using UnityEngine.InputSystem;

public class InputService
{
    private PlayerInputAction _playerInputAction;

    public Vector2 MoveInput { get; private set; }
    // 필요한 다른 입력들도 여기에 추가 (예: JumpInput, AttackInput 등)

    public InputService()
    {
        _playerInputAction = new PlayerInputAction();
        _playerInputAction.Enable();

        _playerInputAction.InGame.Move.performed += OnMove;
        _playerInputAction.InGame.Move.canceled += OnMove;
        // 필요한 다른 액션들도 여기에 추가
    }

    public void Enable(string actionMapName)
    {
        _playerInputAction.asset.FindActionMap(actionMapName).Enable();
    }

    public void Disable(string actionMapName)
    {
        _playerInputAction.asset.FindActionMap(actionMapName).Disable();
    }

    #region Callback Methods

    private void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            MoveInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            MoveInput = Vector2.zero;
        }
    }

    // 필요한 다른 콜백 메서드들도 여기에 추가 (예: OnJump, OnAttack 등)

    #endregion
}
