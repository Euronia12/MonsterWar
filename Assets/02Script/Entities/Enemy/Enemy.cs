using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player player => Player.Instance;
    [SerializeField]
    private Rigidbody2D rbody;
    public Animator animator;

    [SerializeField]
    private Transform spawnerTr;
    public EnemyState state = EnemyState.Idle;
    public EnemyStat stat = new EnemyStat();
    public EnemyStatHandler statHandler;
    private WaitUntil detectPlayer;
    private LayerMask playerMask;

    private void OnEnable()
    {
        StartCoroutine(MoveToPlayer());
    }

    // Start is called before the first frame update
    void Start()
    {
        playerMask = LayerMask.GetMask("Player");
        detectPlayer = new WaitUntil(() => OnMove());
    }

    public void Init()
    {

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
}
