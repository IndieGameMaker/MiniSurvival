using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    private int _maxHp = 100;
    private int _currentHp;
    
    [SerializeField] private IntEventCannel OnHpChanged;

    private void Awake()
    {
        _currentHp = _maxHp;
    }

    public void TakeDamage(int damage)
    {
        
    }
}
