using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

/* IObjectPool<T> (UnityEngine.Pool)
 *
 * Unity 가 제공하는 표준 오브젝트 풀 인터페이스.
 *   T    Get()            : 풀에서 하나 꺼낸다 (없으면 createFunc 로 새로 생성)
 *   void Release(T item)  : 풀로 반납한다
 *   void Clear()          : 풀에 대기중인 객체를 모두 정리한다
 *   int  CountActive      : 현재 사용중인 개수
 *   int  CountInactive    : 풀에서 대기중인 개수
 *
 * 구현체인 ObjectPool<T> 는 4개의 콜백을 받는다.
 *   createFunc      : 객체가 부족할 때 새로 만드는 방법        (Instantiate)
 *   actionOnGet     : 꺼낼 때 할 일                            (SetActive(true))
 *   actionOnRelease : 반납할 때 할 일                          (SetActive(false))
 *   actionOnDestroy : maxSize 를 넘겨 버려질 때 할 일          (Destroy)
 */

// Enemy 전용 오브젝트 풀
public class EnemyPool : MonoBehaviour
{
    [Header("Pool Target")]
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform _poolRoot; // 하이어라키 정리용 부모 (미지정 시 자기 자신)

    [Header("Pool Settings")]
    [SerializeField] private int _prewarmCount = 10;   // 시작 시 미리 만들어 둘 개수
    [SerializeField] private int _defaultCapacity = 20; // 내부 스택의 초기 용량
    [SerializeField] private int _maxSize = 100;        // 이 개수를 넘겨 반납되면 Destroy
    [SerializeField] private bool _collectionCheck = true; // 중복 반납 검사 (개발중엔 켜두는 것을 권장)

    // 구현체(ObjectPool<T>)로 들고 있는 이유는 아래 CountActive / CountAll 때문이다.
    // IObjectPool<T> 인터페이스가 보장하는 멤버는 Get / Release / Clear / CountInactive 뿐이고,
    // CountActive, CountAll 은 구현체인 ObjectPool<T> 에만 있다.
    private ObjectPool<Enemy> _pool;

    // 외부에는 인터페이스로만 노출한다. (사용하는 쪽은 구현체를 몰라도 된다)
    public IObjectPool<Enemy> Pool => EnsurePool();

    public int CountActive => EnsurePool().CountActive;     // 사용중(꺼내간) 개수
    public int CountInactive => EnsurePool().CountInactive; // 풀에서 대기중인 개수
    public int CountAll => EnsurePool().CountAll;           // 지금까지 만들어진 총 개수

    // 다른 스크립트의 Awake 순서와 무관하게 안전하도록 지연 생성(lazy)
    private ObjectPool<Enemy> EnsurePool() => _pool ??= CreatePool();

    private void Awake()
    {
        if (_poolRoot == null) _poolRoot = transform;

        Prewarm(_prewarmCount);
    }

    private void OnDestroy()
    {
        // 씬 종료 시 풀에 남은 객체 정리
        _pool?.Clear();
    }

    private ObjectPool<Enemy> CreatePool()
    {
        return new ObjectPool<Enemy>(
            createFunc: CreateEnemy,
            actionOnGet: OnGetEnemy,
            actionOnRelease: OnReleaseEnemy,
            actionOnDestroy: OnDestroyEnemy,
            collectionCheck: _collectionCheck,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize);
    }

    // 스폰 편의 메소드 : 꺼내기 + 상태 초기화를 한 번에
    public Enemy Spawn(Vector3 position, Transform target)
    {
        Enemy enemy = Pool.Get();
        enemy.OnSpawn(position, target);
        return enemy;
    }

    public void Release(Enemy enemy)
    {
        Pool.Release(enemy);
    }

    // ObjectPool 은 미리 채워주는 기능이 없으므로,
    // 꺼냈다가(Get) 즉시 반납(Release)하는 방식으로 직접 예열한다.
    private void Prewarm(int count)
    {
        if (count <= 0) return;

        var buffer = new List<Enemy>(count);

        for (int i = 0; i < count; i++)
        {
            buffer.Add(Pool.Get());
        }

        foreach (Enemy enemy in buffer)
        {
            Pool.Release(enemy);
        }
    }

    // ---------- ObjectPool 콜백 ----------

    private Enemy CreateEnemy()
    {
        Enemy enemy = Instantiate(_enemyPrefab, _poolRoot);
        enemy.SetPool(Pool); // 자기 자신을 반납할 수 있도록 풀을 주입
        return enemy;
    }

    private void OnGetEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
    }

    private void OnReleaseEnemy(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void OnDestroyEnemy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }
}
