using UnityEngine;

public class HpBar : MonoBehaviour
{
    [SerializeField] private IntEventCannel OnHpChanged;

    private void OnEnable() => OnHpChanged.Register(HpChangedHandler);
    private void OnDisable() => OnHpChanged.Unregister(HpChangedHandler);
    
    private void HpChangedHandler(int hp)
    {
        Debug.Log($"법사 HP : {hp}/100");
    }
}
