using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ステージの管理を行うクラス
/// </summary>
internal class StageManager : MonoBehaviour
{
    [SerializeField, Header("ステージ生成クラス")]
    private StageGenerator stageGenerator;

    [SerializeField, Header("ブロックマネージャー")]
    private BlockManager blockManager;

    [SerializeField, Header("ヒントプレイクラス")]
    private HintPlay hintPlay;

    [SerializeField]
    private int continuousClear = 0;    //連続進捗数

    [SerializeField, Header("天井なしステージの番号")]
    private int[] noCeilingStageNumber;

    [SerializeField,Header("右の壁なしステージの番号")]
    private int[] noRightWallStageNumber;

    [SerializeField,Header("左の壁なしステージの番号")]
    private int[] noLeftWallStageNumber;

    [SerializeField, Header("壁なしステージの番号")]
    private int[] noWallStageNumber;

    private List<GameObject> stages = new List<GameObject>();

    private int currentLevel = 0;

    private bool isSpecialStage = false;

    private GameObject cloneFloor;  // コピー用
    private GameObject cloneStage;  // コピー用

    internal void StageInit(int level)
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
        bool isNoCeilingStage = false;
        bool isNoRightWallStage = false;
        bool isNoLeftWallStage = false;
        bool isNoWallStage = false;
        
        for (int i = 0; i < noCeilingStageNumber.Length; i++)
        {
            if (currentLevel == noCeilingStageNumber[i])
            {
                isNoCeilingStage = true;
            }
            else
            {
            }
        }
        for (int i = 0; i < noRightWallStageNumber.Length; i++)
        {
            if (currentLevel == noRightWallStageNumber[i])
            {
                isNoRightWallStage = true;
            }
            else
            {
            }
        }
        for (int i = 0; i < noLeftWallStageNumber.Length; i++)
        {
            if (currentLevel == noLeftWallStageNumber[i])
            {
                isNoLeftWallStage = true;
            }
            else
            {
            }
        }
        for (int i = 0; i < noWallStageNumber.Length; i++)
        {
            if (currentLevel == noWallStageNumber[i])
            {
                isNoWallStage = true;
            }
            else
            {
            }
        }


        if (isNoCeilingStage)
        {
            stages.Add(stageGenerator.NoCeilingGeneration(continuousClear));
            isSpecialStage = true;
        }
        else if (isNoRightWallStage)
        {
            stages.Add(stageGenerator.NoRightWallGeneration(continuousClear));
            isSpecialStage = true;
        }
        else if (isNoLeftWallStage)
        {
            stages.Add(stageGenerator.NoLeftWallGeneration(continuousClear));
            isSpecialStage = true;
        }
        else if (isNoWallStage)
        {
            stages.Add(stageGenerator.NoWallGeneration(continuousClear));
            isSpecialStage = true;
        }
        else
        {
            stages.Add(stageGenerator.NormalStageGeneration(continuousClear));
        }
    }

    /// <summary>
    /// クリアした時に死なないようにオブジェクトを生成する
    /// </summary>
    internal void ClearStage()
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

    internal bool IsClear
    {
        get { return blockManager.IsClear; }
        set { blockManager.IsClear = value; }
    }

    /// <summary>
    /// 次のステージに進むときにクリアのフラグをfalseにする
    /// </summary>
    internal void StageClearReset()
    {
        blockManager.IsClear = false;
    }

    /// <summary>
    /// タイトル遷移の時の完全ステージリセット関数
    /// </summary>
    internal void Reset()
    {
        //ステージの削除
        for (int i = 0; i < stages.Count; i++)
        {
            Destroy(stages[i]);
        }
        stages.Clear(); //リストのクリア

        blockManager.BlockDestroy(); // ブロックの削除

        hintPlay.HintStop();    // ヒントの再生を止める

        continuousClear = 0;    //連続クリア数のリセット
        blockManager.IsClear = false;
    }

    /// <summary>
    /// ブロックを設置しなおす関数
    /// </summary>
    internal void StageReset()
    {
        blockManager.BlockReset(continuousClear);
    }

    internal int ContinuousClear
    {
        get { return continuousClear; }
    }

    internal GameObject GetCloneFloor
    {
        get { return cloneFloor; }
    }

    /// <summary>
    /// クリア時の出現する床などを削除
    /// </summary>
    internal void ClearStageReset()
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
    internal void HintClick()
    {
        hintPlay.HintStart(continuousClear, currentLevel);
    }
}
