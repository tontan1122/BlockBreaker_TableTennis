using UnityEngine;

/// <summary>
/// �J�[�\��������s��
/// </summary>
public class CursorController : MonoBehaviour
{
    [SerializeField, Header("�J�[�\����\������L�����o�X")]
    private Canvas canvas;

    [SerializeField, Header("�J�[�\���摜��RectTransform")]
    private RectTransform cursorTransform;

    private static readonly float CURSOR_SIZE_ON_CLICK = 0.7f;  // �N���b�N���̃J�[�\���T�C�Y

    void Update()
    {
        MoveCursorPoint();
        DrawingCursorInput();
        HideCursor();
    }

    /// <summary>
    /// �J�[�\���ړ�
    /// </summary>
    private void MoveCursorPoint()
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

    /// <summary>
    /// �J�[�\���̓��͕`��
    /// </summary>
    private void DrawingCursorInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // �摜�T�C�Y�ύX
            cursorTransform.localScale = new Vector3(CURSOR_SIZE_ON_CLICK, CURSOR_SIZE_ON_CLICK, CURSOR_SIZE_ON_CLICK);
        }
        else if(Input.GetMouseButtonUp(0)) 
        {
            cursorTransform.localScale = new Vector3(1, 1, 1);  // ���̃T�C�Y�ɖ߂�
        }
    }

    /// <summary>
    /// �J�[�\���̔�\��
    /// </summary>
    private void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined; // ��ʓ��ɌŒ�
    }
}
