using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackController : MonoBehaviour
{
    Player player;
    public static UnityEvent Event_Fire;
    float fireLoadTime{
        get{
            return player.attackinfo.fireLoadTime / player.myTimeScale;
        }
    }
    float bulletSpeed{
        get{
            return player.attackinfo.bulletSpeed * player.myTimeScale;
        }
    }
    GameObject bulletPrefab{
        get{
            return player.attackinfo.bulletPrefab;
        }
    }
    public float recoilForce{get{return player.attackinfo.recoilForce;}}
    public float recoilRecoverSpeed{get{return player.attackinfo.recoilRecoverSpeed;}}
    GameObject launcher, aimer;
    bool fireFreeze;


    void Awake()
    {
        Event_Fire = new UnityEvent();
    }
    void Start()
    {
        player = GamePlay.player;
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

       /* bullet = Instantiate(bulletPrefab);
        bullet.GetComponent<Bullet>().speed = bulletSpeed;
        bullet.transform.rotation = aimer.transform.rotation * Quaternion.Euler(0,0,5);

        bullet = Instantiate(bulletPrefab);
        bullet.GetComponent<Bullet>().speed = bulletSpeed;
        bullet.transform.rotation = aimer.transform.rotation * Quaternion.Euler(0,0,-5);
*/
        yield return new WaitForSeconds(fireLoadTime);
        fireFreeze = false;
    }

}
