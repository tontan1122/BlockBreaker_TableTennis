using UnityEngine;

public class BlockWave : MonoBehaviour
{

    [SerializeField, Header("残りブロック数")]
    private int remainingBlock;

    [SerializeField, Header("壊れないブロックの数")]
    private int NoBreakBlocks = 0;

    void Start()
    {
        remainingBlock = this.transform.childCount;
    }

    private void Update()
    {
        remainingBlock = this.transform.childCount;

        if (remainingBlock == NoBreakBlocks)    //残りブロック数と壊れないブロックの数が同じなら
        {
            //クリア判定を出す
            BlockManager blockManager = FindObjectOfType<BlockManager>();
            Debug.Log("ステージクリア");
            blockManager.IsClear = true;
            Destroy(this.gameObject);
        }
    }
}
