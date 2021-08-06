using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordArk : MonoBehaviour
{

    private SpriteRenderer _sprite;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        if (_sprite == null)
            Debug.LogError("SwordArk:: SpriteRenderer is null");        
    }

    public void LayerInFront()
    {
        _sprite.sortingOrder = 31;
    }
    public void LayerBehind()
    {
        _sprite.sortingOrder = 29;
    }

}
