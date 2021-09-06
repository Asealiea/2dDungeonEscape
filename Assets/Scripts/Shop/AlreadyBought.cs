using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlreadyBought : MonoBehaviour
{
    private float _timer = 1;

    private void Update()
    {
        if (_timer <= 0)
        {
            UIManager.Instance.MessageOff();
            _timer = 1;
        }
        else
        {
            _timer -= Time.deltaTime;
        }
    }



}
