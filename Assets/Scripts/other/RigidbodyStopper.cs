using UnityEngine;
using UniRx;
using System;

public class RigidbodyStopper : MonoBehaviour
{
    [SerializeField, Header("停止したいRigidBodyコンポーネント")]
    private Rigidbody2D[] pauseRigidbodys;

    void Start()
    {
        PauseUIController.OnPaused.Subscribe(_ =>
        {
            //pauseRigidbodys = Array.FindAll(GetComponentsInChildren<Rigidbody2D>(), (obj) => { return obj.simulated; });
            pauseRigidbodys = UnityEngine.Object.FindObjectsOfType<Rigidbody2D>();
            foreach (var com in pauseRigidbodys)
            {
                if (com != null) com.simulated = false;
            }
        })
            .AddTo(this);
        PauseUIController.OnResumed.Subscribe(_ =>
        {
            foreach (var com in pauseRigidbodys)
            {
                if (com != null) com.simulated = true;
            }

            pauseRigidbodys = null;
        })
            .AddTo(this);
    }

}
