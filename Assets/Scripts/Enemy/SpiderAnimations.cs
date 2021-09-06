using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimations : MonoBehaviour
{

    private Spider _spider;


    private void Start()
    {
        _spider = GetComponentInParent<Spider>();
        if (_spider == null) Debug.LogError(transform.name + ":parent: Spider is null");        
    }

    public void SpitAcid()
    {
        _spider.Attack();
    }

}
