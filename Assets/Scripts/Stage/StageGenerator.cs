using UnityEngine;

/// <summary>
/// �X�e�[�W�������s���N���X
/// </summary>
public class StageGenerator : MonoBehaviour
{
    [SerializeField, Header("��ʓI�ȃX�e�[�W�I�u�W�F�N�g")]
    private GameObject stageObject;

    [SerializeField, Header("�V��Ȃ��X�e�[�W�I�u�W�F�N�g")]
    private GameObject noCeilingObject;

    [SerializeField, Header("�N���A���\����Floor�I�u�W�F�N�g")]
    private GameObject ClearFloor;

    /// <summary>
    /// �ʏ�X�e�[�W�̐���
    /// </summary>
    /// <param name="stageLocation">�X�e�[�W�A�����i�X�e�[�W�����ʒu�j</param>
    public GameObject NormalStageGeneration(int stageLocation)
    {
        return Instantiate(stageObject, new Vector3(0, stageLocation * 15, 0), Quaternion.identity);
    }

    /// <summary>
    /// �V�䖳���X�e�[�W�̐���
    /// </summary>
    public GameObject NoCeilingGeneration(int stageLocation)
    {
        return Instantiate(noCeilingObject, new Vector3(0, stageLocation * 15, 0), Quaternion.identity);
    }

    /// <summary>
    /// �N���A�����ۂ̏�����
    /// </summary>
    public GameObject ClearStageGeneration(int stageLocation)
    {
        return Instantiate(ClearFloor, new Vector3(0, stageLocation * 15 - 4.8f, 0), Quaternion.identity);
    }
}
