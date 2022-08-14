using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[CreateAssetMenu(menuName = "GamePlay/Player Info")]
[Serializable]
public class PlayerInfo : ScriptableObject
{


    public float myTimeScale = 1f;
    [Serializable]
    public class Energy{
        public enum EnergyType{
        Exhaust,
        Flexible
        }
        const float ZERO = 0.0001f;
        public float amount;
        public float capacity;
        public float consumeRate;
        public EnergyType energyType;
        public UnityEvent GainEnergy;
        public Energy() {
            GainEnergy = new UnityEvent();
            amount = 0;
            capacity = 100;
            consumeRate = 1;
            energyType = EnergyType.Exhaust;
        }
        public void CopyFrom(Energy sample) {
            amount = sample.amount;
            capacity = sample.capacity;
            consumeRate = sample.consumeRate;
            energyType = sample.energyType;
        }
        public void AddEnergy(float delta)
        {
            amount += delta;
            if (amount < 0) amount = 0;
            if (amount > capacity) amount = capacity;
        }
        public void AbsorbEnergy(float value)
        {
            value *= consumeRate;
            AddEnergy(value);
            GainEnergy?.Invoke();
        }
        public bool isFullEnergy() {
            return amount > (capacity - ZERO);
        }
    }
    public Energy energy;
    public HeartInfo heartInfo;
    public int def = 0;
    public float atk = 1;
    public float invincibilityTimeOnHurt = 1f;
    [Serializable]    
    public class SteerInfo{
        public float maxMoveSpeed;
        float _damping;
        public float dashForce;
        public int dashCount;
        public int maxDashCount;
        public bool isDashing;
        public float damping{
            get{
                if (isDashing) return _damping * 2;
                return _damping;
            }
            set{
                _damping = value;
            }
        }
        public float pushForce;
        public float recoilFactor;
        public float dashChargeCountdown;
        public SteerInfo() {
            maxMoveSpeed = 8;
            maxDashCount = 1;
            damping = 30;
            pushForce = 80;
            dashCount = maxDashCount;
            dashForce = 22;
            recoilFactor = 3.5f;
            dashChargeCountdown = 0.5f;
        }
        public void CopyFrom(SteerInfo sample)
        {
            maxMoveSpeed = sample.maxMoveSpeed;
            maxDashCount = sample.maxDashCount;
            damping = sample.damping;
            pushForce = sample.pushForce;
            dashCount = sample.dashCount;
            dashForce = sample.dashForce;
            recoilFactor = sample.recoilFactor;
            dashChargeCountdown = sample.dashChargeCountdown;
        }
    }
    public SteerInfo steerinfo;

    [Serializable]
    public class AttackInfo{
        public float fireLoadTime;
        public float bulletSpeed;
        public GameObject bulletPrefab;
        public float recoilForce;
        public float recoilRecoverSpeed;
        public AttackInfo()
        {
            fireLoadTime = 0.1f;
            bulletSpeed = 10f;
            recoilForce = 1.0f;
            recoilRecoverSpeed = 3f;
//            bulletPrefab = Resources.Load("Prefabs/Player/PlayerBulletBasic") as GameObject;
        }
        public void CopyFrom(AttackInfo sample)
        {
            fireLoadTime = sample.fireLoadTime;
            bulletSpeed = sample.bulletSpeed;
            recoilForce = sample.recoilForce;
            bulletPrefab = sample.bulletPrefab;
            recoilRecoverSpeed = sample.recoilRecoverSpeed;   
        }
    }
    public AttackInfo attackinfo;
}
