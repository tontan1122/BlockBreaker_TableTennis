using UnityEngine;

/// <summary>
/// 重力エリアのクラス
/// </summary>
internal class GravityArea : MonoBehaviour
{
    [SerializeField, Header("重力の強さ")]
    private int gravityPower = 20;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            Rigidbody2D ballRigidbody = collision.GetComponent<Rigidbody2D>();
            ballRigidbody.AddForce(this.transform.parent.up * gravityPower);   // 重力をこのオブジェクトの向きに加える
        }
    }
}
