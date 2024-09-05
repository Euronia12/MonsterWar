using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Image hpBar;
    public RectTransform hpBarRect;

    private Camera cam;
    private Vector3 mousePos;

    private Enemy enemy;
    #region InfoPopup
    public GameObject InfoPopup;
    public Image icon;
    public Text nameTxt;
    public Text gradeTxt;
    public Text speedTxt;
    public Text healthTxt;
    #endregion
    protected override void Awake()
    {
        base.Awake();
        cam = Camera.main;
    }

    private void Update()
    {
        if(GameManager.Instance.isPlaying)
        {
            if(Input.GetMouseButtonDown(0)) 
            { 
                mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                var hit = Physics2D.Raycast(mousePos, transform.position, 15f);
                if (hit && hit.collider.gameObject.TryGetComponent(out enemy))
                {
                    InfoPopup.SetActive(true);
                    icon.sprite = enemy.sr.sprite;
                    nameTxt.text = enemy.stat.name;
                    gradeTxt.text = enemy.stat.grade;
                    speedTxt.text = enemy.stat.speed.ToString();
                    healthTxt.text = string.Format("{0} / {1}" ,enemy.stat.health, enemy.stat.maxHealth);
                }
            }
        }
    }

    public void HidePopUp(GameObject go)
    {
        go.SetActive(false);
    }
}
