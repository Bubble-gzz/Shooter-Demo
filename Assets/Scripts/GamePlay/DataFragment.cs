using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataFragment : MonoBehaviour
{
    public int value;
    Rigidbody2D rb;
    float damping = 30;
    float attractionRate = 0.8f;
    float attractionR = 1.5f;
    bool attracted;
    public int type;
    public const int typeN = 3;
    public static int[] values = new int[]{1,5,25};
    static Sprite[] sprites = new Sprite[3];
    public static bool spriteLoaded = false;
    public static GameObject prefab;
    // Start is called before the first frame update
    public static void LoadResources()
    {
        Debug.Log("Loading DataFragment Resources...");
        sprites[0] = Utility.LoadSprite("DataFragment_1",1000);
        sprites[1] = Utility.LoadSprite("DataFragment_5",1000);
        sprites[2] = Utility.LoadSprite("DataFragment_25",1000);
        prefab = Resources.Load("Prefabs/GamePlay/DataFragment") as GameObject;
        if (prefab) Debug.Log("Success!");
        else Debug.Log("DataFragment Prefab is missing!");
        spriteLoaded = true;
    }
    void Start()
    {
        if (!spriteLoaded) LoadResources();
        rb = GetComponent<Rigidbody2D>();
        if (type < 0 || type > 2) type = 0;
        GetComponent<SpriteRenderer>().sprite = sprites[type];
        value = values[type];
        attracted = false;
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f,360f));
        //rb.velocity = new Vector3(5,5,0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = GamePlay.player.transform.position - transform.position;
        float dist = offset.magnitude;
        if (dist < attractionR) attracted = true;
        if (!attracted) {
            Vector3 velocity = rb.velocity;
            if (rb.velocity.magnitude < damping * Time.deltaTime) velocity = Vector3.zero;
            else velocity -= velocity.normalized * damping * Time.deltaTime;            
            rb.velocity = velocity;
        }
        else {
            rb.velocity = offset.normalized * Mathf.Max(5f, 20.0f / Mathf.Max(0.05f,offset.magnitude));
        }
    }
    public void Absorbed()
    {
        GamePlay.dataCoin += value;
        Destroy(gameObject);
    }
}
