using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボールのエフェクト生成
/// </summary>
public class BallEffectGenerate : MonoBehaviour
{
    [SerializeField, Header("エフェクトオブジェクト")]
    private List<GameObject> effectObjects = new List<GameObject>();

    private static readonly float EFFECT_LIFE_TIME = 1.0f;    // 生成されてからの残る時間

    /// <summary>
    /// エフェクト生成
    /// </summary>
    /// <param name="collision">あたった対象</param>
    /// <param name="effectNumber">生成するエフェクト番号</param>
    public void GenerateEffects(Collision2D collision, int effectNumber)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Vector3 position = DecideEffectPosition(collision);
            Quaternion rotation = DecideEffectRotation(collision);

            GameObject newEffect = Instantiate(effectObjects[effectNumber], position, rotation);

            if (collision.gameObject.CompareTag("Player"))
            {
                newEffect.transform.parent = collision.gameObject.transform;    // Playerの子にする
            }

            // effectの削除
            Destroy(newEffect, EFFECT_LIFE_TIME);
        }
    }

    /// <summary>
    /// エフェクトの位置を決める
    /// </summary>
    /// <param name="collision">あたった対象</param>
    /// <returns>あたった位置</returns>
    private Vector3 DecideEffectPosition(Collision2D collision)
    {
        Vector3 position = new Vector3(0, 0, 0);
        foreach (ContactPoint2D contact in collision.contacts)
        {
            position = contact.point;
            position.z -= -1;   // ステージより奥に表示
        }
        return position;
    }

    /// <summary>
    /// エフェクトの方向を決める
    /// </summary>
    /// <param name="collision">あたった対象</param>
    /// <returns>エフェクトの方向</returns>
    private Quaternion DecideEffectRotation(Collision2D collision)
    {
        Quaternion rotation = new Quaternion();
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Vector2 normal = contact.normal; // normalをエフェクトの向きとして使用する
            rotation = Quaternion.FromToRotation(Vector2.up, normal);
        }
        return rotation;
    }
}
