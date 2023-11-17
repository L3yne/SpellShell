using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [System.Serializable]
    public class PlayerStats
    {
        public int maxHealth = 100;

        private int _curHealth;
        public int curHealth
        {
            get {return _curHealth;}
            set {_curHealth = Mathf.Clamp(value, 0, maxHealth);}
        }

        public void Init()
        {
            curHealth = maxHealth;
        }
        
    }

    public PlayerStats stats = new PlayerStats();

    [SerializeField]
    private StatusIndicator statusIndicator;

    void Start()
    {
        stats.Init();

        if(statusIndicator == null)
        {
            Debug.LogError("No status indicator referenced on Player");
        }
        else
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }

    public GameObject player;
    public GameObject deathEffect;

    public void DamagePlayer (int damage)
    {
        stats.curHealth -= damage;
        if (stats.curHealth <= 0)
        {
            GameMaster.KillPlayer(this);
        }

        statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
    }

    public void HealPlayer(int amount)
    {
        stats.curHealth += amount;
        if (stats.curHealth > stats.maxHealth)
        {
            stats.curHealth = stats.maxHealth;
        }
        statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
    }
}
