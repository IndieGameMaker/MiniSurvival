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
    
    // 액션 비활성화
    private void OnDisable() => _moveAction.Disable();

    private void Update()
    {
        // 키보드 값 읽기
        Vector2 input = _moveAction.ReadValue<Vector2>();
        // 2D 입력값을 => Vector3로 변환
        Vector3 dir = new Vector3(input.x, 0, input.y);
        
        // 이동 처리 (방향벡터는 정규화)
        transform.Translate(dir.normalized * _speed * Time.deltaTime);
        
        // 바라보는 방향으로 회전
        if (dir.sqrMagnitude <= 0.001f) return;
        // 바라보는 방향의 벡터를 쿼터니언 타입으로 변환
        Quaternion target = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.RotateTowards(transform.rotation,
                                target,
                                Time.deltaTime * _turnSpeed);
    }
}
