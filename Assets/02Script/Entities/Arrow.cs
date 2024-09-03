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

    private void OnEnable()
    {
        rbody.velocity = Vector2.right * arrowSpeed;
    }

    private void OnBecameInvisible()
    {
        InitArrow();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(enemyTag))
        {
            //플레이어 스탯 가져와서 데미지 주기
            int damage = player.playerStat.atkPower;
            InitArrow();
        }
    }

    private void InitArrow()
    {
        rbody.velocity = Vector2.zero;
        transform.position = player.shootPos.position;
        gameObject.SetActive(false);
    }
}
