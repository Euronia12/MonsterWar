using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatHandler : MonoBehaviour
{
    [SerializeField]
    private Enemy enemy;
    public bool isDead;
    public void Init()
    {

    }

    public void OnDamaged(int damage)
    {
        enemy.stat.health -= damage;
        if (enemy.stat.health <= 0)
        {
            isDead = true;
        }
    }
}
