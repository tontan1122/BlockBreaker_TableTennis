using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BallEffectGenerate : MonoBehaviour
{
    [SerializeField, Header("�G�t�F�N�g�I�u�W�F�N�g")]
    private List<GameObject> effectObjects = new List<GameObject>();

    [SerializeField, Header("�G�t�F�N�g�I�u�W�F�N�g��������܂ł̎���")]
    private float effectLifeTime = 3.0f;

    /// <summary>
    /// �G�t�F�N�g����
    /// </summary>
    /// <param name="collision">���������Ώ�</param>
    /// <param name="effectNumber">��������G�t�F�N�g�ԍ�</param>
    internal void EffectGenerator(Collision2D collision, int effectNumber)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Vector3 position = EffectPositionDecide(collision);
            Quaternion rotation = EffectRotationDecide(collision);

            GameObject newEffect = Instantiate(effectObjects[effectNumber], position, rotation);

            if (collision.gameObject.CompareTag("Player"))
            {
                newEffect.transform.parent = collision.gameObject.transform;    // Player�̎q�ɂ���
            }
            DestroyEffect(newEffect, effectLifeTime);
        }
    }

    /// <summary>
    /// �G�t�F�N�g�̈ʒu�����߂�
    /// </summary>
    /// <param name="collision">���������Ώ�</param>
    /// <returns>���������ʒu</returns>
    private Vector3 EffectPositionDecide(Collision2D collision)
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
    private Quaternion EffectRotationDecide(Collision2D collision)
    {
        Quaternion rotation = new Quaternion();
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Vector2 normal = contact.normal; // normal���G�t�F�N�g�̌����Ƃ��Ďg�p����
            rotation = Quaternion.FromToRotation(Vector2.up, normal);
        }
        return rotation;
    }

    private void DestroyEffect(GameObject destroyEffect, float lifeTime)
    {
        Destroy(destroyEffect.gameObject, lifeTime);
    }
}
