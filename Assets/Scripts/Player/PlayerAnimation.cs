using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private float _direction;

    private Animator _anim;
    private SpriteRenderer _sprite;

    private Animator _swordArk;
    private SpriteRenderer _swordSprite;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        if (_anim == null ) Debug.LogError("PlayerAnimation:child: Animator is null");

        _sprite = GetComponentInChildren<SpriteRenderer>();
        if (_sprite == null) Debug.LogError("PlayerAnimation:child: Sprite is null");

        _swordArk = transform.GetChild(1).GetComponent<Animator>();
        if (_swordArk == null) Debug.LogError("PlayerAnimation::Sword_arc or Animator is null");

        _swordSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        if (_swordSprite == null) Debug.LogError("PlayerAnimation::Sword Sprite is null");

    }

    // Update is called once per frame
    void Update()
    {
        if (_direction != 0)
        {
            _swordSprite.flipY = _direction > 0 ? false : true;
        }
    }

    public void Jump()
    {
        _anim.SetTrigger("Jumps");
    }

    public void Move(float Direction)
    {
        _direction = Direction;
        _anim.SetFloat("Move",Mathf.Abs(Direction));
        if (Direction != 0)
        {
            _sprite.flipX = Direction > 0 ? false : true;
        }

    }
    public void Attack()
    {
        _anim.SetTrigger("Attack");        
    }

    public void AttackEffect()
    {

        _swordArk.SetTrigger("AttackArc");
    }

    public void PlayerDamage()
    {
        if (_anim.GetBool("IsDead"))
        {
            return;
        }
        _anim.SetTrigger("Hit");               
    }

    public void PlayerDeath()
    {
        _anim.SetBool("IsDead",true);
    }
}
