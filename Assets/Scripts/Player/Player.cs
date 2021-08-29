using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour , IDamageable
{
    [SerializeField] private int _diamonds;



    [SerializeField] private Rigidbody2D _rigBody;
    [SerializeField] private float _speed = 3;
    [SerializeField] private int _health = 10;
    private bool _isDead = false;
    [SerializeField] private float _jump = 5;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private BoxCollider2D _box2D;
    private PlayerAnimation _playerAnim;
    private bool _inShop = false;


    public int Health { get; set; }
   
    void Start()
    {
        _box2D = GetComponent<BoxCollider2D>();
        if (_box2D == null) Debug.LogError("Player:: Box collider is null");

        _rigBody = GetComponent<Rigidbody2D>();
        if (_rigBody == null) Debug.LogError("Player:: RigidBody2D is null");

        _playerAnim = GetComponentInChildren<PlayerAnimation>();
        if (_playerAnim == null) Debug.LogError("Player:: Player Animation is null");

        Health = _health;
        UIManager.Instance.UpdateLives(Health);
        
    }


    void Update()
    {
        Movement();

       // if (Input.GetMouseButtonDown(0) && !_inShop && !_playerAnim.FinishedAttack())
//        if ((CrossPlatformInputManager.GetButtonDown("A_Button") || Input.GetMouseButtonDown(0)) && !_inShop && !_playerAnim.FinishedAttack())
        if (CrossPlatformInputManager.GetButtonDown("A_Button") && !_inShop && !_playerAnim.FinishedAttack())
        { 
                _playerAnim.Attack();
        } 
        
        
    } 


    private void Movement()
    {
        if (!_isDead)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float h2 = CrossPlatformInputManager.GetAxis("Horizontal"); // Input.GetAxisRaw("Horizontal");

            //  if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() )
            if ((CrossPlatformInputManager.GetButtonDown("B_Button") || Input.GetKeyDown(KeyCode.Space)) && IsGrounded())
            {
                _rigBody.velocity = new Vector2((h+h2) * _speed, _jump);
                _playerAnim.Jump();
            }
            _playerAnim.Move(h+ h2);
            _rigBody.velocity = new Vector2((h + h2) * _speed, _rigBody.velocity.y);
        }
    }



    private bool IsGrounded()
    {
        bool ground;
        //                                center of the player,  size of the bounds, rotation, direction, distance, layermask
        RaycastHit2D rchit = Physics2D.BoxCast(_box2D.bounds.center, _box2D.bounds.size, 0f, Vector2.down, 0.1f,_groundMask);
        //Debug.Log(rchit.collider);
        //Debug.DrawRay(_box2D.bounds.center, Vector2.down * (_box2D.bounds.extents.y + 0.1f), Color.red);
        return ground = rchit.collider == null ? false : true;
    }

    public void Damage(int damage)
    {     
        _playerAnim.PlayerDamage();
        
        Health--;   
        
        if (Health < 1)
        {
            _playerAnim.PlayerDeath();
            Health = 0;
            _isDead = true;
        }
        UIManager.Instance.UpdateLives(Health);
    }


    public void UpdateDiamonds(int ExtraDiamonds)
    {
        _diamonds += ExtraDiamonds;
        UIManager.Instance.UpdateGemUI(_diamonds);
        //Debug.Log("picked up " + ExtraDiamonds + " diamond");
    }

    public int DiamondsOnHand()
    {
        return _diamonds;
    }

    public void Shop()
    {
        _inShop = !_inShop;
    }

    public void ShopPurchance( int Dia, int itemID)
    {
        _diamonds -= Dia;

        switch (itemID)
        {
            case 0:
                _playerAnim.FireSword();
                break;
            case 1:
                _jump *= 1.5f;
                break;
            case 2:
                GameManager.Instance.hasKeyToCastle = true;
                break;
            default:
                break;
        }
    }






}
