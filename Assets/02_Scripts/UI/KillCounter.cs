using System;
using UnityEngine;

public class KillCounter : MonoBehaviour
{
    [SerializeField] private int _killCount = 0;

    private void OnEnable()
    {
        // 이벤트 구독
        // EnemyHealth.EnemyDie += () => _killCount++;
        
        // Subject에게 구독을 요청
        // EnemyDeathSubject.Instance.Subscribe(this);
        
        // EventBus 구독 요청
        EventBus.Subscribe<EnemyDieEvent>(EnemyDieHandler);
    }

    private void OnDisable()
    {
        // Subject에게 구독 해지 요청
        // EnemyDeathSubject.Instance.Unsubscribe(this);
        
        // EventBus 구독 해지 요청
        EventBus.Unsubscribe<EnemyDieEvent>(EnemyDieHandler);
    }

    // EventBus 패턴일 경우 호출
    private void EnemyDieHandler(EnemyDieEvent ctx)
    {
        _killCount++;
        Debug.Log("이벤트버스: " + _killCount + ": 스코어 리워드 " + ctx.ScoreReward);
    }


    // 옵저버 패턴 일 경우에 호출되는 메서드
    public void OnEnemyDie()
    {
        // 킬 카운트 변경
        _killCount++;
        Debug.Log(_killCount);
    }
}
