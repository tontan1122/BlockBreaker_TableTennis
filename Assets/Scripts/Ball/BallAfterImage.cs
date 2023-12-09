using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �{�[���̎c�������N���X
/// </summary>
public class BallAfterImage : MonoBehaviour
{
    [SerializeField, Header("�{�[���̎c���摜")]
    private Sprite afterImage;

    [SerializeField, Header("�c���̊Ԋu")]
    private int timeInterval = 10;

    [SerializeField, Header("�c���̖���")]
    private int maxAfterImageNumber = 5;

    [SerializeField, Header("�c���̐F")]
    private byte afterImageColor = 172;

    [SerializeField, Header("�c���̓����x")]
    private byte afterImageAlphaNum = 100;
    [SerializeField, Header("fade���x")]
    private int fadeSpeed = 3;

    private List<GameObject> afterImages = new List<GameObject>();

    private GameObject parentObject;    // �c���I�u�W�F�N�g���q�Ɏ��e�I�u�W�F�N�g
    private ObjectPool objectPool;

    private int currentCount = 0;

    void Start()
    {
        parentObject = new GameObject("AfterImageObjects");
        objectPool = parentObject.AddComponent<ObjectPool>();    // �e�I�u�W�F�N�g��ObjectPool�N���X��������
        objectPool.CreateInitialPool(GenerateInitialAfterImage(), maxAfterImageNumber);
    }

    /// <summary>
    /// �c���̕`��
    /// </summary>
    /// <param name="imageTransform"></param>
    internal void DrawAfterImage(Transform imageTransform)
    {
        if (!WaitGenerationTime())
        {
            return;
        }
        SpriteRenderer generateObject = objectPool.Get<SpriteRenderer>();
        afterImages.Add(generateObject.gameObject);
        ConvertImageColor(generateObject);
        generateObject.gameObject.GetComponent<FadeSprite>().SetfadeSpeed = fadeSpeed;

        GameObject operateObject = afterImages[afterImages.Count - 1];  // ���X�g�̍Ō�ɂ�����̂𑀍삷�邽��
        operateObject.transform.position = imageTransform.position; // �ʒu��ݒ�
        operateObject.transform.rotation = imageTransform.rotation; // �p�x��ݒ�
        operateObject.transform.localScale = new Vector3(0.5f, 0.5f, 1); // �傫����ݒ�

        // �c���̍폜
        if (afterImages.Count > maxAfterImageNumber)
        {
            objectPool.Release(afterImages[0].gameObject);
            afterImages.RemoveAt(0);
        }
    }

    private void ConvertImageColor(SpriteRenderer ImageRenderer)
    {
        ImageRenderer.color = new Color32(afterImageColor, afterImageColor, afterImageColor, afterImageAlphaNum);
    }

    /// <summary>
    /// �����܂ł̎��Ԋm��
    /// </summary>
    /// <returns></returns>
    private bool WaitGenerationTime()
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
    /// �c���̃I���W�i���I�u�W�F�N�g�̍쐬
    /// </summary>
    /// <returns></returns>
    private GameObject GenerateInitialAfterImage()
    {
        GameObject spriteObject = new GameObject("GeneratedSprite");
        // �R���|�[�l���g�̒ǉ�
        spriteObject.AddComponent<FadeSprite>();
        SpriteRenderer spriteRenderer = spriteObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = afterImage;
        ConvertImageColor(spriteRenderer);
        return spriteObject;
    }

    /// <summary>
    /// �\�������Ă���c���̑S�폜
    /// </summary>
    internal void RemoveAllAfterImage()
    {
        objectPool.AllHidden(parentObject.transform);
    }
}
