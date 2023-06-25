using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIController : MonoBehaviour
{
    [SerializeField, Header("�X�e�[�W�e�L�X�g")]
    private TextMeshProUGUI stageText;

    [SerializeField, Header("�~�X�\���̃e�L�X�g")]
    private TextMeshProUGUI missText;

    void Start()
    {
        stageText.text = "Stage0";
    }

    public void ChangeStageText(int level)
    {
        stageText.text = "Stage" + level.ToString();
    }

    public void MissCountText(int miss)
    {
        missText.text = miss.ToString() + "Miss";
    }
}