using Cysharp.Threading.Tasks;
using UnityEngine;

public class UnitaskExample : MonoBehaviour
{
    private void Start()
    {
        WaitAndPrintAsync().Forget();
    }

    private async UniTaskVoid WaitAndPrintAsync()
    {
        await UniTask.Delay(1000);
        Debug.Log("1ÃÊ Áö³²");
    }

    private async UniTask DisableAfterTimeAsync()
    {
        await UniTask.Delay(3000);
        gameObject.SetActive(false);
    }


}
