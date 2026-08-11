using System;
using UnityEngine;

public abstract class EventChannel<T> : ScriptableObject
{
    // 구독자
    private Action<T> _listener;
    
    // 발행
    public void Raise(T value)
    {
        _listener?.Invoke(value);
    }
    
    // 구독
    public void Register(Action<T> listener) => _listener += listener;
    
    // 해지
    public void UnRegister(Action<T> listener) => _listener -= listener;

    private void OnDisable() => _listener = null;
}
