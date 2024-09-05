using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    private PoolManager poolManager => PoolManager.Instance;
    private DataManager dataManager => DataManager.Instance;

    public bool isStageClear;
    public int stageCount = 0;

    protected override void Awake()
    {
        base.Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartStage();
    }

    public void StartStage()
    {
        StartCoroutine(ExecuteStage());
    }

    IEnumerator ExecuteStage()
    {
        yield return null;
        while (GameManager.Instance.isPlaying)
        {
            if (stageCount == dataManager.enemyList.Count)
                stageCount = 0;
            poolManager.SpawnFromPool<Enemy>(dataManager.enemyList[stageCount].rcode).Init();
            yield return new WaitUntil(() => isStageClear);
            stageCount++;
            isStageClear = false;
            yield return new WaitForSeconds(2f);
        }
    }

}
