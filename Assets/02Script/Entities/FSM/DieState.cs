using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : BaseState
{
    public DieState(EnemyFSM stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        StartAnimation(fsm.animData.DieParameterHash);
        fsm.rb.velocity = Vector3.zero;
        Player.Instance.fsm.target = null;

    }

    public override void Execute()
    {

    }

    public override void Exit()
    {
        StopAnimation(fsm.animData.DieParameterHash);
    }
}
