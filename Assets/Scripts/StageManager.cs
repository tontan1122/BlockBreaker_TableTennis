using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField, Header("レベル別ステージオブジェクト")]
    private GameObject[] stageObject;

    void Start()
    {
        
    }

    public void StageGeneration(int level)
    {
        Instantiate(stageObject[level], new Vector3(0, 0, 0), Quaternion.identity);
    }
}
