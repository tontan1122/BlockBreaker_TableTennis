using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUIController : MonoBehaviour
{
    [SerializeField, Header("ポーズパネル")]
    private GameObject pausePanel;

    [SerializeField, Header("ポーズボタン")]
    private Button pauseButton;
    
    [SerializeField, Header("ポーズ解除ボタン")]
    private Button resumeButton;

    void Start()
    {
        pausePanel.SetActive(false);

        pauseButton.onClick.AddListener(Pause);

        resumeButton.onClick.AddListener(Resume);
    }

    /// <summary>
    /// ポーズボタンを押したとき
    /// </summary>
    private void Pause()
    {
        Time.timeScale = 0.0f;
        pausePanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// ポーズを解除するとき
    /// </summary>
    public void Resume()
    {
        Time.timeScale = 1.0f;
        pausePanel.SetActive(false);
    }
}
