

public class IdleState : BaseState
{
    EnemyFSM stateMachine;
    Enemy enemy;

    public IdleState(EnemyFSM stateMachine) : base(stateMachine)
    {
        this.stateMachine = stateMachine;
        enemy = stateMachine.enemy;
    }

    public override void Enter()
    {
        StartAnimation(fsm.animData.IdleParameterHash);
    }

    public override void Execute()
    {
        if (enemy.state == EnemyState.Death)
            stateMachine.ChangeState(stateMachine.DieState);
    }

    public override void Exit()
    {
        StopAnimation(fsm.animData.IdleParameterHash);
    }
}
