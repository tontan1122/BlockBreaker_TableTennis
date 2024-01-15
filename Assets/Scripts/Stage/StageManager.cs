using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージの管理を行うクラス
/// </summary>
public class StageManager : MonoBehaviour
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

    [SerializeField, Header("右の壁なしステージの番号")]
    private int[] noRightWallStageNumber;

    [SerializeField, Header("左の壁なしステージの番号")]
    private int[] noLeftWallStageNumber;

    [SerializeField, Header("壁なしステージの番号")]
    private int[] noWallStageNumber;

    [SerializeField, Header("壁と天井なしステージの番号")]
    private int[] noWallAndCeilingStageNumber;

    private List<GameObject> stages = new List<GameObject>();

    private int currentLevel = 0;

    private bool isSpecialStage = false;

    private GameObject cloneFloor;  // コピー用
    private GameObject cloneStage;  // コピー用

    /// <summary>
    /// ステージの準備
    /// </summary>
    /// <param name="level">生成するレベル</param>
    public void PrepareStage(int level)
    {
        //現在のレベルの設定
        currentLevel = level;

        GenerateStage();

        continuousClear++;
        blockManager.GenerateBlock(level, continuousClear);   //ブロックの生成
    }

    /// <summary>
    /// ステージの生成
    /// </summary>
    private void GenerateStage()
    {
        int stagePattern = 0;

        for (int i = 0; i < noCeilingStageNumber.Length; i++)
        {
            if (currentLevel == noCeilingStageNumber[i])
            {
                stagePattern = 1;
            }
            else
            {
            }
        }
        for (int i = 0; i < noRightWallStageNumber.Length; i++)
        {
            if (currentLevel == noRightWallStageNumber[i])
            {
                stagePattern = 2;
            }
            else
            {
            }
        }
        for (int i = 0; i < noLeftWallStageNumber.Length; i++)
        {
            if (currentLevel == noLeftWallStageNumber[i])
            {
                stagePattern = 3;
            }
            else
            {
            }
        }
        for (int i = 0; i < noWallStageNumber.Length; i++)
        {
            if (currentLevel == noWallStageNumber[i])
            {
                stagePattern = 4;
            }
            else
            {
            }
        }
        for (int i = 0; i < noWallAndCeilingStageNumber.Length; i++)
        {
            if (currentLevel == noWallAndCeilingStageNumber[i])
            {
                stagePattern = 5;
            }
            else
            {
            }
        }

        if(stagePattern != 0)
        {
            isSpecialStage = true;
        }

        stages.Add(stageGenerator.InstanceStage(stagePattern, continuousClear));

    }

    /// <summary>
    /// クリアした時に死なないようにオブジェクトを生成する
    /// </summary>
    public void GenerateClearStage()
    {
        int clearCount = continuousClear - 1;
        // 床の生成
        cloneFloor = stageGenerator.GenerateClearStage(clearCount);
        cloneFloor.transform.parent = stages[clearCount].transform;

        // 天井などがない場合のみ全体を覆う
        if (isSpecialStage)
        {
            cloneStage = stageGenerator.InstanceStage(0,clearCount);
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
    public void ResetStage()
    {
        //ステージの削除
        for (int i = 0; i < stages.Count; i++)
        {
            Destroy(stages[i]);
        }
        stages.Clear(); //リストのクリア

        blockManager.DestroyBlock(); // ブロックの削除

        hintPlay.ExitHint();    // ヒントの再生を終わる

        continuousClear = 0;    //連続クリア数のリセット
        blockManager.IsClear = false;
    }

    /// <summary>
    /// ブロックを設置しなおす関数
    /// </summary>
    public void ResetBlockLevel()
    {
        blockManager.ResetBlock(continuousClear);
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
    public void DestroyClearStage()
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
        hintPlay.StartHint(continuousClear, currentLevel);
    }
}
