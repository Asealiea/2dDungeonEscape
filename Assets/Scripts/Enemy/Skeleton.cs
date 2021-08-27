using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy , IDamageable
{
   

    public int Health { get; set; }

    //use this for initialization
    public override void Init()
    {
        base.Init();
        Health = base.health;
    }


    public void Damage()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            return;
        }
        Health--;
        anim.SetTrigger("Hit");
        isHit = true;
        anim.SetBool("InCombat", true);
        if (Health < 1)
        {
            Health = 0;
            anim.SetTrigger("Death");
            GameObject Dia =  Instantiate(_diamonds, transform.position, Quaternion.identity);
            Dia.GetComponent<Diamond>().SpawnDiamonds(gems);
            Destroy(this.gameObject,2.5f);
        }


    }


}
