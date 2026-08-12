using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

/* 코루틴 (Co-routine)
 * 
 *
 * 
 */

public class WaveManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(RespawnPlayer());
    }

    private void Update()
    {
        Debug.Log(Time.time);
    }
    
    // 코루틴은 병렬처리를 위한 유니티 엔진 자체의 방법
    // 멀티스레가 아님, 싱글 스레드 에서 동작
    /*
     * StartCoroutine(RespawnPlayer())
     * Frame 1 : 코루틴 시작
     * yield return : 정지 (메인 스레드에게 제어권 넘김)
     * Frame 2,3,4,,,,,120 : 코루틴은 정지
     * Frame 121 : 3초 경과 멈춘 지점부터 다시 시작
     * 코루틴 종료
     */
    private IEnumerator RespawnPlayer()
    {
        Debug.Log("리스폰 시작 : 3초 후에 부활");
        yield return new WaitForSeconds(3);

        // yield return null; // 1. 다음 프레임까지 대기(가장 기본)
        // yield return new WaitForSeconds(3); // 2. Time.scale 영향 받아
        // yield return new WaitForSecondsRealtime(3);// Time.scale 영향 X
        // //yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        // // 3. 조건을 만족할때까지
        // yield return new WaitUntil(()=> Keyboard.current.spaceKey.wasPressedThisFrame);
        // yield return new WaitForFixedUpdate();
        // yield return StartCoroutine(다른 코루틴()); // 주의 !!!!
        // yield break; // 코루틴 즉시 종료
        
        
        // Thread.Sleep(3000); // 밀리세컨즈 3초, Blocking 방식
        Debug.Log("플레이어 리스폰 완료");
    }
    
    
}
