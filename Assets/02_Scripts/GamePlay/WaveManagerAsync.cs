using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

/* async / await / Task
 *
 * Task : "미래에 완료될 작업" 객체, private IEnumerator 코루틴메서드명();
 * async : 메서드 내부에서 await 키워드를 사용하겠다. 비동기 메서드라는 선언
 * await : "이 작업이 끝날때까지 기다린다." 스레드 블록킹 하지 않는다.
 */

public class WaveManagerAsync : MonoBehaviour
{
    [SerializeField] private int _totalWaves = 3;
    [SerializeField] private float _interval = 2f;
    
    private async void Start()
    {
        await RunWaveAsync(); // 비동기 방식 (Non Blocking)
        Debug.Log("웨이브 시작!");
    }

    private async Task RunWaveAsync()
    {
        
    }
    
}
