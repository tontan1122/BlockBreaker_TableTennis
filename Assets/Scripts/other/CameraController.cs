using DG.Tweening;
using UnityEngine;

/// <summary>
/// �J�����̋����N���X
/// </summary>
internal class CameraController : MonoBehaviour
{

    //�֐��������Ƃ��Ďg���p
    internal delegate void ResetDelegate();

    /// <summary>
    /// ���̃X�e�[�W�փJ�����𓮂���
    /// </summary>
    /// <param name="clearCount"></param>
    internal void MoveNextStageCamera(int clearCount)
    {
        gameObject.transform.DOMove(new Vector3(0, clearCount * GlobalConst.STAGE_SIZE_Y, -10.0f), 1.0f)
    .SetEase(Ease.InOutCubic);
    }

    /// <summary>
    /// �J�������^�C�g���̏ꏊ�܂Ŗ߂�
    /// </summary>
    /// <param name="stageReset">�X�e�[�W�̃��Z�b�g�֐�</param>
    internal void MoveTitleCamera(ResetDelegate stageReset)
    {
        gameObject.transform.DOMove(new Vector3(0, -GlobalConst.STAGE_SIZE_Y, -10), 1.0f)
            .SetEase(Ease.InOutCubic)
            .OnComplete(() => stageReset());
    }
}
