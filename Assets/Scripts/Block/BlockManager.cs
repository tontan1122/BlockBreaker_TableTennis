using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField, Header("�u���b�N�̃��x��")]
    private GameObject[] blockWave;

    //[SerializeField, Header("���݂̃��x��")]
    //private int currentLevel = 1;


    void Start()
    {
        
    }

    public void blockGeneration(int level)
    {
        Instantiate(blockWave[level],new Vector3(0,0,0),Quaternion.identity);
    }
}
