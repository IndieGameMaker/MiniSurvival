using UnityEngine;
using Random = UnityEngine.Random;

public class WaveManagerAwaitable : MonoBehaviour
{
    [SerializeField] private int _totalWaves = 3;
    [SerializeField] private float _interval = 2f;

    [SerializeField] private GameObject _enemyPrefab;

    private Transform playerTr;
    
    private async Awaitable Start()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
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
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20));
            var enemy = Instantiate(_enemyPrefab, pos, Quaternion.identity);
            enemy.GetComponent<EnemyMover>().SetTarget(playerTr);
        }
        
        Debug.Log($"[Awaitable] 웨이브 실행 {waveCount}");
    }
}