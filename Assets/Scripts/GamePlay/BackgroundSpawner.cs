using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    List<GameObject> particles = new List<GameObject>();
    [SerializeField]
    float left = -100, right = 100, top = 100, bottom = -100;
    [SerializeField]
    int number = 200;
    void Start()
    {
        for (int i=0; i<number; i++)
        {
            int j = Random.Range(0, particles.Count);
            GameObject newParticle = Instantiate(particles[j]);
            newParticle.transform.SetParent(transform);
            newParticle.transform.localPosition = new Vector3(Random.Range(left, right), Random.Range(bottom,top), 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
