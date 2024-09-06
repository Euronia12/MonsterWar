using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    private PoolManager poolManager => PoolManager.Instance;
    private DataManager dataManager => DataManager.Instance;

    //스테이지 단계
    public int stageCount = 0;
    //스테이지 클리어 여부
    public bool isStageClear;
    private WaitUntil stageClear;

    //다음스테이지 대기시간
    public float time = 2f;
    private WaitForSeconds waitingTime;

    protected override void Awake()
    {
        base.Awake();
        stageClear = new WaitUntil(() => isStageClear);
        waitingTime = new WaitForSeconds(time);
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
            yield return stageClear;
            stageCount++;
            isStageClear = false;
            yield return waitingTime;
        }
    }

}
