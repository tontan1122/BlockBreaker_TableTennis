using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private new Rigidbody2D rigidbody;

    void Start()
    {
        rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        // �J�[�\���ʒu�����[���h���W�ɕϊ�
        Vector2 target = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = target - (Vector2)transform.position;
        direction.y = 0;

        rigidbody.velocity = direction * 10;
    }
}
