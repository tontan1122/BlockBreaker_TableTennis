using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameObject���ė��p���邽�߂̃N���X
/// </summary>
public class ObjectPool : MonoBehaviour
{

    private List<GameObject> objects = new List<GameObject>();

    /// <summary>
    /// �v�[������I�u�W�F�N�g���w�肵�āA�����v�[�����쐬
    /// </summary>
    internal void CreateInitialPool(GameObject orizinal, int objectNum = 1)
    {
        //�I���W�i�����v�[���ɒǉ����A��͂�����R�s�[
        orizinal.SetActive(false);
        Release(orizinal);

        List<GameObject> copyObjects = new List<GameObject>();
        for (int i = 0; i < objectNum; i++)
        {
            copyObjects.Add(Get());
        }
        foreach (GameObject copy in copyObjects)
        {
            Release(copy);
        }
    }

    /// <summary>
    /// �I�u�W�F�N�g���擾
    /// </summary>
    private GameObject Get()
    {
        GameObject target = null;

        //1�����Ȃ���Ε����A����΃��X�g����擾
        if (objects.Count == 1)
        {
            target = Instantiate(objects[0], transform);
        }
        else
        {
            target = objects[objects.Count - 1];
            objects.Remove(target);
        }

        target.SetActive(true);

        return target;
    }

    /// <summary>
    /// �I�u�W�F�N�g���R���|�[�l���g���w�肵�Ď擾
    /// </summary>
    internal T Get<T>() where T : Component
    {
        return Get().GetComponent<T>();
    }

    /// <summary>
    /// �g�p�����I�u�W�F�N�g��߂�
    /// </summary>
    internal void Release(GameObject target)
    {
        //��\���ɂ��A�v�[���֒ǉ�
        target.SetActive(false);
        target.transform.SetParent(transform);

        objects.Add(target);
    }

    /// <summary>
    /// �g�p���Ă���I�u�W�F�N�g�����ׂĔ�\���ɂ���
    /// </summary>
    internal void AllHidden(Transform parentObject)
    {
        foreach (Transform childObject in parentObject)
        {
            childObject.gameObject.SetActive(false);
        }
    }
}
