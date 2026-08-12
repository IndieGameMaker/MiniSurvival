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
        await Task.Delay(500);
        Debug.Log("[Task #3] 오브젝트풀링 초기화 완료");
    }     
}
