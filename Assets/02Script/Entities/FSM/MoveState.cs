using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MoveState : BaseState
{
    EnemyFSM stateMachine;
    Enemy enemy;
    public MoveState(EnemyFSM stateMachine) : base(stateMachine)
    {
        this.stateMachine = stateMachine;
        enemy = stateMachine.enemy;
    }

    public override void Enter()
    {
        StartAnimation(fsm.animData.MoveParameterHash);
    }

    public override void Execute()
    {
        if (Physics2D.Raycast(enemy.transform.position, Vector2.left, 2f, enemy.playerMask))
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
        else if (enemy.state == EnemyState.Death)
            stateMachine.ChangeState(stateMachine.DieState);
        enemy.rbody.velocity = Vector2.left * enemy.stat.speed;
    }

    public override void Exit()
    {
        StopAnimation(fsm.animData.MoveParameterHash);
    }
}
