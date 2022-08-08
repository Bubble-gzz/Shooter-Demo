using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundParticle : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer renderer;
    [SerializeField]
    Color colorA, colorB;
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        Vector3 scale = transform.localScale * Random.Range(0.5f,3.5f);
        transform.localScale = scale;
        renderer.color = Color.Lerp(colorA, colorB, Random.Range(0f, 1f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
