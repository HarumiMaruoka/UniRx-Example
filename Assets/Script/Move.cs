using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Move : MonoBehaviour
{
    // InputSystem の Actionクラス は フィールドで初期化できない？
    // このクラスは Editorでは Unity によってインスタンスを生成する為、
    // インスタンス生成時に, Actionクラスのインスタンスを生成すると
    // Unity Error が発生してしまうので Start() や Awake() のなかでインスタンスを
    // 生成する必要がある。
    // 又、なんらかの方法で回避する方法を模索することは, 未だ行っていないので
    // 時間があるときに調査する。
    private PlayerActionInput _inputs = null;

    private Rigidbody2D _rigidbody2D = null;

    [SerializeField]
    private float _moveSpeed = 1f;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        // インプットシステムのインスタンスを確保する
        _inputs = new PlayerActionInput();
        // インプットシステムを起動する
        _inputs.Enable();
        // インプットシステムにアクションを登録する
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
