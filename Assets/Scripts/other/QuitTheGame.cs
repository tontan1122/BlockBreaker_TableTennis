using UnityEngine;

public class QuitTheGame : MonoBehaviour
{
    void Update()
    {
        EndGame();
    }

    //�Q�[���I��
    private void EndGame()
    {
        //Esc�������ꂽ��
        if (Input.GetKey(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
    Application.Quit();//�Q�[���v���C�I��
#endif
        }

    }
}
