using UnityEngine;
using UniRx;

/// <summary>
/// Rigidbody���~����N���X
/// </summary>
public class RigidbodyStopper : MonoBehaviour
{
    [SerializeField, Header("��~������RigidBody�R���|�[�l���g")]
    private Rigidbody2D[] pauseRigidbodys;

    void Start()
    {
        PauseUIController.OnPaused.Subscribe(_ =>
        {
            pauseRigidbodys = FindObjectsOfType<Rigidbody2D>(); // ���ׂĂ�Rigidbody���擾
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
