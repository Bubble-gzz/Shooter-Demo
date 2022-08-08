using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    enum EnergyType{
        Exhaust,
        Flexible
    }
    public float myTimeScale;
    public float energy;
    public float maxEnergy;
    float energyRate;
    public bool fullEnergy;
    public UnityEvent GainEnergy;
    EnergyType energyType;
    bool isConsumingEnergy;

    void Awake()
    {
        myTimeScale = 1f;
        energy = 0f;
        maxEnergy = 100f;
        energyRate = 1f;
        fullEnergy = false;
        isConsumingEnergy = false;
        GamePlay.player = GetComponent<Player>();
        GainEnergy = new UnityEvent();
        energyType = EnergyType.Exhaust;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) ConsumeEnergy();
    }
    public void AbsorbEnergy(float value)
    {
        value *= energyRate;
        energy += value;
        if (energy > maxEnergy) {
            energy = maxEnergy;
            fullEnergy = true;
        }
        else fullEnergy = false;
        GainEnergy?.Invoke();
    }
    void ConsumeEnergy()
    {
        if (energyType == EnergyType.Exhaust)
        {
            if (!fullEnergy || isConsumingEnergy) return ;
            isConsumingEnergy = true;
            StartCoroutine(_ConsumeEnergy());
        }
        else 
        {

        }
    }
    IEnumerator _ConsumeEnergy()
    {
        
        isConsumingEnergy = false;
        yield return null;
    }
}
