using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : BaseState
{
    EnemyFSM stateMachine;
    Enemy enemy;
    public DieState(EnemyFSM stateMachine) : base(stateMachine)
    {
        this.stateMachine = stateMachine;
        enemy = stateMachine.enemy;
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
