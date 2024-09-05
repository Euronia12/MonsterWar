using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class BaseState : IState
{
    protected EnemyFSM fsm;

    public abstract void Enter();
    public abstract void Execute();
    public abstract void Exit();

    public BaseState(EnemyFSM stateMachine)
    {
        this.fsm = stateMachine;
    }

    protected void StartAnimation(int animationHash)
    {
        fsm.anim.SetBool(animationHash, true);
    }

    protected void StopAnimation(int animationHash)
    {
        fsm.anim.SetBool(animationHash, false);
    }
}