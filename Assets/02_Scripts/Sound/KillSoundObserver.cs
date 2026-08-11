using System;
using UnityEngine;

public class KillSoundObserver : MonoBehaviour, IEnemyDeathObserver
{
    private void OnEnable() => EnemyDeathSubject.Instance.Subscribe(this);
    private void OnDisable() => EnemyDeathSubject.Instance.Unsubscribe(this);

    public void OnEnemyDie() => Debug.Log("[사운드] 적 사망");
}
