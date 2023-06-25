using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField, Header("���x���ʃX�e�[�W�I�u�W�F�N�g")]
    private GameObject stageObject;

    [SerializeField, Header("�N���A���\����Floor�I�u�W�F�N�g")]
    private GameObject ClearFloor;

    [SerializeField, Header("�u���b�N�}�l�[�W���[")]
    private BlockManager blockManager;

    [SerializeField, Header("�A���i����")]
    private int continuousClear = 0;

    private List<GameObject> stages = new List<GameObject>();

    private GameObject cloneFloor;

    void Start()
    {
    }

    public void StageGeneration(int level)
    {
        stages.Add(Instantiate(stageObject, new Vector3(0, continuousClear * 15, 0), Quaternion.identity));
        continuousClear++;
        blockManager.BlockGeneration(level, continuousClear);
    }

    public void ClearBar()
    {
        int clearCount = continuousClear - 1;
        cloneFloor = Instantiate(ClearFloor, new Vector3(0, clearCount * 15 - 4.8f, 0), Quaternion.identity);
        cloneFloor.transform.parent = stages[clearCount].transform;
    }

    public bool IsClear
    {
        get { return blockManager.IsClear; }
        set { blockManager.IsClear = value; }
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

    public GameObject GetCloneFloor
    {
        get { return cloneFloor; }
    }
}
