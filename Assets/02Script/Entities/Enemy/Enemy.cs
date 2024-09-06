using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, PoolObject
{
    #region 컴포넌트
    public Rigidbody2D rbody;
    public CircleCollider2D coll;
    public Animator animator;
    public SpriteRenderer sr;
    public EnemyFSM fsm;
    #endregion

    #region 적 정보
    public EnemyState state = EnemyState.Idle;
    public EnemyStat stat = new EnemyStat();
    public EnemyStatHandler statHandler;
    #endregion

    [SerializeField]
    private Transform spawnerTr;
    public LayerMask playerMask;

    // Start is called before the first frame update
    void Awake()
    {
        playerMask = LayerMask.GetMask("Player");
    }

    public void Init()
    {
        statHandler.Init();
        UIManager.Instance.curEnemy = this;
    }

    public void SetData(string rcode)
    {
        var tempStat = DataManager.Instance.enemyList.Find((obj) => obj.rcode == rcode);
        stat.rcode = tempStat.rcode;
        stat.name = tempStat.name;
        stat.health = tempStat.health;
        stat.grade = tempStat.grade;
        stat.speed = tempStat.speed;
        stat.maxHealth = tempStat.health;
    }

    public void ReturnPool()
    {
        transform.position = spawnerTr.position;
        gameObject.SetActive(false);
    }

}
