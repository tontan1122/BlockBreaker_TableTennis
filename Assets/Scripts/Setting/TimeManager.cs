using UnityEngine;

/// <summary>
/// éûä‘ä«óù
/// </summary>
internal class TimeManager
{
    internal void ChengeTimeScale(bool isCheck)
    {
        if (isCheck)
        {
            Time.timeScale = 1.5f;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }
}
