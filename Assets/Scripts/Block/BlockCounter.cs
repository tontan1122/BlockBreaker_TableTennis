using UnityEngine;

/// <summary>
/// ブロックの残り個数を数える
/// </summary>
public class BlockCounter : MonoBehaviour
{
    [SerializeField, Header("壊せないオブジェクトの数")]
    private int noBreakBlocks = 0;

    // 残りブロック数
    private int remainingBlockcount;

    void Start()
    {
        remainingBlockcount = this.transform.childCount;
    }

    private void Update()
    {
        remainingBlockcount = this.transform.childCount;

        if (remainingBlockcount == noBreakBlocks)    // 残りブロック数と壊れないブロックの数が同じなら
        {
            // クリア判定を出す
            BlockManager blockManager = FindObjectOfType<BlockManager>();
            blockManager.IsClear = true;
            Destroy(this.gameObject);
        }
    }
}
