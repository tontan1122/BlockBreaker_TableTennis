using DG.Tweening;
using UnityEngine;

/// <summary>
/// �J�����̋����N���X
/// </summary>
public class CameraController : MonoBehaviour
{
    //�֐��������Ƃ��Ďg���p
    public delegate void ResetDelegate();

    /// <summary>
    /// ���̃X�e�[�W�փJ�����𓮂���
    /// </summary>
    /// <param name="clearCount"></param>
    public void MoveNextStageCamera(int clearCount)
    {
        gameObject.transform.DOMove(new Vector3(0, clearCount * 15, -10.0f), 1.0f)
    .SetEase(Ease.InOutCubic);
    }

    /// <summary>
    /// �J�������^�C�g���̏ꏊ�܂Ŗ߂�
    /// </summary>
    /// <param name="stageReset">�X�e�[�W�̃��Z�b�g�֐�</param>
    public void MoveTitleCamera(ResetDelegate stageReset)
    {
        gameObject.transform.DOMove(new Vector3(0, -15, -10), 1.0f)
            .SetEase(Ease.InOutCubic)
            .OnComplete(() => stageReset());
    }
}