using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    const float ZERO = 0.00001f;
    [SerializeField]
    public GameObject parentObject;
    RectTransform rect;
    public Vector2 offset;
    float currentHealth;
    public float targetHealth;
    public float totalHealth;
    public Vector2 scale0;
    RectTransform subRect;
    Image frontImage, backImage;
    float idleTime;
    float idleTimeThreshold;
    float fadeoutSpeed;
    float frontAlpha, backAlpha;
    Camera mainCam;
    void Start()
    {
        currentHealth = targetHealth = totalHealth;
        rect = GetComponent<RectTransform>();
        subRect = transform.Find("Front").GetComponent<RectTransform>();
        frontImage = transform.Find("Front").GetComponent<Image>();
        frontAlpha = frontImage.color.a;
        backImage = transform.Find("Back").GetComponent<Image>();
        backAlpha = backImage.color.a;

        rect.localScale = scale0;
        subRect.localScale = new Vector2(1,1);
        fadeoutSpeed = 5;
        idleTime = 0;
        idleTimeThreshold = 3f;
        mainCam = GamePlay.mainCamera.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (parentObject)
        {
            Vector2 pos = (Vector2)mainCam.WorldToScreenPoint(parentObject.transform.position) + offset;
            //Debug.Log(pos);
            pos.x = pos.x * 1.0f / Screen.width * 1920;
            pos.y = pos.y * 1.0f / Screen.height * 1080;
            rect.anchoredPosition = pos;//(Vector2)GamePlay.mainCamera.GetComponent<Camera>().WorldToScreenPoint(parentObject.transform.position) + offset;
        }
        if (Mathf.Abs(currentHealth - targetHealth) > ZERO)
        {
            idleTime = 0;
            frontImage.color = SetAlpha(frontImage.color, frontAlpha);
            backImage.color = SetAlpha(backImage.color, backAlpha);
            float gap = targetHealth - currentHealth;
            float sign = gap / Mathf.Abs(gap);
            float speed = sign * Mathf.Max(Mathf.Abs(gap) * 5, totalHealth * 1.5f);
            float pace = speed * Time.deltaTime;
            if (Mathf.Abs(pace) > Mathf.Abs(gap)) pace = gap;
            currentHealth += pace;
            subRect.localScale = new Vector2(currentHealth / totalHealth, 1);
        }
        else {
            idleTime += Time.deltaTime;
            if (idleTime > idleTimeThreshold)
            {
                frontImage.color = ChangeAlpha(frontImage.color, -fadeoutSpeed * frontAlpha * Time.deltaTime);
                backImage.color = ChangeAlpha(backImage.color, -fadeoutSpeed * backAlpha* Time.deltaTime);
            }
        }
        if (targetHealth < ZERO) Destroy(gameObject);
    }
    Color ChangeAlpha(Color color, float alpha)
    {
        Color _color = color;
        _color.a += alpha;
        if (_color.a < 0) _color.a = 0;
        return _color;
    }
    Color SetAlpha(Color color, float alpha)
    {
        Color _color = color;
        _color.a = alpha;
        return _color;
    }
}
