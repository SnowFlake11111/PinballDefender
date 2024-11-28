using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTeleporter : MonoBehaviour
{
    #region Public Variables
    [Header("Blacklist duration (to prevent endless teleportation)")]
    public float blacklistDuration = 0;

    [Space] public float whitelistDuration = 0;
    [Header("Linked portal")]
    public BallTeleporter linkedPortal;
    #endregion

    #region Private Variables
    List<Ball> blacklist = new List<Ball>();
    #endregion

    #region Start, Update
    #endregion

    #region Functions
    //----------Public----------
    public void RegisterBlacklistedBall (Ball ball)
    {
        blacklist.Add(ball);
    }

    public void RemoveBlacklistedBall (Ball ball)
    {
        blacklist.Remove(ball);
    }

    //----------Private----------
    void CleanupBlacklist()
    {
        if (blacklist.Count <= 0)
        {
            return;
        }

        List<int> nullBallIndexes = new List<int>();

        foreach (Ball ball in blacklist)
        {
            if (ball == null)
            {
                nullBallIndexes.Add(blacklist.IndexOf(ball));
            }
        }

        if (nullBallIndexes.Count > 0)
        {
            int removedBallCount = 0;

            foreach (int index in nullBallIndexes)
            {
                blacklist.RemoveAt(index - removedBallCount);
                removedBallCount++;
            }
        }
    }
    #endregion

    #region Collision Functions
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ball>() != null)
        {
            CleanupBlacklist();
        }
    }
    #endregion
}
