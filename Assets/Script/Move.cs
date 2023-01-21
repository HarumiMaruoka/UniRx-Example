using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Move : MonoBehaviour
{
    // InputSystem �� Action�N���X �� �t�B�[���h�ŏ������ł��Ȃ��H
    // ���̃N���X�� Editor�ł� Unity �ɂ���ăC���X�^���X�𐶐�����ׁA
    // �C���X�^���X��������, Action�N���X�̃C���X�^���X�𐶐������
    // Unity Error ���������Ă��܂��̂� Start() �� Awake() �̂Ȃ��ŃC���X�^���X��
    // ��������K�v������B
    // ���A�Ȃ�炩�̕��@�ŉ��������@��͍����邱�Ƃ�, �����s���Ă��Ȃ��̂�
    // ���Ԃ�����Ƃ��ɒ�������B
    private PlayerActionInput _inputs = null;

    private Rigidbody2D _rigidbody2D = null;

    [SerializeField]
    private float _moveSpeed = 1f;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        // �C���v�b�g�V�X�e���̃C���X�^���X���m�ۂ���
        _inputs = new PlayerActionInput();
        // �C���v�b�g�V�X�e�����N������
        _inputs.Enable();
        // �C���v�b�g�V�X�e���ɃA�N�V������o�^����
        _inputs.Player.Move.performed += (moveInput) =>
        {
            Movement(moveInput.ReadValue<Vector2>());
        };
        _inputs.Player.Move.canceled += (_) =>
        {
            Movement(Vector2.zero);
        };
    }

    private void Movement(Vector2 vector)
    {
        _rigidbody2D.velocity = vector.normalized * _moveSpeed;
    }
}
