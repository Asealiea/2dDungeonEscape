using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
   [SerializeField] private int _diamondsAmount = 1;

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
                player.UpdateDiamonds(_diamondsAmount);
                Destroy(this.gameObject);
            }

        }
    }
}
