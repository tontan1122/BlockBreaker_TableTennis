using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// その他のゲームSEの再生クラス
/// </summary>
public class OtherAudioManager : AudioManager
{
    /// <summary>
    /// SEの再生
    /// </summary>
    /// <param name="SENumber">
    /// 0：ワープ入るとき
    /// 1：ワープ出るとき
    /// </param>
    internal void PlaySE(int SENumber)
    {
        audioSource.PlayOneShot(soundEffects[SENumber]);
    }
}
