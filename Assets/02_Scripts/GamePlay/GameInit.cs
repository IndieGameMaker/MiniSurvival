using System;
using System.Threading.Tasks;
using UnityEngine;

public class GameInit : MonoBehaviour
{
    // Task.WhenAll

    private async void Start()
    {
        Debug.Log("게임 초기화 시작 ...");

        Task task1 = LoadSaveDataAsync();
        Task task2 = LoadGameConfigAsync();
        Task task3 = SetObjectPoolAsync();

        // Task.WhenAll 동시에 작업을 시작하고 비동기로 대기
        await Task.WhenAll(task1, task2, task3);

        // await LoadSaveDataAsync();
        // await SetObjectPoolAsync();
        // await LoadGameConfigAsync();

        Debug.Log("초기화 완료!");
    }

    private async Task LoadSaveDataAsync()
    {
        await Task.Delay(1000);
        Debug.Log("[Task #1] 세이브 데이터 로딩 완료");
    }

    private async Task LoadGameConfigAsync()
    {
        await Task.Delay(2000);
        Debug.Log("[Task #2] 게임 설정 데이터 로딩 완료");
    }

    private async Task SetObjectPoolAsync()
    {
        try
        {
            await Task.Delay(500);
            Debug.Log("[Task #3] 오브젝트풀링 초기화 완료");
        }
        catch (Exception e)
        {
            
        }
    }
}

/*
 * Unity 6.5 지원 Awaitable
 * 유니티 표준 비동기 기능
 * async / await 문법 사용
 * Task 의 불편한 점을 개선 (힙에 할당)
 * 반환타입 Awaitable ,  Awaitable<T>  ==> Task 대체
 * try-catch 가능
 * 프레임 단위로 대기
 * 풀링 기법을 사용 부담 적다. (재사용 가능)
 *
 * Awaitable.NextFrameAsync()   => yield return null
 * Awaitable.WaitForSecondsAsync(2f)
 * Awaitable.FixedUpdateAsync()
 * Awaitable.MainTh readAsync() => 메인스레드로 복귀
 */