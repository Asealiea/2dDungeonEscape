using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy//, IDamageable
{
    [Header("Projectile")]
    [SerializeField] private GameObject _acid;
    private SpriteRenderer _rend;

   // public int Health { get; set; }


    //use this for initialization
    public override void Init()
    {
        base.Init();
        Health = base.health;
        if (_acid == null) Debug.LogError(transform.name + ":: No acid PreFab attached");
        _rend = GetComponentInChildren<SpriteRenderer>();
    }
    /*
    public void Damage(int damage)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            return;
        }
        Health-= damage;


        if (Health < 1)
        {
            isDead = true;
            Health = 0;
            anim.SetTrigger("Death");
            GameObject Dia = Instantiate(_diamonds, transform.position, Quaternion.identity);
            Dia.GetComponent<Diamond>().SpawnDiamonds(gems);
            Destroy(this.gameObject, 3);
        }

    }
    */
    public override void Damage(int damage)
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            return;
        }
        Health -= damage;


        if (Health < 1)
        {
            isDead = true;
            Health = 0;
            anim.SetTrigger("Death");
            AudioManger.Instance.PlaySfx(_death);
            GameObject Dia = Instantiate(_diamonds, transform.position, Quaternion.identity);
            Dia.GetComponent<Diamond>().SpawnDiamonds(gems);
            Destroy(this.gameObject, 3);
        }
    }

    public override void Movement()
    {
        //empty so spider doesn't move.
    }

    public override void Attack()
    {
        if (_rend.isVisible)
        {
            AudioManger.Instance.PlaySfx(_attack);
            //AudioManger.Instance.PlayAtpoint(_attack, transform);
            Instantiate(_acid, transform.position, Quaternion.identity);
        }
    }



}
