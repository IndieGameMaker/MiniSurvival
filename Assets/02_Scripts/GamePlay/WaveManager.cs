using System.Collections;
using System.Threading;
using UnityEngine;

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
    
    private IEnumerator RespawnPlayer()
    {
        Debug.Log("리스폰 시작 : 3초 후에 부활");
        yield return new WaitForSeconds(3);
        // Thread.Sleep(3000); // 밀리세컨즈 3초, Blocking 방식
        Debug.Log("플레이어 리스폰 완료");
    }
    
    
}
