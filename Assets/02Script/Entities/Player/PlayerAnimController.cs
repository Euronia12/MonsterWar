using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
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
                
                break;
            case PlayerState.Attack:
                animator.SetTrigger(animData.AttackParameterHash);
                break;
            default:
                Debug.Log("상태 에러");
                break;
        }
    }
}
