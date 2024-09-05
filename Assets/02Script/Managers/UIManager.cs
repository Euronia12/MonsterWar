using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Image hpBar;
    public RectTransform hpBarRect;

    protected override void Awake()
    {
        base.Awake();
    }
}
