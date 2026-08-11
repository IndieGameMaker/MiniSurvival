using UnityEngine;

public class KillCounterView : MonoBehaviour
{
    [SerializeField] private EnemyDieEventChannel _enemyDieEventChannel;
    
    private int _killCount;

    private void OnEnable() => _enemyDieEventChannel.Register(EnemyDieHandler);
    private void OnDisable() => _enemyDieEventChannel.UnRegister(EnemyDieHandler);

    private void EnemyDieHandler(EnemyDieEvent ctx)
    {
        _killCount++;
        Debug.Log($"{_killCount} kill");
    }
}
