using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
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


    private static Subject<string> pauseSubject = new Subject<string>();
    private static Subject<string> resumeSubject = new Subject<string>();

    private static bool isPaused = false;   //ポーズしているかどうか

    public static IObservable<string> OnPaused
    {
        get { return pauseSubject; }
    }

    public static IObservable<string> OnResumed
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
    /// ポーズボタンを押したとき
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
    /// ポーズを解除するとき
    /// </summary>
    public void Resume()
    {
        Time.timeScale = 1.0f;
        pausePanel.SetActive(false);
        isPaused = false;
        resumeSubject.OnNext("resume");
    }

    public static bool IsPaused()
    {
        return isPaused;
    }
}
