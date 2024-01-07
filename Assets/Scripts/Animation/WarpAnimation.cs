using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using DG.Tweening;

/// <summary>
/// ワープ時のボールアニメーション
/// </summary>
public class WarpAnimation
{
    private static readonly float WARP_UP_TO_TIME = 0.5f;  // ワープするまでの時間

    private static readonly float ROTATE_ANIMATION_SPEED = 30;    // ワープの回転スピ―ド

    /// <summary>
    /// ワープホール自体のアニメーション処理
    /// </summary>
    internal void StartWarpAnimation(Transform warpTransform)
    {
        warpTransform.transform.Rotate(Vector3.forward, ROTATE_ANIMATION_SPEED * Time.deltaTime);
    }

    /// <summary>
    /// ワープホールに入るときのアニメーション処理
    /// </summary>
    /// <param name="obj">ワープに触れたオブジェクト</param>
    internal async UniTask InitiateWarpAnimation(Vector3 warpPosition,GameObject obj)
    {
        obj.transform.DOMove(warpPosition, WARP_UP_TO_TIME);
        obj.transform.DOScale(0, WARP_UP_TO_TIME);

        await UniTask.Delay(TimeSpan.FromSeconds(WARP_UP_TO_TIME + 0.01f));
    }

    /// <summary>
    /// ワープホールから出るときのアニメーション処理
    /// </summary>
    /// <param name="obj"></param>
    internal void TerminateWarpAnimation(GameObject obj)
    {
        obj.transform.DOScale(GlobalConst.BALL_SIZE, 0);
    }
}
