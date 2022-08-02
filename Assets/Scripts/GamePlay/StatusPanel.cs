using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StatusPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public static UnityEvent ClearUI;
    public static List<Vector2> heartUIPosition = new List<Vector2>();
    public static Vector2 pivot = new Vector2(180,-100);
    PlayerShell playerShell;
    GameObject heartVesselPrefab;
    public const string SolidHeart_Full = "SolidHeart-Full";
    public const string SolidHeart_Half = "SolidHeart-Half";
    public const string SolidHeart_Empty = "SolidHeart-Empty";
    string[] heartVesselSpriteNames = new string[]{
        "SolidHeart-Full",
        "SolidHeart-Half",
        "SolidHeart-Empty"
    };
    Dictionary<string,Sprite> heartVesselSprites = new Dictionary<string, Sprite>();
    void Awake()
    {
        ClearUI = new UnityEvent();
        heartVesselPrefab = Resources.Load("Prefabs/GamePlay/UI/HeartVessel") as GameObject;
        for (int row = 0; row < 1; row++)
            for (int col = 0; col < 12; col++)
                heartUIPosition.Add(pivot + new Vector2(col * 40, row * -100));
        foreach (string spriteName in heartVesselSpriteNames)
        {
            heartVesselSprites.Add(spriteName, Utility.LoadSprite(spriteName));
            if (heartVesselSprites[spriteName] == null) Debug.Log("[" + spriteName + ".Sprite] is missing!");
        }
        if (GamePlay.onPlayerHurt == null) GamePlay.onPlayerHurt = new UnityEvent<HeartInfo>();
        GamePlay.onPlayerHurt.AddListener(DrawHeartVessels);
    }
    void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void DrawHeartVessels(HeartInfo heartInfo)
    {
        ClearUI.Invoke();
        //Debug.Log("StatusPanel Initialize");
        if (heartVesselPrefab == null)
        {
            Debug.Log("HeartVesselPrefab is missing!");
            return ;
        }
        for (int i = 0; i < (heartInfo.maxSolid + 1) / 2; i++)
        {
            GameObject newHeartVessel = Instantiate(heartVesselPrefab,transform); //newHeartVessel.transform.SetParent(transform);
            HeartVessel heartVessel= newHeartVessel.GetComponent<HeartVessel>();
            Sprite sprite = null;
            if (heartInfo.solid < i*2 - 1) sprite = heartVesselSprites[SolidHeart_Empty];
            else if (heartInfo.solid < i*2) sprite = heartVesselSprites[SolidHeart_Half];
            else sprite = heartVesselSprites[SolidHeart_Full];
        
            heartVessel.Init(i, HeartVessel.SolidHeart, sprite);
        }
    }
}
