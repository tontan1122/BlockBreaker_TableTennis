using UnityEngine;

/// <summary>
/// �d�̓G���A�̃N���X
/// </summary>
internal class GravityArea : MonoBehaviour
{
    [SerializeField, Header("�d�͂̋���")]
    private int gravityPower = 20;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            Rigidbody2D ballRigidbody = collision.GetComponent<Rigidbody2D>();
            ballRigidbody.AddForce(this.transform.parent.up * gravityPower);   // �d�͂����̃I�u�W�F�N�g�̌����ɉ�����
        }
    }
}
