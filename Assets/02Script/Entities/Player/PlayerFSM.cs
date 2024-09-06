using System.Collections;
using UnityEngine;

public class PlayerFSM : MonoBehaviour
{
    [SerializeField]
    private Vector2 size;
    [SerializeField]
    private Vector2 pos;

    [SerializeField]
    private LayerMask enemyMask;
    private WaitForSeconds coolTime;

    private PoolManager poolManager => PoolManager.Instance;
    public GameObject target;
    private Collider2D hit;
    public float atkSpeed;

    public PlayerAnimController animController;
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
                        yield return null;
                    }
                    else
                    {
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
        poolManager.SpawnFromPool<Arrow>("Arrow").Init();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireCube(pos, size);
    }
}
