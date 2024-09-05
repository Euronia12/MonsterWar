using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimController : MonoBehaviour
{
    [SerializeField]
    private Enemy enemy;

    public void OnDie()
    {
        enemy.state = EnemyState.Idle;
        enemy.ReturnPool();
        StageManager.Instance.isStageClear = true;
    }

}
