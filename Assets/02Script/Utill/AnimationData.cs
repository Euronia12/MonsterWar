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
    #endregion

    #region ParameterHashs
    public int IdleParameterHash { get; private set; }
    public int attackParameterHash { get; private set;}

    #endregion

    public void Initialize()
    {
        IdleParameterHash = Animator.StringToHash(idleParameterName);
        attackParameterHash = Animator.StringToHash(attackParameterName);
    }
}

