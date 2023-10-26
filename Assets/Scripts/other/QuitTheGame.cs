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

    //Q[IΉ
    internal void EndGame()
    {
        //Escͺ³κ½
        if (Input.GetKey(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//Q[vCIΉ
#else
    Application.Quit();//Q[vCIΉ
#endif
        }

    }
}
