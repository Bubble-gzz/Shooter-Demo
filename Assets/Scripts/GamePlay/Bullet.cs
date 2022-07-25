using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    protected Rigidbody2D rb;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float range;
    [SerializeField]
    public float damage;
    protected Dictionary<string,int> colliderTags = new Dictionary<string, int>();

    float travelDistance;


    bool vanished, stopUpdate;

    protected virtual void Start()
    {
        travelDistance = 0;
        rb = GetComponent<Rigidbody2D>();
        stopUpdate = false;
        vanished = false;
        colliderTags.Add("Wall",0);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (stopUpdate) return;
        if (rb != null)
            travelDistance += rb.velocity.magnitude * Time.deltaTime;
        if (travelDistance > range) StartCoroutine(Vanish());
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (colliderTags.ContainsKey(other.tag)) 
        {
        //    Debug.Log("Hit" + other.tag);
            StartCoroutine(Explode());
        }
    }
    protected virtual IEnumerator Vanish()
    {
        vanished = true;
        Destroy(gameObject);
        yield return null;
    }
    IEnumerator Explode()
    {   
        rb.velocity = Vector2.zero;
        stopUpdate = true;
        yield return ExplodeVFX();
        vanished = true;
        Destroy(gameObject);
    }
    protected virtual IEnumerator ExplodeVFX()
    {
        yield return new WaitForSeconds(1);
    }
}
