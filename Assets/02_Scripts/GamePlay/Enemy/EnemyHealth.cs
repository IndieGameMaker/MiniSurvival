using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int _hp = 100;
    
    // ScriptableObject Event 할당
    [SerializeField] private EnemyDieEventChannel _enemyDieEventChannel;
    
    // 이벤트 선언
    // public static event Action EnemyDie;
    
    public void TakeDamage(int damage)
    {
        // 데미지 처리 HP 차감
        _hp -= damage;

        if (_hp <= 0) Die();
    }

    private void Die()
    {
        // 1. 이벤트 호출(Raise)
        // EnemyDie?.Invoke();
        
        // 2. 모든 옵저버에게 알림요청
        // EnemyDeathSubject.Instance.Notify();
        
        // 3. C# Native EventBus Pattern
        // EventBus.Publish(new EnemyDieEvent
        // {
        //     RemainingEnemies = 10,
        //     ScoreReward = 100
        // });
        
        // 4. ScriptableObject EventChannel(EventBus)
        // 발행자 (Subject, Publisher)
        _enemyDieEventChannel.Raise(new EnemyDieEvent
        {
            RemainingEnemies = 10,
            ScoreReward = 200
        });
        
        
        Destroy(gameObject);
    }
}
