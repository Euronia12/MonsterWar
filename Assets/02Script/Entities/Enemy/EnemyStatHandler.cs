using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatHandler : MonoBehaviour
{
    [SerializeField]
    private Enemy enemy;

    private void Awake()
    {
        enemy.stat.maxHealth = enemy.stat.health;
    }

    public void Init()
    {
        enemy.stat.health = enemy.stat.maxHealth;
    }

    public void OnDamaged(int damage)
    {
        enemy.stat.health -= damage;
        if (enemy.stat.health <= 0)
        {
            enemy.state = EnemyState.Death;
        }
    }
}
