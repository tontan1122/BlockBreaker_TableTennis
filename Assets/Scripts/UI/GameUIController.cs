using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIController : MonoBehaviour
{
    [SerializeField, Header("�X�e�[�W�e�L�X�g")]
    private TextMeshProUGUI stageText;

    void Start()
    {
        stageText.text = "Stage0";
    }

    public void ChangeStageText(int level)
    {
        stageText.text = "Stage" + level.ToString();
    }
}
