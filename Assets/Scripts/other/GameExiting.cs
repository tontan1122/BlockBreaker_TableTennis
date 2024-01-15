using UnityEngine;

/// <summary>
/// �Q�[���̏I��
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

    //�Q�[���I��
    public void EndGame()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
    Application.Quit();//�Q�[���v���C�I��
#endif
    }
}
