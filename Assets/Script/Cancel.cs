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
        // �R�s�[��n���B�i���ړn�����Ɖ����قȂ�̂��낤���j
        _token1 = _tokenSource1.Token;
        _token2 = _tokenSource2.Token;
        // ��ȏ�̗��R�ŃL�����Z���������Ȃ������ǂ�����̂��x�^�[�Ȃ̂��낤���B
        // �Ⴆ�΃_���[�W�ƃf�X�g���C��,�����̗��R�ɂ���đҋ@�̃L�����Z�����������Ȃ������B
        _tokenSource3 = CancellationTokenSource.CreateLinkedTokenSource(_token1, _token2);
        // ��L�̕��@�œ�̃g�[�N�������������g�[�N���\�[�X���쐻���邱�Ƃ��ł���B
        // �i���ƂƂȂ����g�[�N���ɂ͉�����p���Ȃ��B�Ƃ��낢�뎎���Ă݂Ďv�����B�j

        Pattern1(_tokenSource3.Token);
    }
    private void Update()
    {
        // �X�y�[�X�L�[�Œ�~���߂��o��
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // �ǂ̃g�[�N���\�[�X��Cancel���߂ł��L�����Z���ł���B
            _tokenSource3.Cancel();
        }
    }

    private async void Pattern1(CancellationToken token)
    {
        Debug.Log("�\�b�ҋ@���܂�");
        try
        {
            await UniTask.Delay(10000, cancellationToken: token);
        }
        catch (OperationCanceledException e)
        {
            Debug.LogError("�L�����Z������܂���");
            return;
        }
        Debug.Log("�\�b�o�߂��܂���");
    }
    private void Pattern2()
    {

    }
    private void Pattern3()
    {

    }
}
