using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Move Settings")]
    [SerializeField] private float _speed = 5.0f;
    [Range(90f, 1440f)]
    [SerializeField] private float _turnSpeed = 720.0f;
    
    private InputAction _moveAction;

    private void OnEnable()
    {
        // Move 액션 검색 및 할당
        _moveAction = InputSystem.actions.FindAction("Move");
        // 액션 활성화
        _moveAction.Enable();
    }
}
