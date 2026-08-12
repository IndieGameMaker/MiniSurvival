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
        _currentHp = Mathf.Max(0, _currentHp - damage); // 최소값 제한
        // 이벤트 발행 요청
        OnHpChanged.Raise(_currentHp);
        Debug.Log($"법사 피격: HP:{_currentHp}");
    }
}
