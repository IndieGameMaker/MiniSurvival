using System;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

// Multi-Thread 처리를 안전하게 하기위한 유니티고유 기능
// - Race Condition : 공유 객체 동시 접근
// - Dead Lock : 
// - 오류가 발생했을 때 동일한 재현 부가능

// JobSystem + Burst Compiler

public class WaveManagerAwaitable : MonoBehaviour
{
    [SerializeField] private int _totalWaves = 3;
    [SerializeField] private float _interval = 2f;

    [SerializeField] private GameObject _enemyPrefab;
    
    // 프리팹의 주소
    [SerializeField] private string _enemyAddr = "Enemy";
    
    private Transform playerTr;

    // 리소스를 한번에 로딩에 사용 / 사용후에는 핸들을 릴리즈 해야함.
    private AsyncOperationHandle _handle;
    
    private async Awaitable Start()
    {
        // 어드레서블 로딩
        _handle = Addressables.LoadAssetAsync<GameObject>(_enemyAddr);
        await _handle.Task;
        
        // 비동기 로딩
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        await RunWaveAsync();
    }

    private void OnDestroy()
    {
        if (_handle.IsValid()) 
        {
            Addressables.Release(_handle); // 로드한 에셋을 해제 (누수 방지)
        }
    }

    private async Awaitable RunWaveAsync()
    {
        // destroyCancellationToken : 정지시 자동으로 비동기 메소드를 취소
        for (int i = 0; i < _totalWaves; i++)
        {
            await SpawnWave(i);
            await Awaitable.WaitForSecondsAsync(_interval, destroyCancellationToken);
        }

        Debug.Log("[Awaitable] 웨이브 종료");
    }

    private async Awaitable SpawnWave(int waveCount)
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20));
            // var enemy = Instantiate(_enemyPrefab, pos, Quaternion.identity);
            var handle = Addressables.InstantiateAsync(_enemyAddr, pos, Quaternion.identity);
            var enemy = await handle.Task;
            enemy.GetComponent<EnemyMover>().SetTarget(playerTr);
        }
        
        Debug.Log($"[Awaitable] 웨이브 실행 {waveCount}");
    }
}