using UnityEngine;

public class BallEffectGenerate : MonoBehaviour
{
    [SerializeField, Header("跳ね返り時のエフェクト")]
    private GameObject boundEffectPrefab;

    [SerializeField, Header("赤ブロックでやられたときのエフェクト")]
    private GameObject redDeathEffectPrefab;
    [SerializeField, Header("画面外でやられたときのエフェクト")]
    private GameObject outDeathEffectPrefab;


    internal void BoundEffectGenerate(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Vector2 normal = contact.normal;
            // normalをエフェクトの向きとして使用する
            Vector3 position = contact.point;
            position.z -= -1;   // ステージより奥に表示
            Quaternion rotation = Quaternion.FromToRotation(Vector2.up, normal);
            //rotation.x = 0f;
            GameObject newEffect = null;
            if (collision.gameObject.CompareTag("DeathBlock"))
            {
                newEffect = Instantiate(redDeathEffectPrefab, position, rotation);
            }
            else if(collision.gameObject.CompareTag("DeathArea"))
            {
                newEffect = Instantiate(outDeathEffectPrefab, position, rotation);
            }
            else
            {
                newEffect = Instantiate(boundEffectPrefab, position, rotation);
            }

            if (collision.gameObject.CompareTag("Player"))
            {
                newEffect.transform.parent = collision.gameObject.transform;    // Playerの子にする
            }
            DestroyEffect(newEffect, 3.0f);
        }
    }

    private void DestroyEffect(GameObject destroyEffect, float lifeTime)
    {
        Destroy(destroyEffect.gameObject, lifeTime);
    }
}
