using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// ヒントをセーブするクラス
/// </summary>
public class HintSave : MonoBehaviour
{
    [SerializeField, Header("保存するステージの番号")]
    private int stageNumber = 1;

    [SerializeField,Header("ボールオブジェクト")]
    private GameObject ballObject;

    private List<Vector2> ballPositionList = new List<Vector2>();

    private StreamWriter streamWriter;

    private int frameCount = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            string filePath = Path.Combine(Application.dataPath, "Resources/CSV/hint/", stageNumber.ToString() + ".csv");
            // 保存したファイルの中身を削除
            if (File.Exists(filePath))
            {
                // ファイルを開いて中身を空にする
                File.WriteAllText(filePath, string.Empty);
            }

            // 新しいファイルの作成、すでにある場合は上書き
            FileInfo fi = new FileInfo(filePath);

            streamWriter = fi.AppendText();

            string[] s1 = { "X", "Y" };

            string s2 = string.Join(",", s1);

            streamWriter.WriteLine(s2);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("保存中");
            ballPositionList.Add(ballObject.transform.position);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            Debug.Log("ポジションを保存");
            WritePosition(ballPositionList);

            streamWriter.Close();
        }
    }

    private void OnApplicationQuit()
    {
        string filePath = Path.Combine(Application.dataPath, "Resources/CSV/hint/", stageNumber.ToString() + ".csv");
        List<string> lines = new List<string>(File.ReadAllLines(filePath));

        // 最後の行を削除→X座標のみを保存してしまう等保存がうまくできていない場合が多いため
        if (lines.Count > 0)
        {
            lines.RemoveAt(lines.Count - 1);
        }
        else
        {
            Debug.LogError("正常に保存できていません");
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
