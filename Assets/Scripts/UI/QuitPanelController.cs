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

    private void Start()
    {
        quitPanel.SetActive(false);
        quitButton.onClick.AddListener(PushQuitButton);
    }

    /// <summary>
    /// ゲームの終了ボタンを押した際の処理
    /// </summary>
    private void PushQuitButton()
    {
        GameExiting.GetInstance.EndGame();
    }
}