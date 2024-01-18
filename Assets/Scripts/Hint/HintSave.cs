using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// �q���g���Z�[�u����N���X
/// </summary>
public class HintSave : MonoBehaviour
{
    [SerializeField, Header("�ۑ�����X�e�[�W�̔ԍ�")]
    private int stageNumber = 1;

    [SerializeField,Header("�{�[���I�u�W�F�N�g")]
    private GameObject ballObject;

    private List<Vector2> ballPositionList = new List<Vector2>();

    private StreamWriter streamWriter;

    private int frameCount = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            string filePath = Path.Combine(Application.dataPath, "Resources/CSV/hint/", stageNumber.ToString() + ".csv");
            // �ۑ������t�@�C���̒��g���폜
            if (File.Exists(filePath))
            {
                // �t�@�C�����J���Ē��g����ɂ���
                File.WriteAllText(filePath, string.Empty);
            }

            // �V�����t�@�C���̍쐬�A���łɂ���ꍇ�͏㏑��
            FileInfo fi = new FileInfo(filePath);

            streamWriter = fi.AppendText();

            string[] s1 = { "X", "Y" };

            string s2 = string.Join(",", s1);

            streamWriter.WriteLine(s2);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("�ۑ���");
            ballPositionList.Add(ballObject.transform.position);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            Debug.Log("�|�W�V������ۑ�");
            WritePosition(ballPositionList);

            streamWriter.Close();
        }
    }

    private void OnApplicationQuit()
    {
        string filePath = Path.Combine(Application.dataPath, "Resources/CSV/hint/", stageNumber.ToString() + ".csv");
        List<string> lines = new List<string>(File.ReadAllLines(filePath));

        // �Ō�̍s���폜��X���W�݂̂�ۑ����Ă��܂����ۑ������܂��ł��Ă��Ȃ��ꍇ����������
        if (lines.Count > 0)
        {
            lines.RemoveAt(lines.Count - 1);
        }
        else
        {
            Debug.LogError("����ɕۑ��ł��Ă��܂���");
        }
    }

    private void WritePosition(List<Vector2> position)
    {
        for (frameCount = 0; frameCount < position.Count; frameCount++)
        {
            string[] s1 = { position[frameCount].x.ToString(), position[frameCount].y.ToString() };

            string s2 = string.Join(",", s1);

            streamWriter.WriteLine(s2);
        }
    }
}
