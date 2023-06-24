using UnityEngine;

public class BlockWave : MonoBehaviour
{

    [SerializeField, Header("残りブロック数")]
    private int remainingBlock;

    void Start()
    {
        remainingBlock = this.transform.childCount;
    }

    private void Update()
    {
        remainingBlock = this.transform.childCount;

        if (remainingBlock == 0)
        {
            //クリア判定を出す
            BlockManager blockManager = FindObjectOfType<BlockManager>();
            Debug.Log("ステージクリア");
            blockManager.IsClear = true;
            Destroy(this.gameObject);
        }
    }
}
