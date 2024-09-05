using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private PoolManager poolManager => PoolManager.Instance;
    private DataManager dataManager => DataManager.Instance;

    public bool isStageClear;
    public int stageCount = 0;

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
            poolManager.SpawnFromPool<Enemy>(dataManager.enemyList[stageCount++].rcode).Init();
            yield return new WaitUntil(() => isStageClear);
            isStageClear = false;
        }
    }

}
