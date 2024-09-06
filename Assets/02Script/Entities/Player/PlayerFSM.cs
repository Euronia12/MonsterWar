using System.Collections;
using UnityEngine;

public class PlayerFSM : MonoBehaviour
{
    private PoolManager poolManager => PoolManager.Instance;
    public PlayerAnimController animController;

    [SerializeField]
    private LayerMask enemyMask;
    private WaitForSeconds coolTime;

    //확인 구역 넓이
    [SerializeField]
    private Vector2 size;
    //최종 확인 구역 좌표
    [SerializeField]
    private Vector2 pos;
    private Collider2D hit;
    public GameObject target;

    public float atkSpeed;

    // Start is called before the first frame update
    void Start()
    {
        coolTime = new WaitForSeconds(atkSpeed);
        enemyMask = LayerMask.GetMask("Enemy");
        pos = new Vector2(transform.position.x + size.x / 2, transform.position.y);
        StartCoroutine(DetectedEnemy());
    }

    IEnumerator DetectedEnemy()
    {
        while (true)
        {
            if (GameManager.Instance.isPlaying)
            {
                if (target == null)
                {
                    hit = Physics2D.OverlapBox(pos, size, 0, enemyMask);
                    if (hit != null)
                    {
                        var state = hit.GetComponent<Enemy>().state;
                        if (state != EnemyState.Death)
                        {
                            Attack();
                            yield return coolTime;
                        }
                        else
                        {
                            Idle();
                            yield return null;
                        }
                    }
                    else
                    {
                        Idle();
                        yield return null;
                        continue;
                    }
                }
                else
                {
                    Attack();
                    yield return coolTime;
                }
            }
            else
                yield return null;
        }
    }


    public void Attack()
    {
        animController.PlayAnimation(PlayerState.Attack);
    }

    public void Idle()
    {
        animController.PlayAnimation(PlayerState.Idle);
    }

    public void OnShoot()
    {
        poolManager.SpawnFromPool<Arrow>("Arrow").Init();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireCube(pos, size);
    }
}
