using DG.Tweening;
using UnityEngine;

/// <summary>
/// カメラの挙動クラス
/// </summary>
public class CameraController : MonoBehaviour
{
    //関数を引数として使う用
    public delegate void ResetDelegate();

    /// <summary>
    /// 次のステージへカメラを動かす
    /// </summary>
    /// <param name="clearCount"></param>
    public void MoveNextStageCamera(int clearCount)
    {
        gameObject.transform.DOMove(new Vector3(0, clearCount * 15, -10.0f), 1.0f)
    .SetEase(Ease.InOutCubic);
    }

    /// <summary>
    /// カメラをタイトルの場所まで戻す
    /// </summary>
    /// <param name="stageReset">ステージのリセット関数</param>
    public void MoveTitleCamera(ResetDelegate stageReset)
    {
        gameObject.transform.DOMove(new Vector3(0, -15, -10), 1.0f)
            .SetEase(Ease.InOutCubic)
            .OnComplete(() => stageReset());
    }
}
