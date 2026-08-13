using UnityEngine;

public class HeavyJob : MonoBehaviour
{
    // Main Thread ID
    [SerializeField] private int _mainThreadId;

    private async Awaitable Start()
    {
        // 스탑워치 객체 생성
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        
        // 메인 스레드 ID 추출
        _mainThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
        Debug.Log($"시작 스레드가 메인? {IsMainThread()}");
        
        stopwatch.Start();
        // 백그라운드 스레드로 전환
        await Awaitable.BackgroundThreadAsync();
        long sum = HeavyCalc();
        // GameObject, Transform, Rigidbody 접근 X
        
        // 메인스레드로 전환
        await Awaitable.MainThreadAsync();
        stopwatch.Stop();
        // transform.position = new Vector3((float)sum / 2, (float)sum / 2, 0);
        Debug.Log($"경과시간 {stopwatch.ElapsedMilliseconds} 결과값 {sum}");
    }

    private long HeavyCalc()
    {
        long result = 0;

        for (int i = 0; i < 50_000_000; i++)
        {
            result += i % 5;
        }
        return result;
    }
    
    private bool IsMainThread() 
        => System.Threading.Thread.CurrentThread.ManagedThreadId == _mainThreadId;
}
