using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsingAnimation : MonoBehaviour
{
    #region Public Variables
    #endregion

    #region Private Variables
    #endregion

    private void Start()
    {
        Pulsing();
    }

    void Pulsing()
    {
        gameObject.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.75f)
            .SetEase(Ease.Linear)
            .OnComplete(delegate
            {
                gameObject.transform.DOScale(Vector3.one, 0.75f)
                .SetEase(Ease.Linear)
                .OnComplete(delegate
                {
                    Pulsing();
                });
            });
    }
}
