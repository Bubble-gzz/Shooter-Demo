using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected const float ZERO = 0.0001f;
   // [SerializeField]
    //protected Rigidbody2D rb;
    [SerializeField]
    public float speed;
    [SerializeField]
    protected float range;
    [SerializeField]
    public float damage;
    protected Dictionary<string,int> colliderTags = new Dictionary<string, int>();

    float travelDistance;
    protected float shakeForce;
    protected SpriteRenderer renderer;
    Color color0;
    protected enum VanishType{
        FadeOut
    }
    protected VanishType vanishType;
    float vanishSpeed = 10f;
    public bool vanished;
    protected bool isVanishing;
    bool stopUpdate;
    protected Vector2 velocity;
    protected LayerMask layerMask;
    protected float weakenRate = 1.0f;
    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        color0 = renderer.color;
    }
    protected virtual void Start()
    {
        travelDistance = 0;
        //rb = GetComponent<Rigidbody2D>();
        stopUpdate = false;
        vanished = false;
        isVanishing = false;
        colliderTags.Add("Wall",0);
        layerMask |= LayerMask.GetMask("Wall");
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (stopUpdate) return;
        speed *= weakenRate;
        damage *= weakenRate;
        //Debug.Log("bullet.travelDistance:" + travelDistance);
        //if (rb != null)
        //    travelDistance += rb.velocity.magnitude * Time.deltaTime;
        travelDistance += velocity.magnitude * Time.deltaTime;
        if (travelDistance > range) StartCoroutine(Vanish());
    }
    protected void travelWithRaycast(Vector3 offset)
    {
        float dist = offset.magnitude;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, offset, dist, layerMask);
        if (hit.collider != null) {
            //Debug.Log("Raycast hit " + hit.collider.tag);
            if (dist > hit.distance) dist = hit.distance;
        }
        transform.position += offset.normalized * dist;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
//        Debug.Log("Hit" + other.tag);
        if (colliderTags.ContainsKey(other.tag)) 
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position - (Vector3)velocity.normalized*2, velocity, 5f, layerMask);
            Vector2 normal = velocity.normalized;
            if (hit.collider != null) {
                normal = hit.normal;
            }
           // Debug.Log("Hit" + other.tag);
            StartCoroutine(Explode(velocity, normal , other.tag));
        }
    }
    protected virtual IEnumerator Vanish()
    {
        if (isVanishing) yield break;
        isVanishing = true;
        if (vanishType == VanishType.FadeOut)
            yield return Vanish_FadeOut();
        vanished = true;
        Destroy(gameObject);
        yield return null;
    }
    IEnumerator Vanish_FadeOut()
    {
        float percent = 1.0f, delta = vanishSpeed * Time.deltaTime;
        while (percent > delta)
        {
            percent -= delta;
            renderer.color = Color.Lerp(color0, Color.clear, 1 - percent);
            speed *= 0.9f;
            damage *= 0.9f;
            shakeForce *= 0.9f; 
            yield return null;
        }
    }
    IEnumerator Explode(Vector3 bulletDir, Vector3 normal, string tag)
    {   
        //rb.velocity = Vector2.zero;
        velocity = Vector2.zero;
        stopUpdate = true;
        yield return new WaitForEndOfFrame();
        vanished = true;
        yield return ExplodeVFX(bulletDir, normal, tag);
        Destroy(gameObject);
    }
    protected virtual IEnumerator ExplodeVFX(Vector3 bulletDir, Vector3 normal, string tag)
    {
        if (tag != "Wall") StartCoroutine(GamePlay.mainCamera.Shake(shakeForce, 0.05f));
        yield return new WaitForSeconds(0.01f);
    }
}
