using UnityEngine;

/// <summary>
/// �u���b�N�̎c����𐔂���
/// </summary>
public class BlockCounter : MonoBehaviour
{
    [SerializeField, Header("�󂹂Ȃ��I�u�W�F�N�g�̐�")]
    private int noBreakBlocks = 0;

    // �c��u���b�N��
    private int remainingBlockcount;

    void Start()
    {
        remainingBlockcount = this.transform.childCount;
    }

    private void Update()
    {
        remainingBlockcount = this.transform.childCount;

        if (remainingBlockcount == noBreakBlocks)    // �c��u���b�N���Ɖ��Ȃ��u���b�N�̐��������Ȃ�
        {
            // �N���A������o��
            BlockManager blockManager = FindObjectOfType<BlockManager>();
            blockManager.IsClear = true;
            Destroy(this.gameObject);
        }
    }
}
