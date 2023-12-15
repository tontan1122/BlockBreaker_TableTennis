using UnityEngine;

/// <summary>
/// �Q�[���̏I��
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

    //�Q�[���I��
    internal void EndGame()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
    Application.Quit();//�Q�[���v���C�I��
#endif
    }
}
