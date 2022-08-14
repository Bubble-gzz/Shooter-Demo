using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mindless : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        BeforeStart();
        maxhp = 100;
        hp = maxhp;
        value = 1000;
        speed = 2;
        collisionDamage = 1;
        fragmentBlastForce = 35;
        healthBar.offset = new Vector2(0,150);
        healthBar.scale0 = new Vector2(0.6f, 0.15f);
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        //base.Start();
        TurnToPlayer();
    }
}