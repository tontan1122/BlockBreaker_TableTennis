using UnityEngine;
using UniRx;

/// <summary>
/// Rigidbodyを停止するクラス
/// </summary>
public class RigidbodyStopper : MonoBehaviour
{
    [SerializeField, Header("停止したいRigidBodyコンポーネント")]
    private Rigidbody2D[] pauseRigidbodys;

    void Start()
    {
        PauseUIController.OnPaused.Subscribe(_ =>
        {
            pauseRigidbodys = FindObjectsOfType<Rigidbody2D>(); // すべてのRigidbodyを取得
            foreach (var rb in pauseRigidbodys)
            {
                if (rb != null) rb.simulated = false;
            }
        })
            .AddTo(this);

        PauseUIController.OnResumed.Subscribe(_ =>
        {
            foreach (var rb in pauseRigidbodys)
            {
                if (rb != null) rb.simulated = true;
            }

            pauseRigidbodys = null;
        })
            .AddTo(this);
    }

}
