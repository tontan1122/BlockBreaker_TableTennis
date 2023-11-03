using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 終了時のパネル操作
/// </summary>
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
    }

    public void QuitButtonClick()
    {
        QuitTheGame.GetInstance.EndGame();
    }
}