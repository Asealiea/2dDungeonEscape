using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigBody;
    [SerializeField] private float _speed = 3;
    // [SerializeField] private Vector2 _velocity;
    // [SerializeField] private Vector2 _direction;
    [SerializeField] private float _jump = 5;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private BoxCollider2D _box2D;
    private PlayerAnimation _playerAnim;






   
    void Start()
    {
        _box2D = GetComponent<BoxCollider2D>();
        if (_box2D == null)
        {
            Debug.LogError("Player:: Box collider is null");
        }
        _rigBody = GetComponent<Rigidbody2D>();
        if (_rigBody == null)
        {
            Debug.LogError("Player:: RigidBody2D is null");
        }
        _playerAnim = GetComponentInChildren<PlayerAnimation>();
        if (_playerAnim == null)
        {
            Debug.LogError("Player:: Player Animation is null");
        }
    }

    
    void Update()
    {
        Movement();

        if (Input.GetMouseButtonDown(0))
        {
            _playerAnim.Attack();
        }
    }

//#if UNITY_EDITOR_WIN
    private void Movement()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() )
        {
            _rigBody.velocity = new Vector2(h * _speed, _jump);
            //_anim.SetBool("Jump", true);
            _playerAnim.Jump();
        }
        _playerAnim.Move(h);
        _rigBody.velocity = new Vector2(h * _speed, _rigBody.velocity.y);

    }
//#endif

    private bool IsGrounded()
    {
        bool ground;
        //                                center of the player,  size of the bounds, rotation, direction, distance, layermask
        RaycastHit2D rchit = Physics2D.BoxCast(_box2D.bounds.center, _box2D.bounds.size, 0f, Vector2.down, 0.1f,_groundMask);
        //Debug.Log(rchit.collider);
        //Debug.DrawRay(_box2D.bounds.center, Vector2.down * (_box2D.bounds.extents.y + 0.1f), Color.red);
        return ground = rchit.collider == null ? false : true;
    }

   



}
