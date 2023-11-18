using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �p�l���̕\���A��\���A�j���[�V����
/// </summary>
public class PanelActiveAnimation : MonoBehaviour
{
    // �A�j���[�^�[
    private Animator animator;

    // �A�j���[�^�[�R���g���[���[�̃��C���[(�ʏ��0)
    [SerializeField] private int layer;

    [SerializeField, Header("panel���͂�h�����߂̃p�l��")]
    private GameObject noClickPanel;

    // IsOpen�t���O(�A�j���[�^�[�R���g���[���[���Œ�`�����t���O)
    private static readonly int paramIsOpen = Animator.StringToHash("IsOpen");

    public bool IsOpen => gameObject.activeSelf;    // �p�l�����J���Ă��邩�ǂ���

    // �A�j���[�V���������ǂ���
    public bool IsTransition { get; private set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // �_�C�A���O���J��
    public void Open()
    {
        // �s������h�~
        if (IsOpen || IsTransition) return;

        // �p�l�����̂��A�N�e�B�u�ɂ���
        gameObject.SetActive(true);

        // IsOpen�t���O���Z�b�g
        animator.SetBool(paramIsOpen, true);

        // �A�j���[�V�����ҋ@
        StartCoroutine(WaitAnimation("Shown"));
    }

    // �_�C�A���O�����
    public void Close()
    {
        // �s������h�~
        if (!IsOpen || IsTransition) return;

        // IsOpen�t���O���N���A
        animator.SetBool(paramIsOpen, false);

        // �A�j���[�V�����ҋ@���A�I�������p�l�����̂��A�N�e�B�u�ɂ���
        StartCoroutine(WaitAnimation("Hidden", () => gameObject.SetActive(false)));
    }

    // �J�A�j���[�V�����̑ҋ@�R���[�`��
    private IEnumerator WaitAnimation(string stateName, UnityAction onCompleted = null)
    {
        IsTransition = true;
        noClickPanel.SetActive(true);

        yield return new WaitUntil(() =>
        {
            // �X�e�[�g���ω����A�A�j���[�V�������I������܂Ń��[�v
            var state = animator.GetCurrentAnimatorStateInfo(layer);
            return state.IsName(stateName) && state.normalizedTime >= 1;
        });

        IsTransition = false;
        noClickPanel.SetActive(false);


        onCompleted?.Invoke();
    }
}
