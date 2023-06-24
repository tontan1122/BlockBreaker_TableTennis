using UnityEngine;

public class BlockWave : MonoBehaviour
{

    [SerializeField, Header("�c��u���b�N��")]
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
            //�N���A������o��
            BlockManager blockManager = FindObjectOfType<BlockManager>();
            Debug.Log("�X�e�[�W�N���A");
            blockManager.IsClear = true;
            Destroy(this.gameObject);
        }
    }
}
