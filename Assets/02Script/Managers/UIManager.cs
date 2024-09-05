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

    private Enemy choicedEnemy;
    public Enemy curEnemy;
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

    private void Start()
    {
        StartCoroutine(SetUIInteraction());
    }

    IEnumerator SetUIInteraction()
    {
        while (true)
        {
            if (GameManager.Instance.isPlaying)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                    var hit = Physics2D.Raycast(mousePos, transform.position, 15f);
                    if (hit && hit.collider.gameObject.TryGetComponent(out choicedEnemy))
                    {
                        Time.timeScale = 0;
                        InfoPopup.SetActive(true);
                        icon.sprite = choicedEnemy.sr.sprite;
                        nameTxt.text = choicedEnemy.stat.name;
                        gradeTxt.text = choicedEnemy.stat.grade;
                        speedTxt.text = choicedEnemy.stat.speed.ToString();
                        healthTxt.text = string.Format("{0} / {1}", choicedEnemy.stat.health, choicedEnemy.stat.maxHealth);
                    }
                }

                if (curEnemy != null)
                {
                    if (curEnemy.stat.maxHealth > 0)
                        hpBar.fillAmount = Mathf.Max(curEnemy.stat.health, 0) / (float)curEnemy.stat.maxHealth;
                    hpBarRect.position = RectTransformUtility.WorldToScreenPoint(cam, curEnemy.transform.position + (Vector3.up * curEnemy.coll.bounds.size.y));
                }
            }
            yield return null;
        }
    }

    public void HidePopUp(GameObject go)
    {
        Time.timeScale = 1;
        go.SetActive(false);
    }
}
