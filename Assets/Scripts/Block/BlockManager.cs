using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField, Header("ブロックのレベル")]
    private GameObject[] wave;

    private bool isClear = false;

    private GameObject cloneObject;

    private int currentLevel = 0;

    public void BlockGeneration(int level, int cleared)
    {
        cleared--;
        currentLevel = level;
        currentLevel--;
        cloneObject = Instantiate(wave[currentLevel], new Vector3(0, cleared * 15, 0), Quaternion.identity);
    }

    public void BlockReset(int cleared)
    {
        Destroy(cloneObject);
        cleared--;
        cloneObject = Instantiate(wave[currentLevel], new Vector3(0, cleared * 15, 0), Quaternion.identity);
    }

    public bool IsClear
    {
        set { isClear = value; }
        get { return isClear; }
    }
}
