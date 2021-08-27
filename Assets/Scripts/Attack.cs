using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool _canDamage = true;
    public bool hasFireSword;


    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable obj = other.GetComponent<IDamageable>();

        if (obj != null)
        {
            if (_canDamage)
            {
                if (!hasFireSword)
                    obj.Damage(1);                
                else
                    obj.Damage(3);                
                _canDamage = false;
                StartCoroutine(DamageCoolDown());
            }
        }    
    }

    IEnumerator DamageCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        _canDamage = true;
    }
}
