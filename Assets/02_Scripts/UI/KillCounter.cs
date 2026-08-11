using System;
using UnityEngine;

public class KillCounter : MonoBehaviour, IEnemyDeathObserver
{
    [SerializeField] private int _killCount = 0;

    private void OnEnable()
    {
        // 이벤트 구독
        // EnemyHealth.EnemyDie += () => _killCount++;
        
        // Subject에게 구독을 요청
        EnemyDeathSubject.Instance.Subscribe(this);
    }

    private void OnDisable()
    {
        // Subject에게 구독 해지 요청
        EnemyDeathSubject.Instance.Unsubscribe(this);
    }

    public void OnEnemyDie()
    {
        // 킬 카운트 변경
        _killCount++;
        Debug.Log(_killCount);
    }
}
