using UnityEngine;

/// <summary>
/// �u���b�N�̎c����𐔂���
/// </summary>
internal class BlockCounter : MonoBehaviour
{
    [SerializeField, Header("�󂹂Ȃ��I�u�W�F�N�g�̐�")]
    private int noBreakBlocks = 0;

    // �c��u���b�N��
    private int remainingBlock;

    void Start()
    {
        remainingBlock = this.transform.childCount;
    }

    private void Update()
    {
        remainingBlock = this.transform.childCount;

        if (remainingBlock == noBreakBlocks)    //�c��u���b�N���Ɖ��Ȃ��u���b�N�̐��������Ȃ�
        {
            //�N���A������o��
            BlockManager blockManager = FindObjectOfType<BlockManager>();
            blockManager.IsClear = true;
            Destroy(this.gameObject);
        }
    }
}
