using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// �q���g�����[�h���č��W�^�ϐ��ɒ����N���X
/// </summary>
public class HintLoad : MonoBehaviour
{

    public List<Vector2> LoadHintData(int stageNum)
    {
        //�t�@�C����
        string fileName;

        fileName = stageNum.ToString();

        //�ꎞ���͗p�Ŗ��񏉊�������\���̂ƃ��X�g
        Vector2 pos = new Vector2();
        List<Vector2> positionList = new List<Vector2>();


        //Resources����CSV��ǂݍ��ނ̂ɕK�v
        TextAsset csvFile;

        //�ǂݍ���CSV�t�@�C�����i�[
        List<string[]> csvDatas = new List<string[]>();

        //CSV�t�@�C���̍s�����i�[
        int height = 0;

        //for���p�B��s�ڂ͓ǂݍ��܂Ȃ�
        int i = 1;

        /* Resouces/CSV����CSV�ǂݍ��� */
        csvFile = Resources.Load("CSV/hint/" + fileName) as TextAsset;
        //�ǂݍ��񂾃e�L�X�g��String�^�ɂ��Ċi�[
        StringReader reader = new StringReader(csvFile.text);
        while (reader.Peek() > -1)  //�ǂݍ��ނ��̂��Ȃ��Ȃ�܂�
        {
            string line = reader.ReadLine();
            // ,�ŋ�؂���CSV�Ɋi�[
            csvDatas.Add(line.Split(','));
            height++; // �s�����Z
        }
        for (i = 1; i < height; i++)
        {
            //[i]�͍s���B[0]~[1]�͗�
            pos.x = Convert.ToSingle(csvDatas[i][0]);
            pos.y = Convert.ToSingle(csvDatas[i][1]);

            //�߂�l�̃��X�g�ɉ�����
            positionList.Add(pos);
        }
        return positionList;
    }
}
