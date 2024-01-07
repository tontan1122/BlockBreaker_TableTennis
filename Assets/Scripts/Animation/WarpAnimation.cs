using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using DG.Tweening;

/// <summary>
/// ���[�v���̃{�[���A�j���[�V����
/// </summary>
public class WarpAnimation
{
    private static readonly float WARP_UP_TO_TIME = 0.5f;  // ���[�v����܂ł̎���

    private static readonly float ROTATE_ANIMATION_SPEED = 30;    // ���[�v�̉�]�X�s�\�h

    /// <summary>
    /// ���[�v�z�[�����̂̃A�j���[�V��������
    /// </summary>
    internal void StartWarpAnimation(Transform warpTransform)
    {
        warpTransform.transform.Rotate(Vector3.forward, ROTATE_ANIMATION_SPEED * Time.deltaTime);
    }

    /// <summary>
    /// ���[�v�z�[���ɓ���Ƃ��̃A�j���[�V��������
    /// </summary>
    /// <param name="obj">���[�v�ɐG�ꂽ�I�u�W�F�N�g</param>
    internal async UniTask InitiateWarpAnimation(Vector3 warpPosition,GameObject obj)
    {
        obj.transform.DOMove(warpPosition, WARP_UP_TO_TIME);
        obj.transform.DOScale(0, WARP_UP_TO_TIME);

        await UniTask.Delay(TimeSpan.FromSeconds(WARP_UP_TO_TIME + 0.01f));
    }

    /// <summary>
    /// ���[�v�z�[������o��Ƃ��̃A�j���[�V��������
    /// </summary>
    /// <param name="obj"></param>
    internal void TerminateWarpAnimation(GameObject obj)
    {
        obj.transform.DOScale(GlobalConst.BALL_SIZE, 0);
    }
}
