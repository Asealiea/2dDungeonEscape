using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool _canDamage = true;


    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable obj = other.GetComponent<IDamageable>();

        if (obj != null)
        {
            if (_canDamage)
            {
                obj.Damage();
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
