using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mindless : Enemy
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        maxhp = 20;
        hp = maxhp;
        speed = 5;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Start();
        TurnToPlayer();
    }
}
