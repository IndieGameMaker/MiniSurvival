using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHp = 100;
    
    // 이벤트 채널
    [SerializeField] private IntEventChannel OnPlayerHpChanged;
    
    private int _hp;

    private void Awake()
    {
        _hp = _maxHp;
        OnPlayerHpChanged.Raise(_hp);
    }
    
    public void TakeDamage(int damage)
    {
        _hp = Mathf.Max(0, _hp - damage);
        OnPlayerHpChanged.Raise(_hp);
        Debug.Log($"법사 피격: HP = {_hp}");
    }
}
