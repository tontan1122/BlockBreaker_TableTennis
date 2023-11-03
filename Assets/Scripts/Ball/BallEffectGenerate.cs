using UnityEngine;

public class BallEffectGenerate : MonoBehaviour
{
    [SerializeField, Header("���˕Ԃ莞�̃G�t�F�N�g")]
    private GameObject boundEffectPrefab;

    [SerializeField, Header("�ԃu���b�N�ł��ꂽ�Ƃ��̃G�t�F�N�g")]
    private GameObject redDeathEffectPrefab;
    [SerializeField, Header("��ʊO�ł��ꂽ�Ƃ��̃G�t�F�N�g")]
    private GameObject outDeathEffectPrefab;


    internal void BoundEffectGenerate(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Vector2 normal = contact.normal;
            // normal���G�t�F�N�g�̌����Ƃ��Ďg�p����
            Vector3 position = contact.point;
            position.z -= -1;   // �X�e�[�W��艜�ɕ\��
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
                newEffect.transform.parent = collision.gameObject.transform;    // Player�̎q�ɂ���
            }
            DestroyEffect(newEffect, 3.0f);
        }
    }

    private void DestroyEffect(GameObject destroyEffect, float lifeTime)
    {
        Destroy(destroyEffect.gameObject, lifeTime);
    }
}
