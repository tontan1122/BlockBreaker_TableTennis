using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// パネルの表示、非表示アニメーション
/// </summary>
internal class PanelActiveAnimation : MonoBehaviour
{
    // アニメーター
    private Animator animator;

    // アニメーターコントローラーのレイヤー(通常は0)
    [SerializeField]
    private int layer;

    [SerializeField, Header("panel入力を防ぐためのパネル")]
    private GameObject noClickPanel;

    [SerializeField, Header("動かしたくないスクロールビュー")]
    private ScrollRect scrollRect;

    // IsOpenフラグ(アニメーターコントローラー内で定義したフラグ)
    private static readonly int paramIsOpen = Animator.StringToHash("IsOpen");

    internal bool IsOpen => gameObject.activeSelf;    // パネルが開いているかどうか

    // アニメーション中かどうか
    internal bool IsTransition { get; private set; }

    private bool isOpenTransition = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // ダイアログを開く
    internal void Open()
    {
        // 不正操作防止
        if (IsOpen || IsTransition) return;

        AnimationStart();
        isOpenTransition = true;

        // パネル自体をアクティブにする
        gameObject.SetActive(true);

        // IsOpenフラグをセット
        animator.SetBool(paramIsOpen, true);

        // アニメーション待機
        StartCoroutine(WaitAnimation("Shown"));
    }

    // ダイアログを閉じる
    internal void Close()
    {
        // 不正操作防止
        if (!IsOpen || IsTransition) return;

        AnimationStart();

        // IsOpenフラグをクリア
        animator.SetBool(paramIsOpen, false);

        // アニメーション待機し、終わったらパネル自体を非アクティブにする
        StartCoroutine(WaitAnimation("Hidden", () => gameObject.SetActive(false)));
    }

    // 開閉アニメーションの待機コルーチン
    private IEnumerator WaitAnimation(string stateName, UnityAction onCompleted = null)
    {

        yield return new WaitUntil(() =>
        {
            // ステートが変化し、アニメーションが終了するまでループ
            var state = animator.GetCurrentAnimatorStateInfo(layer);
            if (scrollRect != null && isOpenTransition)
            {
                scrollRect.verticalNormalizedPosition = 1;
            }
            return state.IsName(stateName) && state.normalizedTime >= 1;
        });

        AnimationEnd();
        onCompleted?.Invoke();
    }

    private void AnimationStart()
    {
        IsTransition = true;
        noClickPanel.SetActive(true);
    }

    private void AnimationEnd()
    {
        IsTransition = false;
        noClickPanel.SetActive(false);
        isOpenTransition = false;
    }
}
