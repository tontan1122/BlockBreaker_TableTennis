using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using DG.Tweening;

/// <summary>
/// ワープ時のボールアニメーション
/// </summary>
internal class WarpAnimation : MonoBehaviour
{
    [SerializeField, Header("ワープするまでの時間")]
    private float warpUpToTime = 0.5f;

    [SerializeField, Header("回転アニメーションのスピード")]
    private float rotateAnimationSpeed = 30;

    private void Update()
    {
        StartWarpAnimation();
    }

    /// <summary>
    /// ワープホール自体のアニメーション処理
    /// </summary>
    private void StartWarpAnimation()
    {
        gameObject.transform.Rotate(Vector3.forward, rotateAnimationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// ワープホールに入るときのアニメーション処理
    /// </summary>
    /// <param name="obj">ワープに触れたオブジェクト</param>
    internal async UniTask InitiateWarpAnimation(GameObject obj)
    {
        obj.transform.DOMove(this.gameObject.transform.position, warpUpToTime);
        obj.transform.DOScale(0, warpUpToTime);

        await UniTask.Delay(TimeSpan.FromSeconds(warpUpToTime + 0.01f));
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
