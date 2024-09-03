using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    bool isInit;
    [SerializeField]
    Rigidbody2D rbody;
    [SerializeField]
    private float arrowSpeed;
    private string enemyTag = "Enemy";
    private Player player => Player.Instance;
    // Start is called before the first frame update
    void Start()
    {

        isInit = true;
    }

    private void OnEnable()
    {
        if (isInit)
        {
            rbody.velocity = Vector2.right * arrowSpeed;
        }
    }

    private void OnDisable()
    {
        if (!isInit)
        {
            rbody.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(enemyTag))
        {
            //플레이어 스탯 가져와서 데미지 주기
        }
    }
}
