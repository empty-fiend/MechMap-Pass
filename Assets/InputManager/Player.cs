using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _raiseAndLowerSpeed;

    private Vector2 _direction;
    private Vector2 _rotate;

    private Vector2 _rotation;

    private PlayerInput _input;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        _rotate = _input.Player.Look.ReadValue<Vector2>();
        _direction = _input.Player.Move.ReadValue<Vector2>();

        var raiseAndLower = _input.Player.RaiseAndLower.ReadValue<float>();
        transform.position += new Vector3(0, raiseAndLower * _raiseAndLowerSpeed * Time.deltaTime);

        Look(_rotate);
        Move(_direction);
    }

    private void Move(Vector2 direction)
    {
        float scaledMoveSpeed = _moveSpeed * Time.deltaTime;

        Vector3 move = Quaternion.Euler(0, transform.eulerAngles.y, 0) * new Vector3(direction.x, 0, direction.y);
        transform.position += move * scaledMoveSpeed;
    }

    private void Look(Vector2 rotate)
    {
        float scaledRotateSpeed = _rotateSpeed * Time.deltaTime;

        _rotation.y += rotate.x * scaledRotateSpeed;
        _rotation.x = Mathf.Clamp(_rotation.x - rotate.y * scaledRotateSpeed, -90, 90);
        transform.localEulerAngles = _rotation; 
    }
}
