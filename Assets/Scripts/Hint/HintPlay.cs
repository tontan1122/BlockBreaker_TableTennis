using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �q���g���Đ�����N���X
/// </summary>
public class HintPlay : MonoBehaviour
{
    [SerializeField, Header("�q���g�{�[���̃I�u�W�F�N�g")]
    private GameObject hintBall;

    [SerializeField, Header("�q���g���[�h�N���X")]
    private HintLoad hintLoad;

    [SerializeField, Header("�{�[���̈ʒu")]
    private List<Vector2> ballPositionList = new List<Vector2>();

    private Transform ballTransform;

    private int stageCount = 0; //�A���X�e�[�W�N���A��

    void Start()
    {
        ballTransform = hintBall.transform;
        hintBall.SetActive(false);
    }

    //�X�e�[�W�̏ꏊ�����ɂ���ĈႤ���߈�X�e�[�W�ڂŃq���g�̐ݒ�����Ă����ɘA���N���A��*15�ł��
    IEnumerator HintMove()
    {
        for (int i = 0; i < ballPositionList.Count; i++)
        {
            Vector2 pos = ballTransform.position;
            pos.x = ballPositionList[i].x;
            pos.y = ballPositionList[i].y + stageCount * 15;
            ballTransform.position = pos;
            yield return null;  //�P�t���[����~
        }
        Debug.Log("�q���g�I��");
        hintBall.SetActive(false);
    }

    public void HintStart(int sc, int level)
    {
        hintBall.SetActive(true);
        stageCount = sc - 1;    //-1�͈�X�e�[�W�ڂł��ׂĘ^�悵�Ă��邽��
        ballPositionList = hintLoad.LoadHintData(level);
        StartCoroutine(nameof(HintMove));
    }
}