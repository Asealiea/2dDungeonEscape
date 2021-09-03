using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ogre : Enemy
{

    public override void Init()
    {
        base.Init();
        Health = base.health;
    }




}
