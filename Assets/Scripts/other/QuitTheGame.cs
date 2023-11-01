using UnityEngine;

internal class QuitTheGame
{
    private static QuitTheGame instance = null;

    internal static QuitTheGame GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = new QuitTheGame();
            }
            return instance;
        }
    }

    //ゲーム終了
    internal void EndGame()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }
}
