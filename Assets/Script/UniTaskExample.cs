using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class UniTaskExample : MonoBehaviour
{
    CancellationTokenSource _tokenSource = new CancellationTokenSource();
    private void Start()
    {
        Test(_tokenSource.Token);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _tokenSource.Cancel();
        }
    }
    private async void Test(CancellationToken token)
    {
        Debug.Log("10秒待ちます");
        try
        {
            await UniTask.Delay(10000, cancellationToken: token);
        }
        catch (Exception e)
        {
            Debug.Log("UniTaskは破棄されました。");
        }
        Debug.Log("終わり");
    }
}
