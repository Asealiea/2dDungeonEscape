using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    [SerializeField] private GameObject _acid;

    public int Health { get; set; }


    //use this for initialization
    public override void Init()
    {
        base.Init();
        Health = base.health;
        if (_acid == null) Debug.LogError(transform.name + ":: No acid PreFab attached");
    }

    public void Damage()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            return;
        }
        Health--;


        if (Health < 1)
        {
            Health = 0;
            anim.SetTrigger("Death");
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
        Instantiate(_acid, transform.position, Quaternion.identity);
    }


}
