using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay
{
    public static GameObject playerLauncher;
    public static GameObject player;
    // Start is called before the first frame update
    public static void GenBurstParticle(GameObject prefab, Vector3 pos, Vector2 amount)
    {
        ParticleSystem particles = GameObject.Instantiate(prefab, pos, Quaternion.Euler(0,0,0)).GetComponent<ParticleSystem>();
        ParticleSystem.Burst burst = particles.emission.GetBurst(0);
        burst.count = Random.Range((int)amount.x,(int)amount.y);
        particles.emission.SetBurst(0,burst);
        particles.Play();
    }
}
