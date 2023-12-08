using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using DG.Tweening;

/// <summary>
/// ���[�v���̃{�[���A�j���[�V����
/// </summary>
internal class WarpAnimation : MonoBehaviour
{
    [SerializeField, Header("���[�v����܂ł̎���")]
    private float warpUpToTime = 0.5f;

    [SerializeField, Header("��]�A�j���[�V�����̃X�s�[�h")]
    private float rotateAnimationSpeed = 30;

    private void Update()
    {
        WarpHoleAnimation();
    }

    /// <summary>
    /// ���[�v�z�[�����̂̃A�j���[�V��������
    /// </summary>
    private void WarpHoleAnimation()
    {
        gameObject.transform.Rotate(Vector3.forward, rotateAnimationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// ���[�v�z�[���ɓ���Ƃ��̃A�j���[�V��������
    /// </summary>
    /// <param name="obj">���[�v�ɐG�ꂽ�I�u�W�F�N�g</param>
    internal async UniTask WarpInAnimationMove(GameObject obj)
    {
        obj.transform.DOMove(this.gameObject.transform.position, warpUpToTime);
        obj.transform.DOScale(0, warpUpToTime);

        await UniTask.Delay(TimeSpan.FromSeconds(warpUpToTime + 0.01f));
    }

    /// <summary>
    /// ���[�v�z�[������o��Ƃ��̃A�j���[�V��������
    /// </summary>
    /// <param name="obj"></param>
    internal void WarpOutAnimationMove(GameObject obj)
    {
        obj.transform.DOScale(GlobalConst.BALL_SIZE, 0);
    }
}
