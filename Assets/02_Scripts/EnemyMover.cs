using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private Transform _target;
    
    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
