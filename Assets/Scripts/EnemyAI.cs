using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float baseHealth = 100f;
    private float baseDamage = 10f;
    private float health;
    private float damage;
    private float difficultyIncrease = 0.03f;

    public float moveSpeed = 5f;

    private Transform target;

    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            target = players[0].transform;
        }

        // Apply initial difficulty increase to health and damage
        health = baseHealth * (1f + difficultyIncrease);
        damage = baseDamage * (1f + difficultyIncrease);
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Vector3 targetPosition = transform.position + direction * moveSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, targetPosition, 0.5f);
        }
    }

    public void IncreaseDifficulty()
    {
        // Increase health and damage by the difficulty increase factor
        health *= (1f + difficultyIncrease);
        damage *= (1f + difficultyIncrease);
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetDamage()
    {
        return damage;
    }
}