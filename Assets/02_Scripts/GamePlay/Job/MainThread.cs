using UnityEngine;

public class MainThread : MonoBehaviour
{
    [SerializeField] private int _count = 1_000_000; // 100만번

    private float[] _datas; // 일반 C# 배열 - 힙에 할당, GC 대상

    private void Start()
    {
        // 배열 할당
        _datas = new float[_count];
        
        // 초기값 채우기
        for (int i = 0; i < _count; i++)
        {
            _datas[i] = i;
        }
    }

    private void Update()
    {
        // 메인스레드에서 백만번 호출 계산
        for (int i = 0; i < _count; i++)
        {
            _datas[i] = Mathf.Sqrt(_datas[i]) + Mathf.Sin(_datas[i]);
        }
        Debug.Log($"{Time.time} 계산 종료");
    }
}
