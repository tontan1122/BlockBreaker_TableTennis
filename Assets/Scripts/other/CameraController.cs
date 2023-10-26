using DG.Tweening;
using UnityEngine;

/// <summary>
/// カメラの挙動クラス
/// </summary>
internal class CameraController : MonoBehaviour
{

    //関数を引数として使う用
    internal delegate void ResetDelegate();

    /// <summary>
    /// 次のステージへカメラを動かす
    /// </summary>
    /// <param name="clearCount"></param>
    internal void MoveNextStageCamera(int clearCount)
    {
        gameObject.transform.DOMove(new Vector3(0, clearCount * GlobalConst.STAGE_SIZE_Y, -10.0f), 1.0f)
    .SetEase(Ease.InOutCubic);
    }

    /// <summary>
    /// カメラをタイトルの場所まで戻す
    /// </summary>
    /// <param name="stageReset">ステージのリセット関数</param>
    internal void MoveTitleCamera(ResetDelegate stageReset)
    {
        gameObject.transform.DOMove(new Vector3(0, -GlobalConst.STAGE_SIZE_Y, -10), 1.0f)
            .SetEase(Ease.InOutCubic)
            .OnComplete(() => stageReset());
    }
}
