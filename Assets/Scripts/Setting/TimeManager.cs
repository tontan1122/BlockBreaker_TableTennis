using UnityEngine;

/// <summary>
/// ���ԊǗ�
/// </summary>
public class TimeManager : MonoBehaviour
{
    internal void TimeChenger(bool isCheck)
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
