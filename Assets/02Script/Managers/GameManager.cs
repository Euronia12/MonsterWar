using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool isPlaying;

    protected override void Awake()
    {
        base.Awake();
    }
}
