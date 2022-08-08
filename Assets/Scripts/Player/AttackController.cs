using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackController : MonoBehaviour
{
    public static UnityEvent Event_Fire;
    float _fireLoadTime;
    GameObject launcher, aimer;
    float fireLoadTime{
        get{
            return _fireLoadTime / core.myTimeScale;
        }
        set{
            _fireLoadTime = value;
        }
    }
    float _bulletSpeed;
    float bulletSpeed{
        get{
            return _bulletSpeed * core.myTimeScale;
        }
        set{
            _bulletSpeed = value;
        }
    }
    bool fireFreeze;
    [SerializeField]
    GameObject bulletPrefab;
    Player core;
    public static float recoilForce;
    public static float recoilRecoverSpeed;
    void Awake()
    {
        Event_Fire = new UnityEvent();
        recoilForce = 1.0f;
        recoilRecoverSpeed = 3f;
    }
    void Start()
    {
        fireLoadTime = 0.1f;
        bulletSpeed = 100;
        core = GameObject.Find("Player").GetComponent<Player>();
        fireFreeze = false;
        
        launcher = GamePlay.playerLauncher;
        aimer = launcher.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        FireCheck();
    }
    void FireCheck()
    {
        if (Input.GetMouseButton(0))
        {
            if (!fireFreeze)
            {
                Event_Fire.Invoke();
                StartCoroutine(Fire());
            }
        }
    }

    IEnumerator Fire()
    {
    //    Debug.Log("Fire");
        if (bulletPrefab == null) yield break;
        fireFreeze = true;
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.GetComponent<Bullet>().speed = bulletSpeed;
        bullet.transform.eulerAngles = aimer.transform.eulerAngles;

        bullet = Instantiate(bulletPrefab);
        bullet.GetComponent<Bullet>().speed = bulletSpeed;
        bullet.transform.rotation = aimer.transform.rotation * Quaternion.Euler(0,0,5);

        bullet = Instantiate(bulletPrefab);
        bullet.GetComponent<Bullet>().speed = bulletSpeed;
        bullet.transform.rotation = aimer.transform.rotation * Quaternion.Euler(0,0,-5);

        yield return new WaitForSeconds(fireLoadTime);
        fireFreeze = false;
    }

}
