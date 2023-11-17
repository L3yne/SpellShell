using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRecieveDamage : MonoBehaviour
{
    [System.Serializable]
    public class EnemyStats
    {
        public int maxHealth = 100;
        public int damage = 20;
        public float boostPercentage = 0f; // new variable for boost percentage

        private int _curHealth;
        public int curHealth
        {
            get { return _curHealth; }
            set { _curHealth = Mathf.Clamp(value, 0, maxHealth); }
        }

        public void Init()
        {
            // apply boost percentage to max health and damage
            maxHealth = Mathf.RoundToInt(maxHealth * (1 + boostPercentage / 100f));
            damage = Mathf.RoundToInt(damage * (1 + boostPercentage / 100f));

            curHealth = maxHealth;
        }
    }

    public GameObject deathEffect;
    public Transform player;

    public EnemyStats stats = new EnemyStats();

    [Header("Optional: ")]
    [SerializeField]
    private StatusIndicator statusIndicator;

    private float damageInterval = 1.5f; // The interval between each damage instance
    private float lastDamageTime; // The time of the last damage instance

    void Start()
    {
        stats.Init();

        if(statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }

    public void DealDamage(int damage)
    {
        stats.curHealth -= damage;
        if(stats.curHealth <= 0)
        {
            Die();
        }

        if(statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }

    void Die()
    {
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.5f);
        Destroy(gameObject);
    }

    void OnCollisionStay2D(Collision2D _colInfo)
    {
        Player _player = _colInfo.collider.GetComponent<Player>();
        if(_player != null && Time.time - lastDamageTime > damageInterval)
        {
            lastDamageTime = Time.time; // Update the time of the last damage instance
            _player.DamagePlayer(stats.damage);
        }
    }

    public void ResetStats()
    {
        stats.maxHealth = 100;
        stats.curHealth = stats.maxHealth;
        stats.damage = 20;

        if (statusIndicator != null)
        {
            statusIndicator.SetHealth(stats.curHealth, stats.maxHealth);
        }
    }
}