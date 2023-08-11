using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�e�[�W�̊Ǘ����s���N���X
/// </summary>
public class StageManager : MonoBehaviour
{
    [SerializeField, Header("��ʓI�ȃX�e�[�W�I�u�W�F�N�g")]
    private GameObject stageObject;

    [SerializeField, Header("�V��Ȃ��X�e�[�W�I�u�W�F�N�g")]
    private GameObject noCeilingObject;

    [SerializeField, Header("�N���A���\����Floor�I�u�W�F�N�g")]
    private GameObject ClearFloor;

    [SerializeField, Header("�u���b�N�}�l�[�W���[")]
    private BlockManager blockManager;

    [SerializeField, Header("�q���g�v���C�N���X")]
    private HintPlay hintPlay;

    [SerializeField]
    private int continuousClear = 0;    //�A���i����

    private List<GameObject> stages = new List<GameObject>();

    private int currentLevel = 0;

    private bool isSpecialStage = false;

    private GameObject cloneFloor;
    private GameObject cloneStage;

    public void StageInit(int level)
    {
        //���݂̃��x���̐ݒ�
        currentLevel = level;

        StageGeneration();

        continuousClear++;
        blockManager.BlockGeneration(level, continuousClear);   //�u���b�N�̐���
    }

    /// <summary>
    /// �X�e�[�W�̐���
    /// </summary>
    private void StageGeneration()
    {
        /*switch���ɓ��Ă͂܂���͓̂���ȃX�e�[�W�𐶐�����ꍇ�ł���*/
        switch (currentLevel)   // �ǂ̃X�e�[�W�����ꂩ�ǂ���
        {
            case 11:
            case 12:
                stages.Add(Instantiate(noCeilingObject, new Vector3(0, continuousClear * 15, 0), Quaternion.identity)); //�V�䖳���X�e�[�W�̐���
                isSpecialStage = true;
                break;
            default:
                stages.Add(Instantiate(stageObject, new Vector3(0, continuousClear * 15, 0), Quaternion.identity)); //�ʏ�X�e�[�W�̐���
                break;
        }
    }

    /// <summary>
    /// �N���A�������Ɏ��ȂȂ��悤�ɃI�u�W�F�N�g�𐶐�����
    /// </summary>
    public void ClearStage()
    {
        int clearCount = continuousClear - 1;
        // ���̐���
        cloneFloor = Instantiate(ClearFloor, new Vector3(0, clearCount * 15 - 4.8f, 0), Quaternion.identity);
        cloneFloor.transform.parent = stages[clearCount].transform;

        // �V��Ȃǂ��Ȃ��ꍇ�̂ݑS�̂𕢂�
        if (isSpecialStage)
        {
            cloneStage = Instantiate(stageObject, new Vector3(0, clearCount * 15, 0), Quaternion.identity);
            cloneStage.transform.parent = stages[clearCount].transform;
            isSpecialStage = false;
        }
    }

    public bool IsClear
    {
        get { return blockManager.IsClear; }
        set { blockManager.IsClear = value; }
    }

    /// <summary>
    /// ���̃X�e�[�W�ɐi�ނƂ��ɃN���A�̃t���O��false�ɂ���
    /// </summary>
    public void StageClearReset()
    {
        blockManager.IsClear = false;
    }

    /// <summary>
    /// �^�C�g���J�ڂ̎��̊��S�X�e�[�W���Z�b�g�֐�
    /// </summary>
    public void Reset()
    {
        //�X�e�[�W�̍폜
        for (int i = 0; i < stages.Count; i++)
        {
            Destroy(stages[i]);
        }
        stages.Clear(); //���X�g�̃N���A

        blockManager.BlockDestroy(); // �u���b�N�̍폜

        continuousClear = 0;    //�A���N���A���̃��Z�b�g
        blockManager.IsClear = false;
    }

    /// <summary>
    /// �u���b�N��ݒu���Ȃ����֐�
    /// </summary>
    public void StageReset()
    {
        blockManager.BlockReset(continuousClear);
    }

    public int ContinuousClear
    {
        get { return continuousClear; }
    }

    public GameObject GetCloneFloor
    {
        get { return cloneFloor; }
    }

    /// <summary>
    /// �N���A���̏o�����鏰�Ȃǂ��폜
    /// </summary>
    public void ClearStageReset()
    {
        Destroy(cloneFloor);
        if (cloneStage != null)
        {
            isSpecialStage = true;
            Destroy(cloneStage);
        }
    }

    /// <summary>
    /// �q���g���X�^�[�g����Ƃ��Ăяo��
    /// </summary>
    public void HintClick()
    {
        hintPlay.HintStart(continuousClear, currentLevel);
    }
}
