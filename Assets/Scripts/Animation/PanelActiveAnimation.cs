using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// パネルの表示、非表示アニメーション
/// </summary>
public class PanelActiveAnimation : MonoBehaviour
{
    // アニメーター
    private Animator animator;

    // アニメーターコントローラーのレイヤー(通常は0)
    [SerializeField] private int layer;

    [SerializeField, Header("panel入力を防ぐためのパネル")]
    private GameObject noClickPanel;

    // IsOpenフラグ(アニメーターコントローラー内で定義したフラグ)
    private static readonly int paramIsOpen = Animator.StringToHash("IsOpen");

    public bool IsOpen => gameObject.activeSelf;    // パネルが開いているかどうか

    // アニメーション中かどうか
    public bool IsTransition { get; private set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // ダイアログを開く
    public void Open()
    {
        // 不正操作防止
        if (IsOpen || IsTransition) return;

        // パネル自体をアクティブにする
        gameObject.SetActive(true);

        // IsOpenフラグをセット
        animator.SetBool(paramIsOpen, true);

        // アニメーション待機
        StartCoroutine(WaitAnimation("Shown"));
    }

    // ダイアログを閉じる
    public void Close()
    {
        // 不正操作防止
        if (!IsOpen || IsTransition) return;

        // IsOpenフラグをクリア
        animator.SetBool(paramIsOpen, false);

        // アニメーション待機し、終わったらパネル自体を非アクティブにする
        StartCoroutine(WaitAnimation("Hidden", () => gameObject.SetActive(false)));
    }

    // 開閉アニメーションの待機コルーチン
    private IEnumerator WaitAnimation(string stateName, UnityAction onCompleted = null)
    {
        IsTransition = true;
        noClickPanel.SetActive(true);

        yield return new WaitUntil(() =>
        {
            // ステートが変化し、アニメーションが終了するまでループ
            var state = animator.GetCurrentAnimatorStateInfo(layer);
            return state.IsName(stateName) && state.normalizedTime >= 1;
        });

        IsTransition = false;
        noClickPanel.SetActive(false);


        onCompleted?.Invoke();
    }
}
