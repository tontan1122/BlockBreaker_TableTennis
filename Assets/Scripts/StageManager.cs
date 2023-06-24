using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField, Header("���x���ʃX�e�[�W�I�u�W�F�N�g")]
    private GameObject stageObject;

    [SerializeField, Header("�u���b�N�}�l�[�W���[")]
    private BlockManager blockManager;

    [SerializeField, Header("�A���i����")]
    private int continuousClear = 0;

    private List<GameObject> stages = new List<GameObject>();

    void Start()
    {

    }

    public void StageGeneration(int level)
    {
        stages.Add(Instantiate(stageObject, new Vector3(0, continuousClear * 15, 0), Quaternion.identity));
        continuousClear++;
        blockManager.BlockGeneration(level, continuousClear);
    }

    public bool IsClear
    {
        get { return blockManager.IsClear; }
    }

    public void StageMove()
    {
        blockManager.IsClear = false;
    }

    public void Reset()
    {
        //�X�e�[�W�̍폜
        for (int i = 0; i < stages.Count; i++)
        {
            Destroy(stages[i]);
        }
        stages.Clear();

        continuousClear = 0;
        blockManager.IsClear = false;
    }

    public void ResetBlocks()
    {
        blockManager.StageReset(continuousClear);
    }

    public int ContinuousClear
    {
        get { return continuousClear; }
    }
}
