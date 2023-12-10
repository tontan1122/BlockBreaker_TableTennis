using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameObjectを再利用するためのクラス
/// </summary>
public class ObjectPool : MonoBehaviour
{

    private List<GameObject> objects = new List<GameObject>();

    /// <summary>
    /// プールするオブジェクトを指定して、初期プールを作成
    /// </summary>
    internal void CreateInitialPool(GameObject orizinal, int objectNum = 1)
    {
        //オリジナルをプールに追加し、後はそれをコピー
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
    /// オブジェクトを取得
    /// </summary>
    private GameObject Get()
    {
        GameObject target = null;

        //1つしかなければ複製、あればリストから取得
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

        objects.Add(target);
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
