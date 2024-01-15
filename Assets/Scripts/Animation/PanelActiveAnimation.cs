using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// �p�l���̕\���A��\���A�j���[�V����
/// </summary>
public class PanelActiveAnimation : MonoBehaviour
{
    // �A�j���[�^�[
    private Animator animator;


    [SerializeField, Header("panel���͂�h�����߂̃p�l��")]
    private GameObject noClickPanel;

    [SerializeField, Header("�����������Ȃ��X�N���[���r���[")]
    private ScrollRect scrollRect;

    // IsOpen�t���O(�A�j���[�^�[�R���g���[���[���Œ�`�����t���O)
    private static readonly int paramIsOpen = Animator.StringToHash("IsOpen");

    public bool IsOpen => gameObject.activeSelf;    // �p�l�����J���Ă��邩�ǂ���

    private bool isOpenTransition = false;

    private bool isTransition;    // �A�j���[�V���������ǂ���

    private int layer = 0;  // �A�j���[�^�[�R���g���[���[�̃��C���[(�ʏ��0)

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// �_�C�A���O���J��
    /// </summary>
    public void Open()
    {
        if (IsOpen || isTransition) return;    // �s������h�~

        StartAnimation();
        isOpenTransition = true;

        animator.SetBool(paramIsOpen, true);    // IsOpen�t���O���Z�b�g


        StartCoroutine(WaitAnimation("Shown"));    // �A�j���[�V�����ҋ@
    }

    /// <summary>
    /// �_�C�A���O�����
    /// </summary>
    public void Close()
    {
        if (!IsOpen || isTransition) return;    // �s������h�~

        StartAnimation();

        animator.SetBool(paramIsOpen, false);   // IsOpen�t���O���N���A

        StartCoroutine(WaitAnimation("Hidden", () => gameObject.SetActive(false)));   // �A�j���[�V�����ҋ@
    }

    /// <summary>
    /// �J�A�j���[�V�����̑ҋ@�R���[�`��
    /// </summary>
    /// <param name="stateName">�A�j���[�V�����̏��</param>
    /// <param name="onCompleted">�A�j���[�V��������������</param>
    private IEnumerator WaitAnimation(string stateName, UnityAction onCompleted = null)
    {

        yield return new WaitUntil(() =>
        {
            // �X�e�[�g���ω����A�A�j���[�V�������I������܂Ń��[�v
            var state = animator.GetCurrentAnimatorStateInfo(layer);
            if (scrollRect != null && isOpenTransition)
            {
                scrollRect.verticalNormalizedPosition = 1;
            }
            return state.IsName(stateName) && state.normalizedTime >= 1;
        });

        EndAnimation();
        onCompleted?.Invoke();
    }

    /// <summary>
    /// �A�j���[�V�����J�n������
    /// </summary>
    private void StartAnimation()
    {
        isTransition = true;
        gameObject.SetActive(true);
        noClickPanel.SetActive(true);
    }

    /// <summary>
    /// �A�j���[�V�����I��������
    /// </summary>
    private void EndAnimation()
    {
        isTransition = false;
        noClickPanel.SetActive(false);
        isOpenTransition = false;
    }
}
