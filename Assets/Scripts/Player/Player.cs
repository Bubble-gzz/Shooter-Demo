using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Gradient colorGradient;
    [SerializeField]
    Image HP_bar;
    [SerializeField]
    Image HP_border;
    [SerializeField]
    float hp;
    const float maxhp = 100;
    void Start()
    {
        hp = maxhp;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) hp -= 10;
        HP_bar.fillAmount = hp / maxhp;
        HP_bar.color = colorGradient.Evaluate(1 - HP_bar.fillAmount);
        HP_border.color = HP_bar.color;
    }
}
