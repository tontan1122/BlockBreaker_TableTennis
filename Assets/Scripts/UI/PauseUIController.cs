using System;
using UniRx;
using UnityEngine;

/// <summary>
/// ポーズ表示、管理クラス
/// </summary>
internal class PauseUIController : MonoBehaviour
{
    private static Subject<string> pauseSubject = new Subject<string>();
    private static Subject<string> resumeSubject = new Subject<string>();

    private static bool isPaused = false;   //ポーズしているかどうか

    internal static IObservable<string> OnPaused
    {
        get { return pauseSubject; }
    }

    internal static IObservable<string> OnResumed
    {
        get { return resumeSubject; }
    }

    /// <summary>
    /// ポーズボタンを押したとき
    /// </summary>
    internal void Pause()
    {
        isPaused = true;
        pauseSubject.OnNext("pause");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// ポーズを解除するとき
    /// </summary>
    internal void Resume()
    {
        isPaused = false;
        resumeSubject.OnNext("resume");
    }

    internal static bool IsPaused
    {
        get { return isPaused; }
    }
}
