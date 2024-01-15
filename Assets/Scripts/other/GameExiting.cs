using UnityEngine;

/// <summary>
/// ゲームの終了
/// </summary>
public class GameExiting
{
    private static GameExiting instance = null;

    public static GameExiting GetInstance
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
    public void EndGame()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
    }
}
