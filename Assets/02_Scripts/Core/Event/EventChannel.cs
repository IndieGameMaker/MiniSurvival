using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EventChannelSO", menuName = "MiniSurvival/EventChannel")]
public abstract class EventChannel<T> : ScriptableObject
{
    // 구독자 목록
    private Action<T> _listeners;
    // 발행
    public void Raise(T value)
    {
        _listeners?.Invoke(value);
    }
    // 구독 요청
    public void Register(Action<T> listener) => _listeners += listener;

    // 해지 요청
    public void Unregister(Action<T> listener) => _listeners -= listener;
    
    // 초기화 (메모리에 남아있을 수 있는 데이터 초기화)
    private void OnDisable() => _listeners = null;
}
