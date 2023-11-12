using System.Collections.Generic;
using UnityEngine;

public class BallAfterImage : MonoBehaviour
{
    [SerializeField, Header("�{�[���̎c���摜")]
    private Sprite afterImage;

    [SerializeField, Header("�c���̊Ԋu")]
    private int timeInterval = 10;

    [SerializeField, Header("�c���̖���")]
    private int maxAfterImageNumber = 5;

    private List<GameObject> afterImages = new List<GameObject>();

    private GameObject parentObject;    // �c���I�u�W�F�N�g���q�Ɏ��e�I�u�W�F�N�g
    //private ObjectPool objectPool;

    private int currentCount = 0;

    void Start()
    {
        parentObject = new GameObject("AfterImageObjects");
        //objectPool = parentObject.AddComponent<ObjectPool>();    // �e�I�u�W�F�N�g��ObjectPool�N���X��������
        //GameObject spriteObject = new GameObject("GeneratedSprite");
        //objectPool.CreateInitialPool(spriteObject, maxAfterImageNumber);
    }

    private void ImageColorChange(SpriteRenderer ImageRenderer)
    {
        //ImageRenderer.color = Color.gray;
        ImageRenderer.color = new Color32(172, 172, 172, 100);
    }

    internal void DrawAfterImage(Transform imageTransform)
    {
        if (!WaitTimeInterval())
        {
            return;
        }

        GameObject spriteObject = new GameObject("GeneratedSprite");
        afterImages.Add(spriteObject);

        parentObject.transform.parent = gameObject.transform;   // �q�I�u�W�F�N�g��

        SetTransformAfterImage(imageTransform);
        AfterImageDestroy();

        SpriteRenderer spriteRenderer = spriteObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = afterImage;
        ImageColorChange(spriteRenderer);
    }

    private bool WaitTimeInterval()
    {
        currentCount++;
        if(currentCount > timeInterval) 
        {
            currentCount = 0;
            return true;
        }
        return false;
    }

    /// <summary>
    /// �c����Transform�ݒ�
    /// </summary>
    /// <param name="imageTransform"></param>
    private void SetTransformAfterImage(Transform imageTransform)
    {
        GameObject operateObject = afterImages[afterImages.Count - 1];  // ���X�g�̍Ō�ɂ�����̂𑀍삷�邽��
        operateObject.transform.position = imageTransform.position; // �ʒu��ݒ�
        operateObject.transform.rotation = imageTransform.rotation; // �p�x��ݒ�
        operateObject.transform.localScale = new Vector3(0.5f, 0.5f, 1); // �傫����ݒ�
    }

    /// <summary>
    /// �c���̍폜
    /// </summary>
    private void AfterImageDestroy()
    {
        if (afterImages.Count > maxAfterImageNumber)
        {
            Destroy(afterImages[0].gameObject);
            afterImages.RemoveAt(0);
        }
    }
}
