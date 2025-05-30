using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField] private float _speed;
    private const float _gravity = -9.81f;
    private Vector3 _velocity;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance;
    [SerializeField] private LayerMask _ground;
    private bool _isGround;
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        Move();
        Gravity();
    }
    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        _controller.Move(move * _speed * Time.deltaTime);
    }
    private void Gravity()
    {
        _isGround = Physics.CheckSphere(_groundCheck.position, _groundDistance, _ground);
        if(_isGround && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(_groundCheck.position, _groundDistance);
    }
}
