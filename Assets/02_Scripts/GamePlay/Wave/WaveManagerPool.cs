using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

/* 오브젝트 풀을 사용하는 웨이브 매니저
 *
 * 기존 WaveManagerAwaitable 은 웨이브마다 Instantiate 로 적을 새로 만들고,
 * 적이 죽으면 Destroy 했다. -> 웨이브가 반복될수록 GC 부담이 커진다.
 *
 * 여기서는 EnemyPool(IObjectPool<Enemy>) 에서 꺼내 쓰고,
 * 적은 죽을 때 스스로 풀에 반납한다. -> Instantiate/Destroy 가 사라진다.
 */
public class WaveManagerPool : MonoBehaviour
{
    [SerializeField] private EnemyPool _enemyPool;

    [Header("Wave Settings")]
    [SerializeField] private int _totalWaves = 3;      // 총 웨이브 수
    [SerializeField] private int _enemiesPerWave = 10; // 웨이브당 적 수
    [SerializeField] private float _interval = 2f;     // 웨이브 간 대기 시간
    [SerializeField] private float _spawnRange = 20f;  // 스폰 범위 (원점 기준 +-)

    private Transform _playerTr;

    private void Start()
    {
        if (_enemyPool == null) _enemyPool = FindAnyObjectByType<EnemyPool>();

        if (_enemyPool == null)
        {
            Debug.LogError("[Pool] EnemyPool 을 찾을 수 없습니다. 씬에 EnemyPool 을 배치하세요.");
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) _playerTr = player.transform;

        StartCoroutine(RunWave());
    }

    private IEnumerator RunWave()
    {
        for (int i = 0; i < _totalWaves; i++)
        {
            SpawnWave(i);
            yield return new WaitForSeconds(_interval);
        }

        Debug.Log("[Pool] 웨이브 종료");
    }

    private void SpawnWave(int waveIndex)
    {
        for (int i = 0; i < _enemiesPerWave; i++)
        {
            Vector3 pos = new Vector3(
                Random.Range(-_spawnRange, _spawnRange),
                0f,
                Random.Range(-_spawnRange, _spawnRange));

            // Instantiate 대신 풀에서 꺼내 쓴다
            _enemyPool.Spawn(pos, _playerTr);
        }

        Debug.Log($"[Pool] 웨이브 {waveIndex} 소환 / 활성 {_enemyPool.CountActive}, 대기 {_enemyPool.CountInactive}");
    }
}
