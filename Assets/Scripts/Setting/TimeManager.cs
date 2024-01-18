using UnityEngine;

/// <summary>
/// ŠÔŠÇ—ƒNƒ‰ƒX
/// </summary>
public class TimeManager
{
    public void ChengeTimeScale(bool isCheck)
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
