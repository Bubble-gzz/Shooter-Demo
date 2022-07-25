using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet_Basic : Bullet
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject launcher;
    [SerializeField]
    GameObject aimer;
    [SerializeField]
    GameObject blastPrefab;
    protected override void Start()
    {
        base.Start();
        damage = 1;
        speed = 30;
        range = 50;

        launcher = GamePlay.playerLauncher;
        aimer = launcher.transform.parent.gameObject;
        transform.position = launcher.transform.position;
 
        transform.eulerAngles = aimer.transform.eulerAngles;
        rb.velocity = Utility.Angle2Vec(aimer.transform.eulerAngles.z) * speed;

        colliderTags.Add("Enemy",0);
    }
    protected override IEnumerator ExplodeVFX()
    {
        GamePlay.GenBurstParticle(blastPrefab,transform.position,new Vector2(2,3));
        GetComponent<Renderer>().enabled = false;
        yield return new WaitForFixedUpdate();
    }
}
