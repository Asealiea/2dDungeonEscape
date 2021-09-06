using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private bool _canDamage = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
        IDamageable obj = other.GetComponent<IDamageable>();

        if (obj != null)
        {
            if (_canDamage)
            {
                obj.Damage(10);
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
