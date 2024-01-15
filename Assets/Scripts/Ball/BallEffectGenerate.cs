using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �{�[���̃G�t�F�N�g����
/// </summary>
public class BallEffectGenerate : MonoBehaviour
{
    [SerializeField, Header("�G�t�F�N�g�I�u�W�F�N�g")]
    private List<GameObject> effectObjects = new List<GameObject>();

    private static readonly float EFFECT_LIFE_TIME = 1.0f;    // ��������Ă���̎c�鎞��

    /// <summary>
    /// �G�t�F�N�g����
    /// </summary>
    /// <param name="collision">���������Ώ�</param>
    /// <param name="effectNumber">��������G�t�F�N�g�ԍ�</param>
    public void GenerateEffects(Collision2D collision, int effectNumber)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Vector3 position = DecideEffectPosition(collision);
            Quaternion rotation = DecideEffectRotation(collision);

            GameObject newEffect = Instantiate(effectObjects[effectNumber], position, rotation);

            if (collision.gameObject.CompareTag("Player"))
            {
                newEffect.transform.parent = collision.gameObject.transform;    // Player�̎q�ɂ���
            }

            // effect�̍폜
            Destroy(newEffect, EFFECT_LIFE_TIME);
        }
    }

    /// <summary>
    /// �G�t�F�N�g�̈ʒu�����߂�
    /// </summary>
    /// <param name="collision">���������Ώ�</param>
    /// <returns>���������ʒu</returns>
    private Vector3 DecideEffectPosition(Collision2D collision)
    {
        Vector3 position = new Vector3(0, 0, 0);
        foreach (ContactPoint2D contact in collision.contacts)
        {
            position = contact.point;
            position.z -= -1;   // �X�e�[�W��艜�ɕ\��
        }
        return position;
    }

    /// <summary>
    /// �G�t�F�N�g�̕��������߂�
    /// </summary>
    /// <param name="collision">���������Ώ�</param>
    /// <returns>�G�t�F�N�g�̕���</returns>
    private Quaternion DecideEffectRotation(Collision2D collision)
    {
        Quaternion rotation = new Quaternion();
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Vector2 normal = contact.normal; // normal���G�t�F�N�g�̌����Ƃ��Ďg�p����
            rotation = Quaternion.FromToRotation(Vector2.up, normal);
        }
        return rotation;
    }
}
