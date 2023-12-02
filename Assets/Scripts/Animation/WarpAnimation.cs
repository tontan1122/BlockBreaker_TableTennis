using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using DG.Tweening;

internal class WarpAnimation : MonoBehaviour
{
    [SerializeField, Header("ワープするまでの時間")]
    private float warpUpToTime = 0.5f;

    [SerializeField, Header("回転アニメーションのスピード")]
    private float rotateAnimationSpeed = 30;

    private void Update()
    {
        WarpHoleAnimation();
    }

    private void WarpHoleAnimation()
    {
        gameObject.transform.Rotate(Vector3.forward, rotateAnimationSpeed * Time.deltaTime);
    }

    internal async UniTask WarpInAnimationMove(GameObject obj)
    {
        obj.transform.DOMove(this.gameObject.transform.position, warpUpToTime);
        obj.transform.DOScale(0, warpUpToTime);

        await UniTask.Delay(TimeSpan.FromSeconds(warpUpToTime + 0.01f));
    }

    internal void WarpOutAnimationMove(GameObject obj)
    {
        obj.transform.DOScale(GlobalConst.BALL_SIZE, 0);
    }
}
