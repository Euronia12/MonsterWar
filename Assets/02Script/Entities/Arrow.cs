using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rbody;
    [SerializeField]
    private float arrowSpeed;
    private string enemyTag = "Enemy";
    private Player player => Player.Instance;

    private bool isReturn;
    private WaitUntil checking;

    private void Awake()
    {
        checking = new WaitUntil(() => CheckedArrow());
    }

    private void OnEnable()
    {
        isReturn = false;
        transform.position = player.shootPos.position;
        StartCoroutine(Shooting());
    }

    private void OnBecameInvisible()
    {
        InitArrow();
    }

    IEnumerator Shooting()
    {
        yield return checking;
        InitArrow();
    }

    private bool CheckedArrow()
    {
        if (transform.position.x > 10 || isReturn)
            return true;
        rbody.velocity = Vector2.right * arrowSpeed;
        return false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(enemyTag))
        {
            //�÷��̾� ���� �����ͼ� ������ �ֱ�
            int damage = player.playerStat.atkPower;
            isReturn = true;
        }
    }

    private void InitArrow()
    {
        rbody.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}
