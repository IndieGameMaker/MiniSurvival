using System;
using UnityEngine;

public class KillCounter : MonoBehaviour
{
    [SerializeField] private int _killCount = 0;

    private void OnEnable()
    {
        // 이벤트 구독
        EnemyHealth.EnemyDie += () => _killCount++;
    }
}
