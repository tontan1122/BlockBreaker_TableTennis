using UnityEngine;

/// <summary>
/// ステージ生成を行うクラス
/// </summary>
internal class StageGenerator : MonoBehaviour
{
    [SerializeField, Header("一般的なステージオブジェクト")]
    private GameObject stageObject;

    [SerializeField, Header("天井なしステージオブジェクト")]
    private GameObject noCeilingObject;

    [SerializeField, Header("クリア時表示のFloorオブジェクト")]
    private GameObject clearFloor;

    /// <summary>
    /// 通常ステージの生成
    /// </summary>
    /// <param name="stageLocation">ステージ連続数（ステージ生成位置）</param>
    internal GameObject NormalStageGeneration(int stageLocation)
    {
        return Instantiate(stageObject, new Vector3(0, stageLocation * GlobalConst.STAGE_SIZE_Y, 0), Quaternion.identity);
    }

    /// <summary>
    /// 天井無しステージの生成
    /// </summary>
    internal GameObject NoCeilingGeneration(int stageLocation)
    {
        return Instantiate(noCeilingObject, new Vector3(0, stageLocation * GlobalConst.STAGE_SIZE_Y, 0), Quaternion.identity);
    }

    /// <summary>
    /// クリアした際の床生成
    /// </summary>
    internal GameObject ClearStageGeneration(int stageLocation)
    {
        return Instantiate(clearFloor, new Vector3(0, stageLocation * GlobalConst.STAGE_SIZE_Y - 4.8f, 0), Quaternion.identity);
    }
}
