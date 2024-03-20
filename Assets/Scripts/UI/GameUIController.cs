using UnityEngine;
using TMPro;

/// <summary>
/// �Q�[����UI�̑���N���X
/// </summary>
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

    /// <summary>
    /// �X�e�[�W���\�L�̕ύX
    /// </summary>
    /// <param name="level">���x���ԍ�</param>
    public void ChangeStageText(int level)
    {
        stageText.text = "Stage" + level.ToString();
    }

    /// <summary>
    /// �~�X���\�L�̕ύX
    /// </summary>
    /// <param name="missCount">�~�X��</param>
    public void ChengeMissCountText(int missCount)
    {
        missText.text = missCount.ToString() + "Miss";
    }
}
