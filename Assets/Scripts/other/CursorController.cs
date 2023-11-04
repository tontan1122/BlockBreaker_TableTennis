using UnityEngine;

/// <summary>
/// �J�[�\��������s��
/// </summary>
internal class CursorController : MonoBehaviour
{
    [SerializeField, Header("�J�[�\����\������L�����o�X")]
    private Canvas canvas;

    [SerializeField, Header("�J�[�\���摜��RectTransform")]
    private RectTransform cursorTransform;

    [SerializeField, Header("�J�[�\�����������ǂ���")]
    private bool isCursorActive = false;

    void Update()
    {
        CursorPointMove();
        CursorInputAction();
        CursorOff();
    }

    /// <summary>
    /// �J�[�\���ړ�
    /// </summary>
    private void CursorPointMove()
    {
        // Canvas��RectTransform���ɂ���}�E�X�̍��W�����[�J�����W�ɕϊ�����
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.GetComponent<RectTransform>(),
        Input.mousePosition,
            canvas.worldCamera,
            out var mousePosition);

        // �|�C���^�[���}�E�X�̍��W�Ɉړ�����
        cursorTransform.anchoredPosition = new Vector2(mousePosition.x, mousePosition.y);
    }

    private void CursorInputAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cursorTransform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        }
        else if(Input.GetMouseButtonUp(0)) 
        {
            cursorTransform.localScale = new Vector3(1, 1, 1);
        }
    }

    /// <summary>
    /// �J�[�\���̔�\��
    /// </summary>
    private void CursorOff()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined; // ��ʓ��ɌŒ�
    }
}
