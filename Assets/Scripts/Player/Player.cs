using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    const float ZERO = 0.0001f;
    [SerializeField]
    PlayerInfo playerinfo;
    public float myTimeScale;
    public PlayerInfo.Energy energy;
    public HeartInfo heartInfo;
    public PlayerInfo.SteerInfo steerinfo;
    public PlayerInfo.AttackInfo attackinfo;
    public int def;
    public float atk;
    public float invincibilityTimeOnHurt;

    void Awake()
    {
        InitializePlayerInfo();
        GamePlay.player = this;
        /*
        myTimeScale = 1f;
        energy = new PlayerInfo.Energy();

        heartInfo = new HeartInfo();
        steerinfo = new PlayerInfo.SteerInfo();
        attackinfo = new PlayerInfo.AttackInfo();
        */
    }
    
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.R)) ConsumeEnergy();
    }
    public void AbsorbEnergy(float amount) {
        energy.AbsorbEnergy(amount);
    }
    void InitializePlayerInfo()
    {
        energy = new PlayerInfo.Energy();
        heartInfo = new HeartInfo();
        steerinfo = new PlayerInfo.SteerInfo();
        attackinfo = new PlayerInfo.AttackInfo();
        LoadPlayerInfo();
    }
    void LoadPlayerInfo()
    {
        if (playerinfo == null) playerinfo = Resources.Load("ScriptableObjects/GamePlay/Player/PlayerInfo") as PlayerInfo;
        myTimeScale = playerinfo.myTimeScale;
        energy.CopyFrom(playerinfo.energy);
        heartInfo.CopyFrom(playerinfo.heartInfo);
        def = playerinfo.def;
        atk = playerinfo.atk;
        invincibilityTimeOnHurt = playerinfo.invincibilityTimeOnHurt;
        steerinfo.CopyFrom(playerinfo.steerinfo);
        attackinfo.CopyFrom(playerinfo.attackinfo);
    }
    void SavePlayerInfo()
    {
        if (playerinfo == null) playerinfo = Resources.Load("ScriptableObjects/GamePlay/Player/PlayerInfo") as PlayerInfo;
        playerinfo.myTimeScale = myTimeScale;
        playerinfo.energy.CopyFrom(energy);
        playerinfo.heartInfo.CopyFrom(heartInfo);
        playerinfo.def = def;
        playerinfo.atk = atk;
        playerinfo.invincibilityTimeOnHurt = invincibilityTimeOnHurt;
        playerinfo.steerinfo.CopyFrom(steerinfo);
        playerinfo.attackinfo.CopyFrom(attackinfo);
    }
}
