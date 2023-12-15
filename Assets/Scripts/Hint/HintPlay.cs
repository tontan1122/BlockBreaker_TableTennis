using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ヒントを再生するクラス
/// </summary>
internal class HintPlay : MonoBehaviour
{
    [SerializeField, Header("ヒントボールのオブジェクト")]
    private GameObject hintBall;

    [SerializeField, Header("ヒントロードクラス")]
    private HintLoad hintLoad;

    private List<Vector2> ballPositionList = new List<Vector2>();

    private Transform ballTransform;

    private int stageCount = 0; //連続ステージクリア数

    private bool isHintPlay = false;

    void Start()
    {
        ballTransform = hintBall.transform;
        hintBall.SetActive(false);
    }

    /// <summary>
    /// ヒントの再生
    /// </summary>
    /// <param name="stagePosition">ステージ位置</param>
    /// <param name="level">現在のレベル</param>
    internal void StartHint(int stagePosition, int level)
    { 
        hintBall.SetActive(true);
        if (!isHintPlay)
        {
            isHintPlay = true;
        }
        stageCount = stagePosition - 1;    //-1は一ステージ目ですべて録画しているため
        ballPositionList = hintLoad.LoadHintData(level);
        StartCoroutine(nameof(MovingHintBall));
    }

    //ステージの場所が時によって違うため一ステージ目でヒントの設定をしてそこに連続クリア数*15でやる
    private IEnumerator MovingHintBall()
    {
        for (int i = 0; i < ballPositionList.Count; i++)
        {
            Vector2 pos = ballTransform.position;
            pos.x = ballPositionList[i].x;
            pos.y = ballPositionList[i].y + stageCount * GlobalConst.STAGE_SIZE_Y;
            ballTransform.position = pos;

            if (!isHintPlay)
            {
                break;
            }

            yield return null;  //１フレーム停止
        }
        ExitHint(); // ヒント終了
        hintBall.SetActive(false);
    }

    /// <summary>
    /// ヒントの停止
    /// </summary>
    internal void ExitHint()
    {
        isHintPlay = false;
    }
}
