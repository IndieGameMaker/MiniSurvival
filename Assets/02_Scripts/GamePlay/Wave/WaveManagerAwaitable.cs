using UnityEngine;

public class WaveManagerAwaitable : MonoBehaviour
{
    [SerializeField] private int _totalWaves = 3;
    [SerializeField] private float _interval = 2f;

    private async Awaitable Start()
    {
        await RunWaveAsync();
    }

    private async Awaitable RunWaveAsync()
    {
        // destroyCancellationToken : 정지시 자동으로 비동기 메소드를 취소
        for (int i = 0; i < _totalWaves; i++)
        {
            SpawnWave(i);
            await Awaitable.WaitForSecondsAsync(_interval, destroyCancellationToken);
        }
        
        Debug.Log("[Awaitable] 웨이브 종료");
    }

    private void SpawnWave(int waveCount) 
        => Debug.Log($"[Awaitable] 웨이브 실행 {waveCount}");
}
