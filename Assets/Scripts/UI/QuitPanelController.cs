using UnityEngine;
using UnityEngine.UI;

public class QuitPanelController : MonoBehaviour
{
    [SerializeField, Header("�I���p�l��")]
    private GameObject quitPanel;

    [SerializeField, Header("�I���{�^��")]
    private Button quitButton;

    [SerializeField,Header("�Đ��{�^��")]
    private Button startButton;

    private void Start()
    {
        quitPanel.SetActive(false);
        quitButton.onClick.AddListener(QuitButtonClick);
        startButton.onClick.AddListener(StartButtonClick);
    }

    public void QuitButtonClick()
    {
        QuitTheGame.GetInstance.EndGame();
    }

    public void StartButtonClick()
    {

    }
}