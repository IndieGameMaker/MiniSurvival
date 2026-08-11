using System;
using System.Collections.Generic;

public static class EventBus
{
    private static readonly Dictionary<Type, Delegate> _handlers = new();
    
    // 구독 등록
    public static void Subscribe<T>(Action<T> handler) where T : struct
    {
        // 키값 할당
        Type key = typeof(T); // T 타입의 정보를 키로 사용

        if (_handlers.TryGetValue(key, out var existing))
        {
            // 기존 구독자가 있으면 기존 목록에 합침
            _handlers[key] = Delegate.Combine(existing, handler);
        }
        else
        {
            _handlers[key] = handler;
        }
    }
    
    // 구독 해지
    public static void Unsubscribe<T>(Action<T> handler) where T : struct
    {
        Type key = typeof(T);

        if (!_handlers.TryGetValue(key, out var existing)) return;
        
        var removed = Delegate.Remove(existing, handler);
        if (removed == null)
        {
            // 마지막 구독자 인 경우 키 자체를 딕셔너리에서 삭제
            _handlers.Remove(key);
        }
        else
        {
            _handlers[key] = removed;
        }
    }
    
    // 발행 메서드
    public static void Publish<T>(T gameEvent) where T : struct
    {
        if (_handlers.TryGetValue(typeof(T), out var existing))
        {
            
        }
    }
}
