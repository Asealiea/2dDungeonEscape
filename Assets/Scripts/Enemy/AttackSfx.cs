using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSfx : MonoBehaviour
{

    [SerializeField] private AudioClip _attackClip;

    public void PlayAttackSfx()
    {
        AudioManger.Instance.PlaySfx(_attackClip);
    }
}
