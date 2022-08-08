using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mindless : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        maxhp = 100;
        hp = maxhp;
        value = 100;
        speed = 2;
        collisionDamage = 1;
        fragmentBlastForce = 15;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        //base.Start();
        TurnToPlayer();
    }
}
