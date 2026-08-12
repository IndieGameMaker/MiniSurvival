using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

/* [async / await / Task]
 *
 * Task : "미래에 완료될 작업" 객체, private IEnumerator 코루틴메서드명();
 * async : 메서드 내부에서 await 키워드를 사용하겠다. 비동기 메서드라는 선언
 * await : "이 작업이 끝날때까지 기다린다." 스레드 블록킹 하지 않는다.
 * 예외처릭 가능
 * 반환값 리턴 가능
 * 
 * vs.
 * 
 * [Coroutine]
 *
 * - 예외처리 불가 (try ~ catch)
 * - 반환값을 리턴 불가
 */

public class WaveManagerAsync : MonoBehaviour
{
    [SerializeField] private int _totalWaves = 3;
    [SerializeField] private float _interval = 2f;
    
    private async void Start()
    {
        Debug.Log("웨이브 시작!");
        await RunWaveAsync(); // 비동기 방식 (Non Blocking)
    }

    private async Task RunWaveAsync()
    {
        for (int i = 0; i < _totalWaves; i++)
        {
            SpawnWave(i);
            
            // 인터벌 시간동안 대기 (비동기)
            // 밀리세컨드 (1000 => 1초)
            // Time.timeScale = 0 무시
            // 코루틴의 new WaitForSecond(_interval);
            await Task.Delay((int)(_interval * 1000));
        }

        Debug.Log("[Task] 모든 웨이브 종료");
    }

    private void SpawnWave(int i)
        => Debug.Log($"Spawn Wave {i}");
}
