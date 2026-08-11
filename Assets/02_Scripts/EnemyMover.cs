using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private Transform _target;
    
    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void Update()
    {
        if (_target != null)
        {
            // 방향 계산 : 벡터의 뺄셈 연산(A - B)
            Vector3 dir = (_target.position - transform.position).normalized;
            
            transform.position += dir * _speed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}
