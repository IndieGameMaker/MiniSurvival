using UnityEngine;
using Unity.Collections; // NatviveArray, Alloctor
using Unity.Jobs;

/*
 * IJob
 * IJobFor
 * IJobParallelFor
 */

public class SimpleJob : MonoBehaviour
{
    private struct Job : IJob
    {
        // 입력값 (전달할 값)
        [ReadOnly] public NativeArray<float> Input;
        // 반환값 
        public NativeArray<float> Result;

        public void Execute()
        {
            float sum = 0f;
            for (int i = 0; i < Input.Length; i++)
            {
                sum += Input[i];
            }

            Result[0] = sum;
        }
    }

    [SerializeField] private int _count = 1_000_000;
    private NativeArray<float> _datas;
    private struct SqrtJob : IJobFor
    {
        public NativeArray<float> Input;
        
        public void Execute(int index)
        {
            float v = Input[index];
            Input[index] = Mathf.Sqrt(v) + Mathf.Sin(v);
        }
    }
    

    private void Start()
    {
        // 데이터를 준비
        // 잡 한번 쓰는 수명
        // Allocator.Temp
        // Allocator.TempJob
        // Allocator.Persistent
        
        // 2. IJobFor
        _datas = new NativeArray<float>(_count, Allocator.Persistent);
        for (int i = 0; i < _count; i++) _datas[i] = i;
        
        
        // 1. 네이티브배열 초기환
        var input = new NativeArray<float>(10, Allocator.TempJob);
        var result = new NativeArray<float>(1, Allocator.TempJob);

        for (int i = 0; i < input.Length; i++) input[i] = i + 1;
        
        // 1. 잡 생성 - 구조체이기에 반드시 필드에 할당
        var job = new Job
        {
            Input = input,
            Result = result
        };
        
        // 2. 잡 예약 (Job Queue)
        JobHandle handle = job.Schedule();
        
        // 2-1. 메인스레드에서 작업을 진행....
        
        // 3. 완료 대기 - 끝나기를 기다린다.
        handle.Complete();
        
        // 4. 결과값 확인
        Debug.Log($"합계 = {result[0]}");
        
        // 5. 해제 (필수)
        input.Dispose();
        result.Dispose();
        
        Debug.LogError("정지");
    }

    private void Update()
    {
        var sqrtJob = new SqrtJob { Input = _datas };
        sqrtJob.Schedule(_count, default).Complete();
    }
}
