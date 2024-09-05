using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, PoolObject
{
    private Player player => Player.Instance;
    [SerializeField]
    private Rigidbody2D rbody;
    [SerializeField]
    private CircleCollider2D coll;
    public Animator animator;

    [SerializeField]
    private Transform spawnerTr;
    public EnemyState state = EnemyState.Idle;
    public EnemyStat stat = new EnemyStat();
    public EnemyStatHandler statHandler;
    private WaitUntil detectPlayer;
    private LayerMask playerMask;

    // Start is called before the first frame update
    void Awake()
    {
        playerMask = LayerMask.GetMask("Player");
        detectPlayer = new WaitUntil(() => OnMove());
    }

    public void Init()
    {
        statHandler.Init();
        StartCoroutine(MoveToPlayer());
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

    IEnumerator MoveToPlayer()
    {
        yield return detectPlayer;
    }

    public bool OnMove()
    {
        if (Physics2D.Raycast(transform.position, Vector2.left, 1.5f, playerMask) || state == EnemyState.Death)
        {
            rbody.velocity = Vector2.zero;
            return true;
        }
        rbody.velocity = Vector2.left * stat.speed;
        return false;
    }


    private void Update()
    {
        if(stat.maxHealth > 0)
            UIManager.Instance.hpBar.fillAmount = Mathf.Max(stat.health, 0) / (float)stat.maxHealth;
        UIManager.Instance.hpBarRect.position = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position + (Vector3.up * coll.bounds.size.y));
    }
}
