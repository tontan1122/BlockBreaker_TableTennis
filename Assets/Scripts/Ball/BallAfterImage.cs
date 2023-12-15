using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボールの残像処理クラス
/// </summary>
public class BallAfterImage : MonoBehaviour
{
    [SerializeField, Header("ボールの残像画像")]
    private Sprite afterImage;

    private static readonly int TIME_INTERVAL = 2;  // 残像の間隔
    private static readonly int MAX_AFTER_IMAGE_NUMBER = 5;    // 残像の枚数
    private static readonly byte AFTER_IMAGE_COLOR = 172;   // 残像の色
    private static readonly byte AFTER_IMAGE_ALPHA_NUM = 150;  // 残像の透明度
    private static readonly int FADE_SPEED = 8;              // フェード速度

    private List<GameObject> afterImages = new List<GameObject>();

    private GameObject parentObject;    // 残像オブジェクトを子に持つ親オブジェクト
    private ObjectPool objectPool;

    private int currentCount = 0;

    void Start()
    {
        parentObject = new GameObject("AfterImageObjects");
        objectPool = parentObject.AddComponent<ObjectPool>();    // 親オブジェクトにObjectPoolクラスを加える
        objectPool.CreateInitialPool(GenerateInitialAfterImage(), MAX_AFTER_IMAGE_NUMBER);
    }

    /// <summary>
    /// 残像の描画
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

        GameObject operateObject = afterImages[afterImages.Count - 1];  // リストの最後にあるものを操作するため
        operateObject.transform.position = imageTransform.position; // 位置を設定
        operateObject.transform.rotation = imageTransform.rotation; // 角度を設定
        operateObject.transform.localScale = new Vector3(0.5f, 0.5f, 1); // 大きさを設定

        // 残像の削除
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
    /// 生成までの時間確保
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
    /// 残像のオリジナルオブジェクトの作成
    /// </summary>
    /// <returns></returns>
    private GameObject GenerateInitialAfterImage()
    {
        GameObject spriteObject = new GameObject("GeneratedSprite");
        // コンポーネントの追加
        spriteObject.AddComponent<FadeSprite>();
        SpriteRenderer spriteRenderer = spriteObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = afterImage;
        ConvertImageColor(spriteRenderer);
        return spriteObject;
    }

    /// <summary>
    /// 表示をしている残像の全削除
    /// </summary>
    internal void RemoveAllAfterImage()
    {
        objectPool.AllHidden(parentObject.transform);
    }
}
