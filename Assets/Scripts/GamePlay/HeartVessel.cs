using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartVessel : MonoBehaviour
{
    // Start is called before the first frame update
    public const int SolidHeart = 1;
    public const int FragileHeart = 2;
    int heartType;
    [SerializeField]
    public int id;
    RectTransform rect;
    //Dictionary<string,Sprite> sprites = new Dictionary<string, Sprite>();
    //Dictionary<string,float> 

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    public void Init(int _id, int _heartType, Sprite sprite)
    {
        id = _id; heartType = _heartType;
        rect.anchoredPosition = StatusPanel.heartUIPosition[id];
        //Debug.Log("Rect.localPosition:" + rect.localPosition);
        //Debug.Log(sprite);
        GetComponent<Image>().sprite = sprite;
        StatusPanel.ClearUI.AddListener(SelfDestroy);
        //GamePlay.onPlayerHurt.AddListener(UpdateLook);
    }
    void SelfDestroy()
    {
        Destroy(gameObject);
    }
/*    void UpdateLook(float hp)
    {
        if (hp<)
    }
*/
}
