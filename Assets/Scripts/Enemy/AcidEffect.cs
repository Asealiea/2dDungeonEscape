using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidEffect : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotate;
    [SerializeField] private float _destroy = 3f;


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, _rotate));
        transform.Translate(Vector3.right * _speed * Time.deltaTime) ;
        Destroy(this.gameObject, _destroy);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            IDamageable hit = other.GetComponent<IDamageable>();

            if (hit != null)
            {
                hit.Damage();
            }
        }
    }



}
