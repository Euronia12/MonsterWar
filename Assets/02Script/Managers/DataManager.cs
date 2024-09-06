using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    #region 분류 기호
    static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
    static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r";
    static char[] TRIM_CHARS = { '\"' };
    #endregion

    [SerializeField]
    private string fileName;

    #region 오브젝트 풀
    public List<Dictionary<string, object>> monsterInfo = new List<Dictionary<string, object>>();
    public List<EnemyStat> enemyList = new List<EnemyStat>();
    #endregion

    protected override void Awake()
    {
        base.Awake();
        monsterInfo = LoadFromCSV(fileName);
        InitMonsterInfo();
    }

    public void InitMonsterInfo()
    {
        for (int i = 0; i < monsterInfo.Count; i++)
        {
            EnemyStat stat = new EnemyStat();
            foreach (var key in monsterInfo[i].Keys)
            {
                var fieldInfo = stat.GetType().GetField(key);
                fieldInfo.SetValue(stat, monsterInfo[i][key]);
            }
            enemyList.Add(stat);
        }
    }


    public List<Dictionary<string, object>> LoadFromCSV(string file)
    {
        var list = new List<Dictionary<string, object>>();
        TextAsset data = Resources.Load(file) as TextAsset;

        var lines = Regex.Split(data.text, LINE_SPLIT_RE);

        if (lines.Length <= 1) return list;

        var header = Regex.Split(lines[0], SPLIT_RE);
        for (var i = 1; i < lines.Length; i++)
        {
            var values = Regex.Split(lines[i], SPLIT_RE);
            if (values.Length == 0 || values[0] == "") continue;

            var entry = new Dictionary<string, object>();
            for (var j = 0; j < header.Length && j < values.Length; j++)
            {
                string value = values[j];
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                object finalvalue = value;
                int n;
                float f;
                if (int.TryParse(value, out n))
                {
                    finalvalue = n;
                }
                else if (float.TryParse(value, out f))
                {
                    finalvalue = f;
                }
                entry[header[j]] = finalvalue;
            }
            list.Add(entry);
        }
        return list;
    }
}
