using System.Collections;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private float _respawnDelay = 3f; // 부활 대기 시간
    [SerializeField] private float _godModeTime = 2f; // 무적 모드 시간

    [SerializeField] private bool _isGodMode; // 무적 여부

    private WaitForSeconds _wait;
    
    private void Start()
    {
        _wait = new WaitForSeconds(_respawnDelay);
    }
    
    public void BeginRespawn()
    {
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        Debug.Log("주인공 사망 - 부활 대기중...");
        
        // 3초 대기
        yield return _wait;
        Debug.Log("주인공 부활");
        // 무적모드 진입 요청
        yield return StartCoroutine(GodMode());
    }
    
    // 무적 모드 코루틴
    private IEnumerator GodMode()
    {
        _isGodMode = true;
        Debug.Log("무적 상태 진입...");
        yield return new WaitForSeconds(2.0f);

        _isGodMode = false;
        Debug.Log("무적 상태 해제...");
    }
}
