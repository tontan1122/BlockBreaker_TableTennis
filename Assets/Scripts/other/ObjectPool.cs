using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameObject���ė��p���邽�߂̃N���X
/// </summary>
public class ObjectPool : MonoBehaviour
{

    private List<GameObject> objectList = new List<GameObject>();

    /// <summary>
    /// �v�[������I�u�W�F�N�g���w�肵�āA�����v�[�����쐬
    /// </summary>
    public void CreateInitialPool(GameObject orizinal, int objectNum = 1)
    {
        //�I���W�i�����v�[���ɒǉ����A��͂�����R�s�[
        orizinal.SetActive(false);
        Release(orizinal);

        List<GameObject> copyObjectList = new List<GameObject>();
        for (int i = 0; i < objectNum; i++)
        {
            copyObjectList.Add(Get());
        }
        foreach (GameObject copy in copyObjectList)
        {
            Release(copy);
        }
    }

    /// <summary>
    /// �I�u�W�F�N�g���擾
    /// </summary>
    public GameObject Get()
    {
        GameObject target = null;

        //1�����Ȃ���Ε����A����΃��X�g����擾
        if (objectList.Count == 1)
        {
            target = Instantiate(objectList[0], transform);
        }
        else
        {
            target = objectList[objectList.Count - 1];
            objectList.Remove(target);
        }

        target.SetActive(true);

        return target;
    }

    /// <summary>
    /// �I�u�W�F�N�g���R���|�[�l���g���w�肵�Ď擾
    /// </summary>
    public T Get<T>() where T : Component
    {
        return Get().GetComponent<T>();
    }

    /// <summary>
    /// �g�p�����I�u�W�F�N�g��߂�
    /// </summary>
    public void Release(GameObject target)
    {
        //��\���ɂ��A�v�[���֒ǉ�
        target.SetActive(false);
        target.transform.SetParent(transform);

        objectList.Add(target);
    }

}
