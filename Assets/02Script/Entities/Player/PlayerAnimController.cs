using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    Player player => Player.Instance;
    [SerializeField]
    private Animator animator;
    private AnimationData animData;

    private void Awake()
    {
        animData = new AnimationData();
        animData.Initialize();
    }
    public void PlayAnimation(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.Idle:
                animator.SetBool(animData.AttackParameterHash, false);
                break;
            case PlayerState.Attack:
                animator.SetBool(animData.AttackParameterHash, true);
                break;
            default:
                Debug.Log("상태 에러");
                break;
        }
    }

    public void OnShoot()
    {
        player.fsm.OnShoot();
    }
}
