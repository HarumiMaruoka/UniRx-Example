using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Cancel : MonoBehaviour
{
    private CancellationTokenSource _tokenSource1 = new CancellationTokenSource();
    private CancellationTokenSource _tokenSource2 = new CancellationTokenSource();
    private CancellationTokenSource _tokenSource3 = new CancellationTokenSource();

    private CancellationToken _token1 = default;
    private CancellationToken _token2 = default;
    private CancellationToken _token3 = default;

    private void Start()
    {
        // コピーを渡す。（直接渡す事と何が異なるのだろうか）
        _token1 = _tokenSource1.Token;
        _token2 = _tokenSource2.Token;
        // 二つ以上の理由でキャンセルしたくなった時どうするのがベターなのだろうか。
        // 例えばダメージとデストロイ等,複数の理由によって待機のキャンセルがしたくなった時。
        _tokenSource3 = CancellationTokenSource.CreateLinkedTokenSource(_token1, _token2);
        // 上記の方法で二つのトークンを結合したトークンソースを作製することができる。
        // （もととなったトークンには何も作用しない。といろいろ試してみて思った。）

        Pattern1(_tokenSource3.Token);
    }
    private void Update()
    {
        // スペースキーで停止命令を出す
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // どのトークンソースのCancel命令でもキャンセルできる。
            _tokenSource3.Cancel();
        }
    }

    private async void Pattern1(CancellationToken token)
    {
        Debug.Log("十秒待機します");
        try
        {
            await UniTask.Delay(10000, cancellationToken: token);
        }
        catch (OperationCanceledException e)
        {
            Debug.LogError("キャンセルされました");
            return;
        }
        Debug.Log("十秒経過しました");
    }
    private void Pattern2()
    {

    }
    private void Pattern3()
    {

    }
}
