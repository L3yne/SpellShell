using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster gm;
    // Start is called before the first frame update
    void Start()
    {
        if(gm==null)
        {
            gm=GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 2;
    
    public static void KillPlayer(Player player)
    {
        Destroy (player.gameObject);
    }

    public static void KillEnemy(EnemyRecieveDamage enemy)
    {
        Destroy(enemy.gameObject);
    }

}
