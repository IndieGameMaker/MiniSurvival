using System.Collections.Generic;
using UnityEngine;

// 관찰자 목록을 관리
// Nofity 주체
public class EnemyDeathSubject : MonoBehaviour
{
    // 싱글턴 패턴
    public static EnemyDeathSubject Instance;
    
    // 관찰자 목록
    private readonly List<IEnemyDeathObserver> _observers = new();

    private void Awake()
    {
        // 싱글턴 할당
        Instance = this;
    }
    
    // 구독 등록
    // 관찰자가 "나도 구독하겠다" 등록하는 메서드
    public void Subscribe(IEnemyDeathObserver observer)
    {
        // 중복여부 체크
        if (!_observers.Contains(observer)) _observers.Add(observer);
    }
    
    // 구독 해제
    public void Unsubscribe(IEnemyDeathObserver observer)
    {
        _observers.Remove(observer);
    }
    
    // 적 처치시 Enemy가 호출
    // 구독자들에게 알람을 전송
    public void Notify()
    {
        for (int i = 0; i < _observers.Count; i++)
        {
            _observers[i].OnEnemyDie();
        }
    }
}
