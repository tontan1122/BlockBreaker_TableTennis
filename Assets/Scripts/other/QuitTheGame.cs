using UnityEngine;

public class QuitTheGame
{
    private static QuitTheGame instance = null;

    public static QuitTheGame GetInstance
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
    public void EndGame()
    {
        //Escが押された時
        if (Input.GetKey(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//ゲームプレイ終了
#else
    Application.Quit();//ゲームプレイ終了
#endif
        }

    }
}
