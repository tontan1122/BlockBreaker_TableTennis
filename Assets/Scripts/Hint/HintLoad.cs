using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// ヒントをロードして座標型変数に直すクラス
/// </summary>
public class HintLoad : MonoBehaviour
{

    public List<Vector2> LoadHintData(int stageNum)
    {
        //ファイル名
        string fileName;

        fileName = stageNum.ToString();

        //一時入力用で毎回初期化する構造体とリスト
        Vector2 pos = new Vector2();
        List<Vector2> positionList = new List<Vector2>();


        //ResourcesからCSVを読み込むのに必要
        TextAsset csvFile;

        //読み込んだCSVファイルを格納
        List<string[]> csvDatas = new List<string[]>();

        //CSVファイルの行数を格納
        int height = 0;

        //for文用。一行目は読み込まない
        int i = 1;

        /* Resouces/CSV下のCSV読み込み */
        csvFile = Resources.Load("CSV/hint/" + fileName) as TextAsset;
        //読み込んだテキストをString型にして格納
        StringReader reader = new StringReader(csvFile.text);
        while (reader.Peek() > -1)  //読み込むものがなくなるまで
        {
            string line = reader.ReadLine();
            // ,で区切ってCSVに格納
            csvDatas.Add(line.Split(','));
            height++; // 行数加算
        }
        for (i = 1; i < height; i++)
        {
            //[i]は行数。[0]~[1]は列数
            pos.x = Convert.ToSingle(csvDatas[i][0]);
            pos.y = Convert.ToSingle(csvDatas[i][1]);

            //戻り値のリストに加える
            positionList.Add(pos);
        }
        return positionList;
    }
}
