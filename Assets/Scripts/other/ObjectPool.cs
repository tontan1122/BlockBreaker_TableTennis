using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameObjectを再利用するためのクラス
/// </summary>
public class ObjectPool : MonoBehaviour
{

    private List<GameObject> objectList = new List<GameObject>();

    /// <summary>
    /// プールするオブジェクトを指定して、初期プールを作成
    /// </summary>
    internal void CreateInitialPool(GameObject orizinal, int objectNum = 1)
    {
        //オリジナルをプールに追加し、後はそれをコピー
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
    /// オブジェクトを取得
    /// </summary>
    internal GameObject Get()
    {
        GameObject target = null;

        //1つしかなければ複製、あればリストから取得
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
    /// オブジェクトをコンポーネントを指定して取得
    /// </summary>
    internal T Get<T>() where T : Component
    {
        return Get().GetComponent<T>();
    }

    /// <summary>
    /// 使用したオブジェクトを戻す
    /// </summary>
    internal void Release(GameObject target)
    {
        //非表示にし、プールへ追加
        target.SetActive(false);
        target.transform.SetParent(transform);

        objectList.Add(target);
    }

    /// <summary>
    /// 使用しているオブジェクトをすべて非表示にする
    /// </summary>
    internal void AllHidden(Transform parentObject)
    {
        foreach (Transform childObject in parentObject)
        {
            childObject.gameObject.SetActive(false);
        }
    }
}
