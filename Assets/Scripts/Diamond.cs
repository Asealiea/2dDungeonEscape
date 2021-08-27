using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    [SerializeField] private int _diamondsAmount = 1;
    private Vector3 _startPos;

    private void Awake()
    {
        _startPos = transform.position;
    }

    private void Update()
    {
        //moves the diamonds up and down over time.
        transform.position = _startPos + (new Vector3(0, Mathf.Sin(Time.time), 0f) / 5);
    }

    public void SpawnDiamonds( int Diamonds)
    {
        _diamondsAmount = Diamonds;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //add gems to player based off how many the monster that was killed
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.UpdateDiamonds(_diamondsAmount); //Just about to add this on in.
                Destroy(this.gameObject);
            }

        }
    }
}
