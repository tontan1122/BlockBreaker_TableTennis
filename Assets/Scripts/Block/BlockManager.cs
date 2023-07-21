using UnityEngine;

/// <summary>
/// ブロックの管理クラス
/// </summary>
public class BlockManager : MonoBehaviour
{
    [SerializeField, Header("ブロックのレベル")]
    private GameObject[] wave;

    private GameObject cloneObject;
        
    private bool isClear = false;

    //現在のレベル
    private int currentLevel = 0;

    /// <summary>
    /// ブロックの生成
    /// </summary>
    /// <param name="level">生成したいブロックのレベル</param>
    /// <param name="cleared">連続でクリアしている数</param>
    public void BlockGeneration(int level, int cleared)
    {
        cleared--;
        currentLevel = level;
        currentLevel--;
        cloneObject = Instantiate(wave[currentLevel], new Vector3(0, cleared * 15, 0), Quaternion.identity);
    }

    /// <summary>
    /// ブロックのリセット
    /// </summary>
    /// <param name="cleared">連続クリア数</param>
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
