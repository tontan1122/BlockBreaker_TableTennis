using System.Collections.Generic;
using UnityEngine;

public class BallAfterImage : MonoBehaviour
{
    [SerializeField, Header("ボールの残像画像")]
    private Sprite afterImage;

    [SerializeField, Header("残像の間隔")]
    private float timeInterval = 0.5f;

    [SerializeField, Header("残像の枚数")]
    private int maxAfterImageNumber = 5;

    private List<GameObject> afterImages = new List<GameObject>();

    private GameObject parentObject;

    void Start()
    {
        parentObject = new GameObject("AfterImageObjects");
    }

    private void ImageColorChange(SpriteRenderer ImageRenderer)
    {
        //ImageRenderer.color = Color.gray;
        ImageRenderer.color = new Color32(172, 172, 172, 100);
    }

    internal void DrawAfterImage(Transform imageTransform)
    {
        GameObject spriteObject = new GameObject("GeneratedSprite");
        afterImages.Add(spriteObject);

        parentObject.transform.parent = gameObject.transform;   // 子オブジェクト化

        SetTransformAfterImage(imageTransform);
        AfterImageDestroy();

        SpriteRenderer spriteRenderer = spriteObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = afterImage;
        ImageColorChange(spriteRenderer);
    }

    /// <summary>
    /// 残像のTransform設定
    /// </summary>
    /// <param name="imageTransform"></param>
    private void SetTransformAfterImage(Transform imageTransform)
    {
        GameObject operateObject = afterImages[afterImages.Count - 1];  // リストの最後にあるものを操作するため
        operateObject.transform.position = imageTransform.position; // 位置を設定
        operateObject.transform.rotation = imageTransform.rotation; // 角度を設定
        operateObject.transform.localScale = new Vector3(0.5f, 0.5f, 1); // 大きさを設定
    }

    private void AfterImageDestroy()
    {
        if (afterImages.Count > maxAfterImageNumber)
        {
            Destroy(afterImages[0].gameObject);
            afterImages.RemoveAt(0);
        }
    }
}
