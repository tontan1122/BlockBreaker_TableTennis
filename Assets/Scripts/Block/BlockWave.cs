using UnityEngine;

public class BlockWave : MonoBehaviour
{
    [SerializeField, Header("壊せないオブジェクトの数")]
    private int noBreakBlocks = 0;

    // 残りブロック数
    private int remainingBlock;

    void Start()
    {
        remainingBlock = this.transform.childCount;
    }

    private void Update()
    {
        remainingBlock = this.transform.childCount;

        if (remainingBlock == noBreakBlocks)    //残りブロック数と壊れないブロックの数が同じなら
        {
            //クリア判定を出す
            BlockManager blockManager = FindObjectOfType<BlockManager>();
            blockManager.IsClear = true;
            Destroy(this.gameObject);
        }
    }
}
