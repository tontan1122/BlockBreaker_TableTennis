using UnityEngine;

/// <summary>
/// ゲームの終了
/// </summary>
internal class GameExiting
{
    private static GameExiting instance = null;

    internal static GameExiting GetInstance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameExiting();
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
