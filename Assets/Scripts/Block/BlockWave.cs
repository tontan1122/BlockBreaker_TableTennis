using UnityEngine;

public class BlockWave : MonoBehaviour
{

    [SerializeField, Header("�c��u���b�N��")]
    private int remainingBlock;

    [SerializeField, Header("���Ȃ��u���b�N�̐�")]
    private int NoBreakBlocks = 0;

    void Start()
    {
        remainingBlock = this.transform.childCount;
    }

    private void Update()
    {
        remainingBlock = this.transform.childCount;

        if (remainingBlock == NoBreakBlocks)    //�c��u���b�N���Ɖ��Ȃ��u���b�N�̐��������Ȃ�
        {
            //�N���A������o��
            BlockManager blockManager = FindObjectOfType<BlockManager>();
            Debug.Log("�X�e�[�W�N���A");
            blockManager.IsClear = true;
            Destroy(this.gameObject);
        }
    }
}
