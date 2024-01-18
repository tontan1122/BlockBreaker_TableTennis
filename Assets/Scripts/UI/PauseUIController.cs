using System;
using UniRx;
using UnityEngine;

/// <summary>
/// �|�[�Y�\���A�Ǘ��N���X
/// </summary>
public class PauseUIController : MonoBehaviour
{
    private static Subject<string> pauseSubject = new Subject<string>();
    private static Subject<string> resumeSubject = new Subject<string>();

    private static bool isPaused = false;   //�|�[�Y���Ă��邩�ǂ���

    public static IObservable<string> OnPaused
    {
        get { return pauseSubject; }
    }

    public static IObservable<string> OnResumed
    {
        get { return resumeSubject; }
    }

    /// <summary>
    /// �|�[�Y�{�^�����������Ƃ�
    /// </summary>
    public void Pause()
    {
        isPaused = true;
        pauseSubject.OnNext("pause");
    }

    /// <summary>
    /// �|�[�Y����������Ƃ�
    /// </summary>
    public void Resume()
    {
        isPaused = false;
        resumeSubject.OnNext("resume");
    }

    /// <summary>
    /// �|�[�Y���Ă��邩�ǂ���
    /// </summary>
    public static bool IsPaused
    {
        get { return isPaused; }
    }
}
