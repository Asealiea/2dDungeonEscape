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
    [SerializeField] private float _jump = 5;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private BoxCollider2D _box2D;
    private bool _isDead = false;
    private PlayerAnimation _playerAnim;
    private bool _inShop = false;
    private bool _paused;
    [Header("Audio")]
    [SerializeField] AudioClip _coinPickup;
    [SerializeField] AudioClip _death;
    [SerializeField] AudioClip FlamingSwordJump;
    [SerializeField] AudioClip _healthGain;
    [SerializeField] AudioClip _hit;
    [SerializeField] AudioClip _itemCollection;
    [SerializeField] AudioClip _jumps;
    [SerializeField] AudioClip _jumpwithSword;
    [SerializeField] AudioClip _squishyCut;
    [SerializeField] AudioClip _swingSword;
    [SerializeField] AudioClip _swingFireSword;
    [SerializeField] Transform _spawnPoint;
    [SerializeField] int _lives = 4;





    public int Health { get; set; }
   
    void Start()
    {
       // QualitySettings.vSyncCount = 0;
       // Application.targetFrameRate = 30;

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
        if ((CrossPlatformInputManager.GetButtonDown("A_Button") || Input.GetAxis("Attack") == 1) && !_inShop && !_playerAnim.FinishedAttack() && !_paused)
        { 
                _playerAnim.Attack();
        }
        if (CrossPlatformInputManager.GetButtonDown("Pause_Button") || (Input.GetAxisRaw("Pause") == 1 && !_paused))
        {
            _paused = true; 
            UIManager.Instance.PauseGame();
        }
        if (CrossPlatformInputManager.GetButtonDown("Resume_Button") || (Input.GetAxisRaw("Submit") == 1 && _paused))
        {
            _paused = false;
            UIManager.Instance.ResumeGame();
        }


        //for testing the checkpoints
        if (Input.GetKeyDown(KeyCode.L))
        {
            CheckPoint();
        }
        
        
    } 


    private void Movement()
    {
        if (!_isDead && !_paused)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float h2 = CrossPlatformInputManager.GetAxis("Horizontal"); // Input.GetAxisRaw("Horizontal");

            //  if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() )
            if ((CrossPlatformInputManager.GetButtonDown("B_Button") || Input.GetAxisRaw("Jump") == 1) && IsGrounded() && !_paused && !_inShop)
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
        if (damage == 10)
        {
            _playerAnim.PlayerDeath(true);
            Health = 0;
            _isDead = true;
            UIManager.Instance.UpdateLives(Health);
            if (_lives < 1)
            {
                UIManager.Instance.RestartFromCheckPoint();

            }
            return;
        }
        
        _playerAnim.PlayerDamage();            
        Health--;   
        //*
        if (Health < 1)
        {
            _playerAnim.PlayerDeath(true);
            Health = 0;
            _isDead = true;
            
            if (_lives > 1)
            {
                UIManager.Instance.RestartFromCheckPoint();
            }
        }
       // */
        UIManager.Instance.UpdateLives(Health);
    }

    public void CheckPoint()
    {
        //UIManager.Instance.RestartFromCheckPoint();
        //show UI for the restart to checkpoint.
        //take away a life.
        _lives--;
        //take away death status
        _isDead = false;
        Health = 4;
        transform.position = _spawnPoint.position;
        UIManager.Instance.UpdateLives(Health);
        _playerAnim.PlayerDeath(false);

    }

    public void CheckPoint(Transform trans)
    {
        _spawnPoint = trans;
    }


    public void UpdateDiamonds(int ExtraDiamonds)
    {
        _diamonds += ExtraDiamonds;
        UIManager.Instance.UpdateGemUI(_diamonds);      
        //Debug.Log("picked up " + _diamonds + " diamond");
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
            case 1:
                _playerAnim.FireSword();
                GameManager.Instance.hasFlameSword = true;
                break;
            case 2:
                _jump *= 1.5f;
                GameManager.Instance.hasBootsOfFlight = true;
                break;
            case 3:
                GameManager.Instance.hasKeyToCastle = true;
                break;
            default:
                break;
        }
    }






}
