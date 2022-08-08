using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GamePlay
{
    public static GameObject playerLauncher;
    public static GameObject player;
    public static UnityEvent<HeartInfo> onPlayerHurt;
    public static CameraController mainCamera;
    public static int dataCoin;
    public static GameObject floatingHealthBarPrefab;
    public static GameObject GamePlay_UI;
    // Start is called before the first frame update
    public static void GenBurstParticle_60(GameObject prefab, Vector3 pos, Quaternion rot, Vector2 amount, float alpha)
    {
        ParticleSystem particles = GameObject.Instantiate(prefab, pos, rot * Quaternion.Euler(0,0,-30)).GetComponent<ParticleSystem>();
        ParticleSystem.Burst burst = particles.emission.GetBurst(0);
        burst.count = Random.Range((int)amount.x,(int)amount.y);
        particles.emission.SetBurst(0,burst);
        particles.Play();
    }
}
