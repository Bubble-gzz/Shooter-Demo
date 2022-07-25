using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    protected float hp;
    protected float maxhp;
    Rigidbody2D rb;
    protected float speed;
    [SerializeField]
    GameObject damageParticlesPrefab;

    int flashVFX_count;
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //damageParticles = transform.Find("DamageParticles").GetComponent<ParticleSystem>();
        //damageParticles.Stop();
        //damageParticles.Play();
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }
    protected void onHurt(float damage)
    {
        hp -= damage;
        StartCoroutine(FlashVFX(5));
    /*    ParticleSystem damageParticles = Instantiate(damageParticlesPrefab,transform.position,Quaternion.Euler(0,0,0)).GetComponent<ParticleSystem>();
        ParticleSystem.Burst burst = damageParticles.emission.GetBurst(0);
        burst.count = Random.Range(4,6);
        damageParticles.emission.SetBurst(0,burst);
        damageParticles.Play();
    */
        if (hp < 0)
            Vanish();
    }
    IEnumerator FlashVFX(int frames)
    {
        flashVFX_count ++;
        Debug.Log(GetComponent<Renderer>().material.GetFloat("Hit_VFX"));
    //    GetComponent<Renderer>().material.EnableKeyword("Hit_VFX");
        GetComponent<Renderer>().material.SetFloat("Hit_VFX",1.0f);
        for (int i = 0; i < frames; i++)
            yield return new WaitForFixedUpdate();
        flashVFX_count --;
        if (flashVFX_count == 0)
            GetComponent<Renderer>().material.SetFloat("Hit_VFX",0.0f);
    
    }
    protected void TurnToPlayer()
    {
        float aimDir = Utility.Vec2Angle(GamePlay.player.transform.position - transform.position);
        transform.eulerAngles = new Vector3(0,0,aimDir);
        rb.velocity = Utility.Angle2Vec(transform.eulerAngles.z) * speed;
    }
    
    protected void Vanish()
    {
        Destroy(gameObject);
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
       // Debug.Log("Enemy: Hit by " + other.gameObject.tag); 
        if (other.tag == "PlayerAttack")
        {
            //Debug.Log("Enemy: Hit by PlayerAttack");
            onHurt(other.gameObject.GetComponent<Bullet>().damage);
        }
    }
}
