using System.Threading.Tasks;
using UnityEngine;

public class SaveDataLoader : MonoBehaviour
{
    private async void Start()
    {
        int bestScore = await LoadBestScoreAsync();
        Debug.Log($"최고 점수 : {bestScore}");
    }

    private async Task<int> LoadBestScoreAsync()
    {
        Debug.Log("최고 점수 로딩 중 ...");
        // 파일을 로딩 또는 서버에서 데이터 Query 작업
        await Task.Delay(1000);

        return 25000; // 코루틴에서 불가
    }
}
