using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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

    public GameObject target;
    public float atkSpeed;

    public PlayerAnimController animController;
    // Start is called before the first frame update
    void Start()
    {
        atkSpeed = Player.Instance.playerStat.attackSpeed;
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
                   var enemy = Physics2D.OverlapBox(pos, size, 0, enemyMask);
                    if (enemy != null)
                    {
                        target = enemy.gameObject;
                        Attack(target);
                        yield return coolTime;
                    }
                    else
                    {
                        yield return null;
                        continue;
                    }
                }
                else
                {
                    Attack(target);
                    yield return coolTime;
                }
            }
            else
            {
                yield return null;
            }
        }
    }


    public void Attack(GameObject enemy)
    {
        animController.PlayAnimation(PlayerState.Attack);
        Debug.Log("����");
    }
    void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireCube(pos, size);
    }
}
