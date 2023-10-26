using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

internal class PauseUIController : MonoBehaviour
{
    [SerializeField, Header("�|�[�Y�p�l��")]
    private GameObject pausePanel;

    [SerializeField, Header("�|�[�Y�{�^��")]
    private Button pauseButton;
    
    [SerializeField, Header("�|�[�Y�����{�^��")]
    private Button resumeButton;


    private static Subject<string> pauseSubject = new Subject<string>();
    private static Subject<string> resumeSubject = new Subject<string>();

    private static bool isPaused = false;   //�|�[�Y���Ă��邩�ǂ���

    internal static IObservable<string> OnPaused
    {
        get { return pauseSubject; }
    }

    internal static IObservable<string> OnResumed
    {
        get { return resumeSubject; }
    }


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
        isPaused = true;
        pauseSubject.OnNext("pause");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// �|�[�Y����������Ƃ�
    /// </summary>
    internal void Resume()
    {
        Time.timeScale = 1.0f;
        pausePanel.SetActive(false);
        isPaused = false;
        resumeSubject.OnNext("resume");
    }

    internal static bool IsPaused
    {
        get { return isPaused; }
    }
}
