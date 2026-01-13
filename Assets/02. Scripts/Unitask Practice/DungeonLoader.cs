using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

public class DungeonLoader : MonoBehaviour
{
    private void Start()
    {
        LoadDungeonAsync().Forget();
    }

    private async UniTaskVoid LoadDungeonAsync()
    { 
        Debug.Log("로딩 시작...");

        // 이렇게 하면 안된다.
        // WhenAll에 묶인 비동기 메서드가 완료되기 전에 프로그램이 종료된다면, 완료되지 않은 비동기 메서드는 좀비 Task가 되기 때문이다.
        // await UniTask.WhenAll(LoadMap(), LoadMonsters(), LoadBGM());

        // WhenAll을 사용할 땐, 프로그램 종료에 대비하여 동일한 취소토큰으로 묶어주자.
        // try-catch를 사용하지 않아도 UniTask가 자동으로 예외를 처리해주긴 하지만,
        // Unity 콘솔 상에서 로그를 통해 확인하고 싶다면 try-catch로 감싸주자.
        CancellationToken cancelToken = this.GetCancellationTokenOnDestroy();
        try
        {
            await UniTask.WhenAll(LoadMap(cancelToken), LoadMonsters(cancelToken), LoadBGM(cancelToken));
        }
        catch (OperationCanceledException)
        {
            Debug.Log("플레이어가 창을 닫아버림");
        }
        finally
        {
            // 딱히 요구사항이 없어서 비워놓음
        }
    }

    private async UniTask LoadMap(CancellationToken token = default)
    {
        await UniTask.Delay(2000, cancellationToken: token);
        Debug.Log("맵 로딩 완료");
    }

    private async UniTask LoadMonsters(CancellationToken token = default)
    {
        await UniTask.Delay(3000, cancellationToken: token);
        Debug.Log("몬스터 로딩 완료");
    }

    private async UniTask LoadBGM(CancellationToken token = default)
    {
        await UniTask.Delay(1000, cancellationToken: token);
        Debug.Log("BGM 로딩 완료");
    }
}