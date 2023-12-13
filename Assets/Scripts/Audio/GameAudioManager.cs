using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームのシステムオーディオ
/// </summary>
internal class GameAudioManager : AudioManager
{
    /// <summary>
    /// SEの再生
    /// </summary>
    /// <param name="SENumber">
    /// 0：ボタンクリックSE
    /// 1：ステージ移動SE
    /// 2：ステージクリアSE
    /// </param>
    public void PlayGameSE(int SENumber)
    {
        audioSource.PlayOneShot(soundEffects[SENumber]);
    }
}
