using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �{�[���̎c�������N���X
/// </summary>
public class BallAfterImage : MonoBehaviour
{
    [SerializeField, Header("�{�[���̎c���摜")]
    private Sprite afterImage;

    private static readonly int TIME_INTERVAL = 2;  // �c���̊Ԋu
    private static readonly int MAX_AFTER_IMAGE_NUMBER = 5;    // �c���̖���
    private static readonly byte AFTER_IMAGE_COLOR = 172;   // �c���̐F
    private static readonly byte AFTER_IMAGE_ALPHA_NUM = 150;  // �c���̓����x
    private static readonly int FADE_SPEED = 8;              // �t�F�[�h���x

    private List<GameObject> afterImages = new List<GameObject>();

    private GameObject parentObject;    // �c���I�u�W�F�N�g���q�Ɏ��e�I�u�W�F�N�g
    private ObjectPool objectPool;

    private int currentCount = 0;

    void Start()
    {
        parentObject = new GameObject("AfterImageObjects");
        objectPool = parentObject.AddComponent<ObjectPool>();    // �e�I�u�W�F�N�g��ObjectPool�N���X��������
        objectPool.CreateInitialPool(GenerateInitialAfterImage(), MAX_AFTER_IMAGE_NUMBER);
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
        generateObject.gameObject.GetComponent<FadeSprite>().SetfadeSpeed = FADE_SPEED;

        GameObject operateObject = afterImages[afterImages.Count - 1];  // ���X�g�̍Ō�ɂ�����̂𑀍삷�邽��
        operateObject.transform.position = imageTransform.position; // �ʒu��ݒ�
        operateObject.transform.rotation = imageTransform.rotation; // �p�x��ݒ�
        operateObject.transform.localScale = new Vector3(0.5f, 0.5f, 1); // �傫����ݒ�

        // �c���̍폜
        if (afterImages.Count > MAX_AFTER_IMAGE_NUMBER)
        {
            objectPool.Release(afterImages[0].gameObject);
            afterImages.RemoveAt(0);
        }
    }

    private void ConvertImageColor(SpriteRenderer ImageRenderer)
    {
        ImageRenderer.color = new Color32(AFTER_IMAGE_COLOR, AFTER_IMAGE_COLOR, AFTER_IMAGE_COLOR, AFTER_IMAGE_ALPHA_NUM);
    }

    /// <summary>
    /// �����܂ł̎��Ԋm��
    /// </summary>
    /// <returns></returns>
    private bool WaitGenerationTime()
    {
        currentCount++;
        if(currentCount > TIME_INTERVAL) 
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
