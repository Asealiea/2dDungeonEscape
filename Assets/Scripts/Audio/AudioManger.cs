using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManger : MonoBehaviour
{

    #region Singleton
    private static AudioManger _instance;
    public static AudioManger Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UIManager:: Instance is null");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    #endregion


    [SerializeField] private AudioSource _sfx;

    public void PlaySfx(AudioClip CliptoPlay)
    {
        _sfx.clip = CliptoPlay;
        _sfx.Play();
    }

}
