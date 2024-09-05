using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PoolObject
{
    public abstract void SetData(string rcode);
}

public interface IState
{
    void Enter();
    void Execute();
    void Exit();
}