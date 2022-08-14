using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[Serializable]
public class HeartInfo
{
    public UnityEvent<HeartInfo> onInfoChange = new UnityEvent<HeartInfo>();
    public enum HeartType
    {
        Solid,
        Fragile
    }
    
    public int solid, maxSolid;
    public int fragile;
    public HeartInfo(){solid = maxSolid = 6; fragile = 0;}
    public HeartInfo(int _maxSolid, int _fragile)
    {
        solid = maxSolid = _maxSolid; fragile = _fragile;
    }
    public void CopyFrom(HeartInfo sample)
    {
        solid = sample.solid;
        maxSolid = sample.maxSolid;
        fragile = sample.fragile;
    }
    public void Add(int hp, HeartType type)
    {
        switch(type)
        {
            case HeartType.Solid: {
                solid = Mathf.Min(solid + hp, maxSolid);
                break;
            }
            case HeartType.Fragile: {
                fragile += hp;
                break;
            }
        }
        onInfoChange?.Invoke(this);
    }
    public void Dec(int hp)
    {
        if (fragile > 0) {
            int delta = Mathf.Min(fragile, hp);
            fragile -= delta;
            hp -= delta;
        }
        if (hp<1) return ;
        solid -= hp;
        onInfoChange?.Invoke(this);
    }
    bool isDead() {
        return totalAmount() < 1;
    }
    int totalAmount() {
        return solid + fragile;
    }
}
