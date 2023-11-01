using UnityEngine;
using UnityEngine.UI;

public class QuitPanelController : MonoBehaviour
{
    [SerializeField, Header("終了パネル")]
    private GameObject quitPanel;

    [SerializeField, Header("終了ボタン")]
    private Button quitButton;

    [SerializeField,Header("再生ボタン")]
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