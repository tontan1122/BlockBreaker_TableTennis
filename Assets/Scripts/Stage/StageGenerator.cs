using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�e�[�W�������s���N���X
/// </summary>
internal class StageGenerator : MonoBehaviour
{
    [SerializeField,Header("�X�e�[�W�I�u�W�F�N�g")]
    private List<GameObject> stageObjects = new List<GameObject>();

    [SerializeField, Header("�N���A���\����Floor�I�u�W�F�N�g")]
    private GameObject clearFloor;


    /// <summary>
    /// �X�e�[�W�������\�b�h
    /// </summary>
    /// <param name="stagePattern">�X�e�[�W�̎��</param>
    /// <param name="stageLocation">�����ꏊ</param>
    /// <returns>���������X�e�[�W�I�u�W�F�N�g</returns>
    internal GameObject StageGeneration(int stagePattern,int stageLocation)
    {
        return Instantiate(stageObjects[stagePattern], new Vector3(0, stageLocation * GlobalConst.STAGE_SIZE_Y, 0), Quaternion.identity);
    }

    /// <summary>
    /// �N���A�����ۂ̏�����
    /// </summary>
    internal GameObject ClearStageGeneration(int stageLocation)
    {
        return Instantiate(clearFloor, new Vector3(0, stageLocation * GlobalConst.STAGE_SIZE_Y - 4.8f, 0), Quaternion.identity);
    }
}
