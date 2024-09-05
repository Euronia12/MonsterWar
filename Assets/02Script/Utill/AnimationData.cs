using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AnimationData
{
    #region ParameterNames
    [SerializeField] private string idleParameterName = "Idle";
    [SerializeField] private string attackParameterName = "NormalAttack";
    [SerializeField] private string moveParameterName = "Move";
    [SerializeField] private string dieParameterName = "Die";
    #endregion

    #region ParameterHashs
    public int IdleParameterHash { get; private set; }
    public int AttackParameterHash { get; private set;}
    public int MoveParameterHash { get; private set;}
    public int DieParameterHash { get; private set; }

    #endregion

    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        AttackParameterHash = Animator.StringToHash(attackParameterName);
        MoveParameterHash = Animator.StringToHash(moveParameterName);
        DieParameterHash = Animator.StringToHash(dieParameterName);
    }
}

