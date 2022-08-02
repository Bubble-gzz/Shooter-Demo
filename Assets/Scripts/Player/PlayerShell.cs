using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShell : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Gradient colorGradient;
    [SerializeField]
    Image HP_bar;
    [SerializeField]
    Image HP_border;
    [SerializeField]
    HeartInfo heartInfo;
    int initialMaxSolid;
    int def = 0;
    float invincibilityTimeOnHurt;
    bool invincibleOnHurt;
    [SerializeField]
    StatusPanel statusPanel;
    void Start()
    {
        initialMaxSolid = 6;
        heartInfo = new HeartInfo(initialMaxSolid,0);
        invincibilityTimeOnHurt = 0.5f;
        invincibleOnHurt = false;
        statusPanel = GameObject.Find("StatusPanel").GetComponent<StatusPanel>();
        //statusPanel = (StatusPanel)GameObject.FindObjectOfType(typeof(StatusPanel));
        if (statusPanel != null) {
            statusPanel.DrawHeartVessels(heartInfo);
        }
    }

    // Update is called once per frame
    void Update()
    {
    /*
        if (Input.GetKeyDown(KeyCode.Space)) hp -= 10;
        HP_bar.fillAmount = hp*1.0f / maxhp;
        HP_bar.color = colorGradient.Evaluate(1 - HP_bar.fillAmount);
        HP_border.color = HP_bar.color;
    */
    }
 
    public void onHurt(int damage)
    {
        if (invincibleOnHurt) return;
        invincibleOnHurt = true;
        heartInfo.Dec(damage);
        GamePlay.onPlayerHurt.Invoke(heartInfo);
        StartCoroutine(InvincibleCountDown(invincibilityTimeOnHurt));
        //Debug.Log("Hurt by " + damage.ToString());
    }
    IEnumerator InvincibleCountDown(float time)
    {
        yield return new WaitForSeconds(time);
        invincibleOnHurt = false;
    }
}
