using System;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [FormerlySerializedAs("_hp")]
    [SerializeField] private int _maxHp = 100;

    // ScriptableObject Event 할당
    [SerializeField] private EnemyDieEventChannel _enemyDieEventChannel;

    // 이벤트 선언
    // public static event Action EnemyDie;

    // 사망 알림 : Enemy 가 구독해서 오브젝트 풀로 반납한다.
    public event Action Died;

    private int _currentHp;
    private bool _isDead;

    private void Awake()
    {
        ResetHp();
    }

    // 풀에서 재사용될 때 상태를 되돌린다. (풀링의 핵심)
    public void ResetHp()
    {
        _currentHp = _maxHp;
        _isDead = false;
    }

    public void TakeDamage(int damage)
    {
        // 이미 죽은 적이 또 맞으면 Die() 가 두 번 호출되어 풀에 중복 반납된다.
        if (_isDead) return;

        // 데미지 처리 HP 차감
        _currentHp -= damage;

        if (_currentHp <= 0) Die();
    }

    private void Die()
    {
        _isDead = true;

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


        // 5. 오브젝트 풀링
        // Destroy(gameObject) 대신 사망을 알리고, Enemy 가 풀에 반납한다.
        if (Died != null)
        {
            Died.Invoke();
            return;
        }

        // 구독자가 없다면(= Enemy 컴포넌트 없이 쓰는 경우) 기존처럼 파괴
        Destroy(gameObject);
    }
}
