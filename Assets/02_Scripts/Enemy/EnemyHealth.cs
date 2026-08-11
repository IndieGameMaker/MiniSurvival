using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int _hp = 100;
    
    // 이벤트 선언
    public static event Action EnemyDie;
    
    public void TakeDamage(int damage)
    {
        // 데미지 처리 HP 차감
        _hp -= damage;

        if (_hp <= 0) Die();
    }

    private void Die()
    {
        // 이벤트 호출(Raise)
        // EnemyDie?.Invoke();
        
        // 모든 옵저버에게 알림요청
        EnemyDeathSubject.Instance.Notify();
        Destroy(gameObject);
    }
}
