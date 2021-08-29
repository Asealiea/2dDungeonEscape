using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameManager:: Instance is null");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    #endregion

    public bool hasKeyToCastle { get; set; }


}
