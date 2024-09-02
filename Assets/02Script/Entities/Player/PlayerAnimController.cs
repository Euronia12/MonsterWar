using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    private Player player => Player.Instance;
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
                animator.SetTrigger(animData.attackParameterHash);
                break;
            default:
                Debug.Log("상태 에러");
                break;
        }
    }
}
