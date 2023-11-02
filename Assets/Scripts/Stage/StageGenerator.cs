using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステージ生成を行うクラス
/// </summary>
internal class StageGenerator : MonoBehaviour
{
    [SerializeField,Header("ステージオブジェクト")]
    private List<GameObject> stageObjects = new List<GameObject>();

    [SerializeField, Header("クリア時表示のFloorオブジェクト")]
    private GameObject clearFloor;


    /// <summary>
    /// ステージ生成メソッド
    /// </summary>
    /// <param name="stagePattern">ステージの種類</param>
    /// <param name="stageLocation">生成場所</param>
    /// <returns>生成したステージオブジェクト</returns>
    internal GameObject StageGeneration(int stagePattern,int stageLocation)
    {
        return Instantiate(stageObjects[stagePattern], new Vector3(0, stageLocation * GlobalConst.STAGE_SIZE_Y, 0), Quaternion.identity);
    }

    /// <summary>
    /// クリアした際の床生成
    /// </summary>
    internal GameObject ClearStageGeneration(int stageLocation)
    {
        return Instantiate(clearFloor, new Vector3(0, stageLocation * GlobalConst.STAGE_SIZE_Y - 4.8f, 0), Quaternion.identity);
    }
}
