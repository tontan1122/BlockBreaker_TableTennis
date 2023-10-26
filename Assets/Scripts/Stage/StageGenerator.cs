using UnityEngine;

/// <summary>
/// �X�e�[�W�������s���N���X
/// </summary>
internal class StageGenerator : MonoBehaviour
{
    [SerializeField, Header("��ʓI�ȃX�e�[�W�I�u�W�F�N�g")]
    private GameObject stageObject;

    [SerializeField, Header("�V��Ȃ��X�e�[�W�I�u�W�F�N�g")]
    private GameObject noCeilingObject;

    [SerializeField, Header("�N���A���\����Floor�I�u�W�F�N�g")]
    private GameObject clearFloor;

    /// <summary>
    /// �ʏ�X�e�[�W�̐���
    /// </summary>
    /// <param name="stageLocation">�X�e�[�W�A�����i�X�e�[�W�����ʒu�j</param>
    internal GameObject NormalStageGeneration(int stageLocation)
    {
        return Instantiate(stageObject, new Vector3(0, stageLocation * GlobalConst.STAGE_SIZE_Y, 0), Quaternion.identity);
    }

    /// <summary>
    /// �V�䖳���X�e�[�W�̐���
    /// </summary>
    internal GameObject NoCeilingGeneration(int stageLocation)
    {
        return Instantiate(noCeilingObject, new Vector3(0, stageLocation * GlobalConst.STAGE_SIZE_Y, 0), Quaternion.identity);
    }

    /// <summary>
    /// �N���A�����ۂ̏�����
    /// </summary>
    internal GameObject ClearStageGeneration(int stageLocation)
    {
        return Instantiate(clearFloor, new Vector3(0, stageLocation * GlobalConst.STAGE_SIZE_Y - 4.8f, 0), Quaternion.identity);
    }
}
