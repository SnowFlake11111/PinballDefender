using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningFlashEffect : MonoBehaviour
{
    #region Public Variables
    [Header("Effect reference")]
    public ParticleSystem flashEffect;
    #endregion

    #region Private Variables
    #endregion

    #region Start, Update
    #endregion

    #region Functions
    //----------Public----------
    public void InitSummonFlashEffect()
    {
        StartCoroutine(SelfDestructSequence());
    }

    //----------Private----------
    IEnumerator SelfDestructSequence()
    {
        yield return new WaitForSeconds(flashEffect.main.duration + flashEffect.main.startLifetime.constantMax);
        Destroy(gameObject);
    }
    #endregion
}
