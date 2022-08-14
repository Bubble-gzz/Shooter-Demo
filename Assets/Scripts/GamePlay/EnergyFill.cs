using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyFill : MonoBehaviour
{
    // Start is called before the first frame update
    const float ZERO = 0.0001f;
    Player player;
    Image image;
    [SerializeField]
    Color colorA0, colorA1;
    bool isShining;
    float fadeSpeed;
    [SerializeField]
    Gradient colorB;
    float gradientProgress;
    float gradientSpeed;
    
    void Start()
    {
        player = GamePlay.player;
        image = GetComponent<Image>();
        player.energy.GainEnergy?.AddListener(Shine);
        image.color = colorA0;
        fadeSpeed = 7f;
        gradientSpeed = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = player.energy.amount / player.energy.capacity;
        if (!isShining)
        {
            if (player.energy.isFullEnergy())
            {
                gradientProgress += gradientSpeed * Time.deltaTime;
                while (gradientProgress > 1f) gradientProgress -= 1;
                image.color = colorB.Evaluate(gradientProgress);
            }
            else image.color = colorA0;
        }
    }
    void Shine()
    {
        if (isShining || player.energy.isFullEnergy()) return;
        isShining = true;
        StartCoroutine(_Shine());
    }
    IEnumerator _Shine()
    {
        image.color = colorA1;
        float percent = 0, delta = fadeSpeed * Time.deltaTime;
        while (percent < 1 - delta)
        {
            percent += delta;
            image.color = Color.Lerp(colorA1, colorA0, percent);
            yield return null;
        }
        image.color = colorA0;
        isShining = false;
    }
}
