using System;
using UnityEngine;

public class KillCounter : MonoBehaviour
{
    [SerializeField] private int _killCount = 0;
    
    // EventChannel
    [SerializeField] private EnemyDieEventChannel _enemyDieEventChannel;
    

    private void OnEnable()
    {
        // 1. 이벤트 구독
        // EnemyHealth.EnemyDie += () => _killCount++;
        
        // 2. Subject에게 구독을 요청
        // EnemyDeathSubject.Instance.Subscribe(this);
        
        // 3. EventBus 구독 요청
        // EventBus.Subscribe<EnemyDieEvent>(EnemyDieHandler);
        
        // 4. SO EventChannel 구독 요청
        _enemyDieEventChannel.Register(EnemyDieHandler);
    }

    private void OnDisable()
    {
        // Subject에게 구독 해지 요청
        // EnemyDeathSubject.Instance.Unsubscribe(this);
        
        // EventBus 구독 해지 요청
        // EventBus.Unsubscribe<EnemyDieEvent>(EnemyDieHandler);
        
        // 4. SO EventChannel 구독 해지 요청
        _enemyDieEventChannel.Unregister(EnemyDieHandler);
    }

    // EventBus 패턴일 경우 호출
    private void EnemyDieHandler(EnemyDieEvent ctx)
    {
        _killCount++;
        Debug.Log("SO 이벤트버스: " + _killCount + ": 스코어 리워드 " + ctx.ScoreReward);
    }
    
    // 옵저버 패턴 일 경우에 호출되는 메서드
    public void OnEnemyDie()
    {
        // 킬 카운트 변경
        _killCount++;
        Debug.Log(_killCount);
    }
}
