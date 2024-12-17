using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    #region Public Variables
    public ParticleSystem effect;
    #endregion

    #region Private Variables
    float effectDuration = 0;
    #endregion

    #region Start, Update
    #endregion

    #region Functions
    //----------Public----------
    public void Init()
    {
        effectDuration = effect.main.duration + effect.main.startLifetime.constantMax;
        StartCoroutine(SelfRecycle());
    }
    //----------Private----------
    IEnumerator SelfRecycle()
    {
        yield return new WaitForSeconds(effectDuration);
        SimplePool2.Despawn(gameObject);
    }
    #endregion
}
