using System;
using UniRx;
using UnityEngine;

/// <summary>
/// ポーズ表示、管理クラス
/// </summary>
public class PauseUIController : MonoBehaviour
{
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

    /// <summary>
    /// ポーズボタンを押したとき
    /// </summary>
    public void Pause()
    {
        isPaused = true;
        pauseSubject.OnNext("pause");
    }

    /// <summary>
    /// ポーズを解除するとき
    /// </summary>
    public void Resume()
    {
        isPaused = false;
        resumeSubject.OnNext("resume");
    }

    /// <summary>
    /// ポーズしているかどうか
    /// </summary>
    public static bool IsPaused
    {
        get { return isPaused; }
    }
}
