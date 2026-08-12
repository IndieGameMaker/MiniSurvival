using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "MiniSurvival/EnemyDataSO")]
public class EnemyDataSO : ScriptableObject
{
    // 속도 속성
    public float Speed = 2.0f;
    // 정지 거리
    public float StoppingDistance = 0.5f;
}
