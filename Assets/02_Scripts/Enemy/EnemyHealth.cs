using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int _hp = 100;
    
    public void TakeDamage(int damage)
    {
        // 데미지 처리 HP 차감
        _hp -= damage;

        if (_hp <= 0) Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
