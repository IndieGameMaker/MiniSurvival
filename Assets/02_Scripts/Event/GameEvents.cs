
// 적 사망시 전달할 데이터
public struct EnemyDieEvent
{
    public int RemainingEnemies;    // 남은 적수
    public int ScoreReward;         // 점수
}

// 플레이어 피격시 전달할 데이터
public struct PlayerDamagedEvent
{
    public int CurrentHp;
    public int Damage;
}
