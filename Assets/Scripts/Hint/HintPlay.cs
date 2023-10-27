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

    [SerializeField, Header("ボールの位置")]
    private List<Vector2> ballPositionList = new List<Vector2>();

    private Transform ballTransform;

    private int stageCount = 0; //連続ステージクリア数

    private bool isHintPlay = false;

    void Start()
    {
        ballTransform = hintBall.transform;
        hintBall.SetActive(false);
    }

    //ステージの場所が時によって違うため一ステージ目でヒントの設定をしてそこに連続クリア数*15でやる
    IEnumerator HintMove()
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
        Debug.Log("ヒント終了");
        hintBall.SetActive(false);
    }

    internal void HintStart(int sc, int level)
    {
        hintBall.SetActive(true);
        isHintPlay = true;
        stageCount = sc - 1;    //-1は一ステージ目ですべて録画しているため
        ballPositionList = hintLoad.LoadHintData(level);
        StartCoroutine(nameof(HintMove));
    }

    internal void HintStop()
    {
        isHintPlay = false;
    }
}
