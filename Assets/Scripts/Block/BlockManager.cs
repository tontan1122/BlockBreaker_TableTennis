using UnityEngine;

/// <summary>
/// �u���b�N�̊Ǘ��N���X
/// </summary>
public class BlockManager : MonoBehaviour
{
    [SerializeField, Header("�u���b�N�̃��x��")]
    private GameObject[] blockLevels;

    private GameObject cloneObject;

    private bool isClear = false;

    //���݂̃��x��
    private int currentLevel = 0;

    /// <summary>
    /// �u���b�N�̐���
    /// </summary>
    /// <param name="level">�����������u���b�N�̃��x��</param>
    /// <param name="cleared">�A���ŃN���A���Ă��鐔</param>
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
    /// �u���b�N�̃��Z�b�g
    /// </summary>
    /// <param name="cleared">�A���N���A��</param>
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
