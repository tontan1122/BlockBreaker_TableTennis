using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �I�����̃p�l������
/// </summary>
public class QuitPanelController : MonoBehaviour
{
    [SerializeField, Header("�I���p�l��")]
    private GameObject quitPanel;

    [SerializeField, Header("�I���{�^��")]
    private Button quitButton;

    private void Start()
    {
        quitPanel.SetActive(false);
        quitButton.onClick.AddListener(PushQuitButton);
    }

    private void PushQuitButton()
    {
        GameExiting.GetInstance.EndGame();
    }
}