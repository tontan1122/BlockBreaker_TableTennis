using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �X�e�[�W�̊Ǘ����s���N���X
/// </summary>
internal class StageManager : MonoBehaviour
{
    [SerializeField, Header("�X�e�[�W�����N���X")]
    private StageGenerator stageGenerator;

    [SerializeField, Header("�u���b�N�}�l�[�W���[")]
    private BlockManager blockManager;

    [SerializeField, Header("�q���g�v���C�N���X")]
    private HintPlay hintPlay;

    [SerializeField]
    private int continuousClear = 0;    //�A���i����

    [SerializeField, Header("�V��Ȃ��X�e�[�W�̔ԍ�")]
    private int[] noCeilingStageNumber;

    [SerializeField, Header("�E�̕ǂȂ��X�e�[�W�̔ԍ�")]
    private int[] noRightWallStageNumber;

    [SerializeField, Header("���̕ǂȂ��X�e�[�W�̔ԍ�")]
    private int[] noLeftWallStageNumber;

    [SerializeField, Header("�ǂȂ��X�e�[�W�̔ԍ�")]
    private int[] noWallStageNumber;

    [SerializeField, Header("�ǂƓV��Ȃ��X�e�[�W�̔ԍ�")]
    private int[] noWallAndCeilingStageNumber;

    private List<GameObject> stages = new List<GameObject>();

    private int currentLevel = 0;

    private bool isSpecialStage = false;

    private GameObject cloneFloor;  // �R�s�[�p
    private GameObject cloneStage;  // �R�s�[�p

    internal void StageInit(int level)
    {
        //���݂̃��x���̐ݒ�
        currentLevel = level;

        StageGeneration();

        continuousClear++;
        blockManager.GenerateBlock(level, continuousClear);   //�u���b�N�̐���
    }

    /// <summary>
    /// �X�e�[�W�̐���
    /// </summary>
    private void StageGeneration()
    {
        int stagePattern = 0;

        for (int i = 0; i < noCeilingStageNumber.Length; i++)
        {
            if (currentLevel == noCeilingStageNumber[i])
            {
                stagePattern = 1;
            }
            else
            {
            }
        }
        for (int i = 0; i < noRightWallStageNumber.Length; i++)
        {
            if (currentLevel == noRightWallStageNumber[i])
            {
                stagePattern = 2;
            }
            else
            {
            }
        }
        for (int i = 0; i < noLeftWallStageNumber.Length; i++)
        {
            if (currentLevel == noLeftWallStageNumber[i])
            {
                stagePattern = 3;
            }
            else
            {
            }
        }
        for (int i = 0; i < noWallStageNumber.Length; i++)
        {
            if (currentLevel == noWallStageNumber[i])
            {
                stagePattern = 4;
            }
            else
            {
            }
        }
        for (int i = 0; i < noWallAndCeilingStageNumber.Length; i++)
        {
            if (currentLevel == noWallAndCeilingStageNumber[i])
            {
                stagePattern = 5;
            }
            else
            {
            }
        }

        if(stagePattern != 0)
        {
            isSpecialStage = true;
        }

        stages.Add(stageGenerator.StageGeneration(stagePattern, continuousClear));

    }

    /// <summary>
    /// �N���A�������Ɏ��ȂȂ��悤�ɃI�u�W�F�N�g�𐶐�����
    /// </summary>
    internal void ClearStage()
    {
        int clearCount = continuousClear - 1;
        // ���̐���
        cloneFloor = stageGenerator.ClearStageGeneration(clearCount);
        cloneFloor.transform.parent = stages[clearCount].transform;

        // �V��Ȃǂ��Ȃ��ꍇ�̂ݑS�̂𕢂�
        if (isSpecialStage)
        {
            cloneStage = stageGenerator.StageGeneration(0,clearCount);
            cloneStage.transform.parent = stages[clearCount].transform;
            isSpecialStage = false;
        }
    }

    internal bool IsClear
    {
        get { return blockManager.IsClear; }
        set { blockManager.IsClear = value; }
    }

    /// <summary>
    /// ���̃X�e�[�W�ɐi�ނƂ��ɃN���A�̃t���O��false�ɂ���
    /// </summary>
    internal void StageClearReset()
    {
        blockManager.IsClear = false;
    }

    /// <summary>
    /// �^�C�g���J�ڂ̎��̊��S�X�e�[�W���Z�b�g�֐�
    /// </summary>
    internal void Reset()
    {
        //�X�e�[�W�̍폜
        for (int i = 0; i < stages.Count; i++)
        {
            Destroy(stages[i]);
        }
        stages.Clear(); //���X�g�̃N���A

        blockManager.DestroyBlock(); // �u���b�N�̍폜

        hintPlay.ExitHint();    // �q���g�̍Đ����~�߂�

        continuousClear = 0;    //�A���N���A���̃��Z�b�g
        blockManager.IsClear = false;
    }

    /// <summary>
    /// �u���b�N��ݒu���Ȃ����֐�
    /// </summary>
    internal void StageReset()
    {
        blockManager.ResetBlock(continuousClear);
    }

    internal int ContinuousClear
    {
        get { return continuousClear; }
    }

    internal GameObject GetCloneFloor
    {
        get { return cloneFloor; }
    }

    /// <summary>
    /// �N���A���̏o�����鏰�Ȃǂ��폜
    /// </summary>
    internal void ClearStageReset()
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
    internal void HintClick()
    {
        hintPlay.StartHint(continuousClear, currentLevel);
    }
}
