using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージの管理を行うクラス
/// </summary>
public class StageManager : MonoBehaviour
{
    [SerializeField,Header("ステージ生成クラス")]
    private StageGenerator stageGenerator;

    [SerializeField, Header("ブロックマネージャー")]
    private BlockManager blockManager;

    [SerializeField, Header("ヒントプレイクラス")]
    private HintPlay hintPlay;

    [SerializeField]
    private int continuousClear = 0;    //連続進捗数

    private List<GameObject> stages = new List<GameObject>();

    private int currentLevel = 0;

    private bool isSpecialStage = false;

    private GameObject cloneFloor;
    private GameObject cloneStage;

    public void StageInit(int level)
    {
        //現在のレベルの設定
        currentLevel = level;

        StageGeneration();

        continuousClear++;
        blockManager.BlockGeneration(level, continuousClear);   //ブロックの生成
    }

    /// <summary>
    /// ステージの生成
    /// </summary>
    private void StageGeneration()
    {
        /*switch文に当てはまるものは特殊なステージを生成する場合である*/
        switch (currentLevel)   // どのステージが特殊かどうか
        {
            case 16:
            case 17:
                stages.Add(stageGenerator.NoCeilingGeneration(continuousClear));
                isSpecialStage = true;
                break;
            default:
                stages.Add(stageGenerator.NormalStageGeneration(continuousClear));
                break;
        }
    }

    /// <summary>
    /// クリアした時に死なないようにオブジェクトを生成する
    /// </summary>
    public void ClearStage()
    {
        int clearCount = continuousClear - 1;
        // 床の生成
        cloneFloor = stageGenerator.ClearStageGeneration(clearCount);
        cloneFloor.transform.parent = stages[clearCount].transform;

        // 天井などがない場合のみ全体を覆う
        if (isSpecialStage)
        {
            cloneStage = stageGenerator.NormalStageGeneration(clearCount);
            cloneStage.transform.parent = stages[clearCount].transform;
            isSpecialStage = false;
        }
    }

    public bool IsClear
    {
        get { return blockManager.IsClear; }
        set { blockManager.IsClear = value; }
    }

    /// <summary>
    /// 次のステージに進むときにクリアのフラグをfalseにする
    /// </summary>
    public void StageClearReset()
    {
        blockManager.IsClear = false;
    }

    /// <summary>
    /// タイトル遷移の時の完全ステージリセット関数
    /// </summary>
    public void Reset()
    {
        //ステージの削除
        for (int i = 0; i < stages.Count; i++)
        {
            Destroy(stages[i]);
        }
        stages.Clear(); //リストのクリア

        blockManager.BlockDestroy(); // ブロックの削除

        continuousClear = 0;    //連続クリア数のリセット
        blockManager.IsClear = false;
    }

    /// <summary>
    /// ブロックを設置しなおす関数
    /// </summary>
    public void StageReset()
    {
        blockManager.BlockReset(continuousClear);
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
    /// クリア時の出現する床などを削除
    /// </summary>
    public void ClearStageReset()
    {
        Destroy(cloneFloor);
        if (cloneStage != null)
        {
            isSpecialStage = true;
            Destroy(cloneStage);
        }
    }

    /// <summary>
    /// ヒントをスタートするとき呼び出し
    /// </summary>
    public void HintClick()
    {
        hintPlay.HintStart(continuousClear, currentLevel);
    }
}
