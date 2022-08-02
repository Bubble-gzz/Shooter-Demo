using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartInfo
{
    public enum HeartType
    {
        Solid,
        Fragile
    }
    public int solid, maxSolid;
    public int fragile;
    HeartInfo(){solid = maxSolid = fragile = 0;}
    public HeartInfo(int _maxSolid, int _fragile)
    {
        solid = maxSolid = _maxSolid; fragile = _fragile;
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
    }
    bool isDead() {
        return (solid < 1) && (fragile < 1);
    }
}
