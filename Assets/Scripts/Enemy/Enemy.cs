using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    const float ZERO = 0.00001f;
    protected float hp;
    protected float maxhp;
    protected int value;
    protected int collisionDamage;
    Rigidbody2D rb;
    protected float _speed;
    protected float fragmentBlastForce;
    protected float energyRate = 1.0f;
    protected float speed{
        get{
            return _speed * myTimeScale;
        }
        set{
            _speed = value;
        }
    }
    [SerializeField]
    GameObject damageParticlesPrefab;
    Vector2 healthBarScale;
    protected FloatingHealthBar healthBar;

    int flashVFX_count;
    public float myTimeScale;
    protected void BeforeStart()
    {
        myTimeScale = 1.0f;
        rb = GetComponent<Rigidbody2D>();
        healthBar = Instantiate(GamePlay.floatingHealthBarPrefab).GetComponent<FloatingHealthBar>();
    }
    protected virtual void Start()
    {
        healthBar.transform.parent = GamePlay.GamePlay_UI.transform;
        healthBar.parentObject = gameObject;
        healthBar.totalHealth = maxhp;
        //damageParticles = transform.Find("DamageParticles").GetComponent<ParticleSystem>();
        //damageParticles.Stop();
        //damageParticles.Play();
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }
    protected virtual void onHurt(float damage)
    {
        if (hp < ZERO) return ;
        hp -= damage;
        GamePlay.player.AbsorbEnergy(damage * energyRate);
        healthBar.targetHealth = hp;
        StartCoroutine(FlashVFX(5));
    /*    ParticleSystem damageParticles = Instantiate(damageParticlesPrefab,transform.position,Quaternion.Euler(0,0,0)).GetComponent<ParticleSystem>();
        ParticleSystem.Burst burst = damageParticles.emission.GetBurst(0);
        burst.count = Random.Range(4,6);
        damageParticles.emission.SetBurst(0,burst);
        damageParticles.Play();
    */
        if (hp < ZERO)
            StartCoroutine(Vanish());
    }
    IEnumerator FlashVFX(int frames)
    {
        flashVFX_count ++;
    //    Debug.Log(GetComponent<Renderer>().material.GetFloat("Hit_VFX"));
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
    
    protected IEnumerator Vanish()
    {
        GenerateDataFragment();
        Destroy(gameObject);
        yield break;
    }
    void GenerateDataFragment()
    {
        int restValue = value;
        while (restValue > 0)
        {
            int n = DataFragment.typeN - 1;
            while (DataFragment.values[n] > restValue && n > 0) n--;
            int i = Random.Range(0, n + 1);
            if (DataFragment.values[i] > restValue) continue;
            restValue -= DataFragment.values[i];
           // Debug.Log("random test" + Random.Range(-5.0f,5.0f));
            GameObject fragment = Instantiate(DataFragment.prefab, transform.position, Quaternion.identity);
            //Debug.Log("fragment parent test:" + fragment.transform.parent.tag);
            fragment.GetComponent<DataFragment>().type = i;
            fragment.GetComponent<Rigidbody2D>().velocity =  new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f)).normalized * Random.Range(3f,fragmentBlastForce);
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
       // Debug.Log("Enemy: Hit by " + other.gameObject.tag); 
        if (other.tag == "PlayerBullet")
        {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            if (!bullet.vanished)
                onHurt(bullet.damage);
        }
    }
    protected virtual void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.tag == "PlayerShell")
        {
            other.collider.gameObject.GetComponent<PlayerShell>().onHurt(collisionDamage);
        }
    }
    float Rand(float min, float max)
    {
        float sign = 1.0f;
        if (Random.Range(1,3) == 1) sign *= -1;
        return Random.Range(min, max) * sign;
    }
}
