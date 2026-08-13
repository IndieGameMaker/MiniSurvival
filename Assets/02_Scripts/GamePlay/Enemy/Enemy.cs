using UnityEngine;
using UnityEngine.Pool;

/* Object Pooling (오브젝트 풀링)
 *
 * Instantiate / Destroy 는 비용이 큰 작업이다.
 * - Instantiate : 메모리 할당 + 컴포넌트 초기화
 * - Destroy     : 가비지 발생 -> GC 유발 -> 프레임 드랍(스파이크)
 *
 * 풀링은 미리 만들어 둔 객체를 "껐다 켰다" 하면서 재사용한다.
 * Unity 는 UnityEngine.Pool 네임스페이스에 IObjectPool<T> 표준 인터페이스를 제공한다.
 *
 * 재사용이 핵심이므로, 꺼내 쓸 때(Get) 반드시 상태를 초기화해야 한다.
 * 초기화를 빼먹으면 "죽은 채로 살아나는 적", "체력이 0인 적" 같은 버그가 생긴다.
 */

// 풀링 대상이 되는 Enemy 본체 (프리팹 루트에 부착)
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyMover))]
public class Enemy : MonoBehaviour
{
    // 자신이 소속된 풀. 사망 시 이 풀로 스스로 반납(Release)한다.
    private IObjectPool<Enemy> _pool;

    private EnemyHealth _health;
    private EnemyMover _mover;

    private void Awake()
    {
        _health = GetComponent<EnemyHealth>();
        _mover = GetComponent<EnemyMover>();
    }

    // 풀에서 꺼내질 때(SetActive(true)) 구독, 반납될 때(SetActive(false)) 해제
    // -> 재사용 시 중복 구독으로 이벤트가 여러 번 호출되는 것을 막는다.
    private void OnEnable()
    {
        _health.Died += ReturnToPool;
    }

    private void OnDisable()
    {
        _health.Died -= ReturnToPool;
    }

    // 풀이 객체를 새로 생성(createFunc)할 때 한 번만 호출된다.
    public void SetPool(IObjectPool<Enemy> pool)
    {
        _pool = pool;
    }

    // 풀에서 꺼낸 직후 호출 : 재사용을 위한 상태 초기화
    public void OnSpawn(Vector3 position, Transform target)
    {
        transform.SetPositionAndRotation(position, Quaternion.identity);

        _health.ResetHp();      // 체력 원복 (초기화를 빼먹으면 즉사한 적이 나온다)
        _mover.SetTarget(target); // 추적 대상 재설정
    }

    // 사망 시 파괴(Destroy) 대신 풀로 반납
    public void ReturnToPool()
    {
        if (_pool != null)
        {
            _pool.Release(this);
            return;
        }

        // 풀 없이 직접 Instantiate 된 경우엔 기존처럼 파괴 (하위 호환)
        Destroy(gameObject);
    }
}
