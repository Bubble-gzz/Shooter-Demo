using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayInitialize : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        DataFragment.LoadResources();
        GamePlay.floatingHealthBarPrefab = Resources.Load("Prefabs/GamePlay/FloatingHealthBar") as GameObject;
        if (GamePlay.floatingHealthBarPrefab == null) Debug.Log("Cannot Load floatingHealthBarPrefab!");
        GamePlay.GamePlay_UI = GameObject.Find("UI");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
