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
    ParticleSystem trail;
    Vector3 scale0;
    protected override void Start()
    {
        base.Start();
        trail = transform.Find("Trail").gameObject.GetComponent<ParticleSystem>();
        trail.Play();
        damage = 1;
        range = 10;
        shakeForce = 0.07f;

        launcher = GamePlay.playerLauncher;
        aimer = launcher.transform.parent.gameObject;
        transform.position = launcher.transform.position;
 
        transform.eulerAngles = aimer.transform.eulerAngles;
        //rb.velocity = Utility.Angle2Vec(aimer.transform.eulerAngles.z) * speed;
        velocity = Utility.Angle2Vec(aimer.transform.eulerAngles.z) * speed;
        colliderTags.Add("Enemy",0);
        scale0 = transform.localScale;
        layerMask |= LayerMask.GetMask("Enemy");
    }
    protected override void Update()
    {
        base.Update();
        //Debug.Log("bullet.velocity = " + rb.velocity.magnitude);
        travelWithRaycast((Vector3)velocity * Time.deltaTime);
        transform.localScale = new Vector3(scale0.x * (1.0f + velocity.magnitude / 20.0f), scale0.y, scale0.z);

    }
    protected override IEnumerator ExplodeVFX(Quaternion bulletRotation, string tag)
    {
        //Debug.Log("After base.ExplodeVFX");
        GamePlay.GenBurstParticle_60(blastPrefab,transform.position, bulletRotation * Quaternion.Euler(0,0,180), new Vector2(2,3));
        GetComponent<Renderer>().enabled = false;
        yield return base.ExplodeVFX(bulletRotation, tag);
    }
}
