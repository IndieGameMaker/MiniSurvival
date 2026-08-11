using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private float _fireRate = 0.5f; // 발사간격
    
    
    private float _nextFire;

    private void Update()
    {
        // 발사 쿨다운
        _nextFire += Time.deltaTime;
        if (_nextFire < _fireRate) return;
        
        // 가장 가까운 적 검색
        Transform target = FindEnemy();
        if (target == null) return;
        
        // 총알 발사
        Fire(target);
        _nextFire = 0;
    }

    private void Fire(Transform target)
    {
        // 발사 방향 계산 
        Vector3 dir = (target.position - transform.position).normalized;
        // 총알 생성 
        Instantiate(_bulletPrefab, transform.position, Quaternion.LookRotation(dir));
    }

    private Transform FindEnemy()
    {
        EnemyHealth[] enemies = FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None);

        Transform nearest = null;
        float nearestDist = 0f;
        
        foreach (var enemy in enemies)
        {
            // 거리 비교 (두좌표간의 거리) Vector3.Distance (A, B)
            float dist = (enemy.transform.position - transform.position).sqrMagnitude;
            if (dist < nearestDist)
            {
                nearestDist = dist;
                nearest = enemy.transform;
            }
        }
        
        return nearest;
    }
}
