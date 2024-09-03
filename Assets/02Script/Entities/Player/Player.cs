using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public PlayerFSM fsm;
    public PlayerAnimController anim;
    public Transform shootPos;
    public PlayerStat playerStat = new PlayerStat();

}
