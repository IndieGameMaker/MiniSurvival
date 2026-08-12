using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    // [SerializeField] private float _speed = 2.0f;
    // [SerializeField] private float _stoppingDistance = 1.0f;
    // 스크립터블 오브젝트 참조를 위한 변수
    [SerializeField] private EnemyDataSO _enemyDataSO;
    [SerializeField] private Transform _target;
    
    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void Update()
    {
        if (_target != null)
        {
            if (IsStoppingDistance()) return;
            
            // 방향 계산 : 벡터의 뺄셈 연산(A - B)
            Vector3 dir = (_target.position - transform.position).normalized;
            
            transform.position += dir * _enemyDataSO.Speed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    private bool IsStoppingDistance()
    {
        float distance = Vector3.Distance(transform.position, _target.position);
        return distance <= _enemyDataSO.StoppingDistance;
    }
}
