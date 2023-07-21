using UnityEngine;

/// <summary>
/// �u���b�N�̊Ǘ��N���X
/// </summary>
public class BlockManager : MonoBehaviour
{
    [SerializeField, Header("�u���b�N�̃��x��")]
    private GameObject[] wave;

    private GameObject cloneObject;
        
    private bool isClear = false;

    //���݂̃��x��
    private int currentLevel = 0;

    /// <summary>
    /// �u���b�N�̐���
    /// </summary>
    /// <param name="level">�����������u���b�N�̃��x��</param>
    /// <param name="cleared">�A���ŃN���A���Ă��鐔</param>
    public void BlockGeneration(int level, int cleared)
    {
        cleared--;
        currentLevel = level;
        currentLevel--;
        cloneObject = Instantiate(wave[currentLevel], new Vector3(0, cleared * 15, 0), Quaternion.identity);
    }

    /// <summary>
    /// �u���b�N�̃��Z�b�g
    /// </summary>
    /// <param name="cleared">�A���N���A��</param>
    public void BlockReset(int cleared)
    {
        Destroy(cloneObject);
        cleared--;
        cloneObject = Instantiate(wave[currentLevel], new Vector3(0, cleared * 15, 0), Quaternion.identity);
    }

    public bool IsClear
    {
        set { isClear = value; }
        get { return isClear; }
    }
}
