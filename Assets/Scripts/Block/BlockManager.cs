using UnityEngine;

/// <summary>
/// ブロックの管理クラス
/// </summary>
public class BlockManager : MonoBehaviour
{
    [SerializeField, Header("ブロックのレベル")]
    private GameObject[] blockLevels;

    private GameObject cloneObject;

    private bool isClear = false;

    //現在のレベル
    private int currentLevel = 0;

    /// <summary>
    /// ブロックの生成
    /// </summary>
    /// <param name="level">生成したいブロックのレベル</param>
    /// <param name="cleared">連続でクリアしている数</param>
    internal void GenerateBlock(int level, int cleared)
    {
        cleared--;
        currentLevel = level;
        currentLevel--;
        cloneObject = Instantiate(blockLevels[currentLevel], new Vector3(0, cleared * GlobalConst.STAGE_SIZE_Y, 0), Quaternion.identity);
    }

    internal void DestroyBlock()
    {
        if (cloneObject != null)
        {
            Destroy(cloneObject);
        }
    }

    /// <summary>
    /// ブロックのリセット
    /// </summary>
    /// <param name="cleared">連続クリア数</param>
    internal void ResetBlock(int cleared)
    {
        Destroy(cloneObject);
        cleared--;
        cloneObject = Instantiate(blockLevels[currentLevel], new Vector3(0, cleared * GlobalConst.STAGE_SIZE_Y, 0), Quaternion.identity);
    }

    internal bool IsClear
    {
        set { isClear = value; }
        get { return isClear; }
    }
}
