using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private Animator _anim;
    private SpriteRenderer _sprite;

    private Animator _swordArk;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        if (_anim == null )
        {
            Debug.LogError("PlayerAnimation:child: Animator is null");
        }
        _sprite = GetComponentInChildren<SpriteRenderer>();
        if (_sprite == null)
        {
            Debug.LogError("PlayerAnimation:child: Sprite is null");
        }
        _swordArk = GameObject.Find("Sword_arc").GetComponent<Animator>();
        if (_swordArk == null)
        {
            Debug.LogError("PlayerAnimation::Sword_arc or Animator is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jump()
    {
        _anim.SetTrigger("Jumps");
    }

    public void Move(float Direction)
    {
        _anim.SetFloat("Move",Mathf.Abs(Direction));
        if (Direction != 0)
        {
            _sprite.flipX = Direction > 0 ? false : true;
        }

    }
    public void Attack()
    {
        _anim.SetTrigger("Attack");

        _swordArk.SetTrigger("AttackArc");
        
    }
}
