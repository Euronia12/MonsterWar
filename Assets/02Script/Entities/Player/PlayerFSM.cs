using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PlayerFSM : MonoBehaviour
{
    [SerializeField]
    private Vector2 size;
    private LayerMask enemyMask;
    private WaitForSeconds coolTime;
    public GameObject target;
    public float AtkSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        coolTime = new WaitForSeconds(AtkSpeed);
        enemyMask = LayerMask.GetMask("Enemy");
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
                   var enemy = Physics2D.OverlapBox(transform.position, size, 0, enemyMask);
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
        Debug.Log("АјАн");
    }
    void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
