using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private CharacterController _controller;
    [SerializeField] private Transform _playerPosition;


    [SerializeField] private float _speed = 12f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float _jumpHeight = 3f;
    [SerializeField] private float _defaultVelocity = -2f;

    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private LayerMask _groundMask;

    Vector3 velocity;
    private bool _isGrounded;

    void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);

        if(_isGrounded && velocity.y < 0)
        {
            velocity.y = _defaultVelocity;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = _playerPosition.right * x + _playerPosition.forward * z;

        _controller.Move(move * _speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && _isGrounded)
        {
            velocity.y = Mathf.Sqrt(_jumpHeight * _defaultVelocity * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        _controller.Move(velocity * Time.deltaTime);

    }
}
