using UnityEngine.UI;
using UnityEngine;

public class EnemyHealthbar : MonoBehaviour
{
    public Transform enemyHead;

    void Update()
    {
        if (enemyHead != null)
        {
            transform.position = enemyHead.position + Vector3.up * 1.0f;
            transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position, Vector3.up);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0.0f);
        }
    }
}