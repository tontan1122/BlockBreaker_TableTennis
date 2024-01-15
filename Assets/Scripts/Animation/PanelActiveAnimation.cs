using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// パネルの表示、非表示アニメーション
/// </summary>
public class PanelActiveAnimation : MonoBehaviour
{
    // アニメーター
    private Animator animator;


    [SerializeField, Header("panel入力を防ぐためのパネル")]
    private GameObject noClickPanel;

    [SerializeField, Header("動かしたくないスクロールビュー")]
    private ScrollRect scrollRect;

    // IsOpenフラグ(アニメーターコントローラー内で定義したフラグ)
    private static readonly int paramIsOpen = Animator.StringToHash("IsOpen");

    public bool IsOpen => gameObject.activeSelf;    // パネルが開いているかどうか

    private bool isOpenTransition = false;

    private bool isTransition;    // アニメーション中かどうか

    private int layer = 0;  // アニメーターコントローラーのレイヤー(通常は0)

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// ダイアログを開く
    /// </summary>
    public void Open()
    {
        if (IsOpen || isTransition) return;    // 不正操作防止

        StartAnimation();
        isOpenTransition = true;

        animator.SetBool(paramIsOpen, true);    // IsOpenフラグをセット


        StartCoroutine(WaitAnimation("Shown"));    // アニメーション待機
    }

    /// <summary>
    /// ダイアログを閉じる
    /// </summary>
    public void Close()
    {
        if (!IsOpen || isTransition) return;    // 不正操作防止

        StartAnimation();

        animator.SetBool(paramIsOpen, false);   // IsOpenフラグをクリア

        StartCoroutine(WaitAnimation("Hidden", () => gameObject.SetActive(false)));   // アニメーション待機
    }

    /// <summary>
    /// 開閉アニメーションの待機コルーチン
    /// </summary>
    /// <param name="stateName">アニメーションの状態</param>
    /// <param name="onCompleted">アニメーション完了時処理</param>
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

        EndAnimation();
        onCompleted?.Invoke();
    }

    /// <summary>
    /// アニメーション開始時処理
    /// </summary>
    private void StartAnimation()
    {
        isTransition = true;
        gameObject.SetActive(true);
        noClickPanel.SetActive(true);
    }

    /// <summary>
    /// アニメーション終了時処理
    /// </summary>
    private void EndAnimation()
    {
        isTransition = false;
        noClickPanel.SetActive(false);
        isOpenTransition = false;
    }
}
