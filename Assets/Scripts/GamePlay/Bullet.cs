using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
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

    public bool vanished;
    bool stopUpdate;
    protected Vector2 velocity;
    protected LayerMask layerMask;
    protected virtual void Start()
    {
        travelDistance = 0;
        //rb = GetComponent<Rigidbody2D>();
        stopUpdate = false;
        vanished = false;
        colliderTags.Add("Wall",0);
        layerMask |= LayerMask.GetMask("Wall");
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (stopUpdate) return;
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
           // Debug.Log("Hit" + other.tag);
            StartCoroutine(Explode(transform.rotation, other.tag));
        }
    }
    protected virtual IEnumerator Vanish()
    {
        vanished = true;
        Destroy(gameObject);
        yield return null;
    }
    IEnumerator Explode(Quaternion bulletRotation, string tag)
    {   
        //rb.velocity = Vector2.zero;
        velocity = Vector2.zero;
        stopUpdate = true;
        yield return new WaitForEndOfFrame();
        vanished = true;
        yield return ExplodeVFX(bulletRotation, tag);
        Destroy(gameObject);
    }
    protected virtual IEnumerator ExplodeVFX(Quaternion bulletRotation, string tag)
    {
        if (tag != "Wall") StartCoroutine(GamePlay.mainCamera.Shake(shakeForce, 0.05f));
        yield return new WaitForSeconds(0.01f);
    }
}
