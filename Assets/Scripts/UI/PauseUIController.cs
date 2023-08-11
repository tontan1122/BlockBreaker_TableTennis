using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUIController : MonoBehaviour
{
    [SerializeField, Header("�|�[�Y�p�l��")]
    private GameObject pausePanel;

    [SerializeField, Header("�|�[�Y�{�^��")]
    private Button pauseButton;
    
    [SerializeField, Header("�|�[�Y�����{�^��")]
    private Button resumeButton;

    void Start()
    {
        pausePanel.SetActive(false);

        pauseButton.onClick.AddListener(Pause);

        resumeButton.onClick.AddListener(Resume);
    }

    /// <summary>
    /// �|�[�Y�{�^�����������Ƃ�
    /// </summary>
    private void Pause()
    {
        Time.timeScale = 0.0f;
        pausePanel.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// �|�[�Y����������Ƃ�
    /// </summary>
    public void Resume()
    {
        Time.timeScale = 1.0f;
        pausePanel.SetActive(false);
    }
}
