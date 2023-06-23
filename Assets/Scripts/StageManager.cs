using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField, Header("���x���ʃX�e�[�W�I�u�W�F�N�g")]
    private GameObject[] stageObject;

    [SerializeField,Header("�u���b�N�}�l�[�W���[")]
    private BlockManager blockManager;

    void Start()
    {
        
    }

    public void StageGeneration(int level)
    {
        Instantiate(stageObject[level], new Vector3(0, 0, 0), Quaternion.identity);
        blockManager.blockGeneration(level);
    }
}
