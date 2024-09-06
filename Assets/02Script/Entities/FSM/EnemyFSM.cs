using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public IState currentState;
    public Enemy enemy;

    [Header("Animations")]
    public Animator anim;
    public AnimationData animData = new AnimationData();

    public Rigidbody2D rb;

    #region Enemy States
    public IdleState IdleState { get; private set; }
    public MoveState MoveState { get; private set; }
    public DieState DieState { get; private set; }
    #endregion


    public void ChangeState(IState newState)
    {
        if (currentState != null)
            currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    public void Awake()
    {
        animData.Initialize();
        IdleState = new IdleState(this);
        MoveState = new MoveState(this);
        DieState = new DieState(this);
    }

    public void Update()
    {
        if (currentState != null)
            currentState?.Execute();
    }

    private void OnEnable()
    {
        ChangeState(MoveState);
    }
}
