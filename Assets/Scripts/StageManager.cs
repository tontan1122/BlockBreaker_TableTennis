using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージの管理を行うクラス
/// </summary>
public class StageManager : MonoBehaviour
{
    [SerializeField, Header("レベル別ステージオブジェクト")]
    private GameObject stageObject;

    [SerializeField, Header("クリア時表示のFloorオブジェクト")]
    private GameObject ClearFloor;

    [SerializeField, Header("ブロックマネージャー")]
    private BlockManager blockManager;

    [SerializeField,Header("ヒントプレイクラス")]
    private HintPlay hintPlay;

    [SerializeField]
    private int continuousClear = 0;    //連続進捗数

    private List<GameObject> stages = new List<GameObject>();

    private int currentLevel = 0;

    private GameObject cloneFloor;

    void Start()
    {
    }

    public void StageGeneration(int level)
    {
        //現在のレベルの設定
        currentLevel = level;

        stages.Add(Instantiate(stageObject, new Vector3(0, continuousClear * 15, 0), Quaternion.identity)); //ステージの生成
        continuousClear++;
        blockManager.BlockGeneration(level, continuousClear);   //ブロックの生成
    }

    /// <summary>
    /// クリアした時の床を生成する
    /// </summary>
    public void ClearBar()
    {
        int clearCount = continuousClear - 1;
        cloneFloor = Instantiate(ClearFloor, new Vector3(0, clearCount * 15 - 4.8f, 0), Quaternion.identity);
        cloneFloor.transform.parent = stages[clearCount].transform;
    }

    public bool IsClear
    {
        get { return blockManager.IsClear; }
        set { blockManager.IsClear = value; }
    }

    public void StageMove()
    {
        blockManager.IsClear = false;
    }

    public void Reset()
    {
        //ステージの削除
        for (int i = 0; i < stages.Count; i++)
        {
            Destroy(stages[i]);
        }
        stages.Clear(); //リストのクリア

        continuousClear = 0;    //連続クリア数のリセット
        blockManager.IsClear = false;
    }

    public void ResetBlocks()
    {
        blockManager.StageReset(continuousClear);
    }

    public int ContinuousClear
    {
        get { return continuousClear; }
    }

    public GameObject GetCloneFloor
    {
        get { return cloneFloor; }
    }

    /// <summary>
    /// ヒントをスタートするとき呼び出し
    /// </summary>
    public void HintClick()
    {
        hintPlay.HintStart(continuousClear, currentLevel);
    }
}
