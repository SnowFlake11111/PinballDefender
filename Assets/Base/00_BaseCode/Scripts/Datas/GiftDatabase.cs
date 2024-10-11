using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[CreateAssetMenu(menuName = "Datas/GiftDatabase", fileName = "GiftDatabase.asset")]
public class GiftDatabase : SerializedScriptableObject
{
    public Dictionary<GiftType, Gift> giftList;

    public bool GetGift(GiftType giftType, out Gift gift)
    {
        return giftList.TryGetValue(giftType, out gift);
    }

    public Sprite GetIconItem(GiftType giftType)
    {
        Gift gift = null;
        //if (IsCharacter(giftType))
        //{
        //    var Char = GameController.Instance.dataContain.dataSkins.GetSkinInfo(giftType);
        //    if (Char != null)
        //        return Char.iconSkin;
        //}
        bool isGetGift = GetGift(giftType, out gift);
        return isGetGift ? gift.getGiftSprite : null;
    }
    public GameObject GetAnimItem(GiftType giftType)
    {
        Gift gift = null;
        bool isGetGift = GetGift(giftType, out gift);
        return isGetGift ? gift.getGiftAnim : null;
    }

    public void Claim(GiftType giftType, int amount, Reason reason = Reason.none)
    {
        List<GiftRewardShow> giftRewardShows = new List<GiftRewardShow>();

        //Debug.LogError(giftType.ToString());

        switch (giftType)
        {
            case GiftType.Coin:
                UseProfile.Coin += amount;
                giftRewardShows.Add(new GiftRewardShow() { amount = amount, type = GiftType.Coin });
                //EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.CHANGE_COIN);
                break;
            case GiftType.UnlimitedHeart:
                UseProfile.RemainingTimeForUnlimitedHeart += amount;
                giftRewardShows.Add(new GiftRewardShow() { amount = amount, type = GiftType.UnlimitedHeart });
                break;
            case GiftType.RemoveAds:
                Debug.LogError("NO ADS BOUGHT");
                GameController.Instance.useProfile.IsRemoveAds = true;
                GameController.Instance.admobAds.DestroyBanner();
                giftRewardShows.Add(new GiftRewardShow() { amount = amount, type = GiftType.RemoveAds });
                break;
            case GiftType.Booster_Delete:
                Debug.LogError("BOOSTER_DELETE GAINED");
                UseProfile.DeleteTwoMistakes += amount;
                giftRewardShows.Add(new GiftRewardShow() { amount = amount, type = GiftType.Booster_Delete });
                break;
            case GiftType.Booster_Reshuffle:
                Debug.LogError("BOOSTER_RESHUFFLE GAINED");
                UseProfile.ReshuffleCake += amount;
                giftRewardShows.Add(new GiftRewardShow() { amount = amount, type = GiftType.Booster_Reshuffle });
                break;
            case GiftType.Booster_Instant:
                Debug.LogError("BOOSTER_INSTANT GAINED");
                UseProfile.InstantOrder += amount;
                giftRewardShows.Add(new GiftRewardShow() { amount = amount, type = GiftType.Booster_Instant });
                break;
            
        }

        PopupRewardBase.Setup().Show(giftRewardShows, delegate { });

        giftRewardShows.Clear();
    }

    public static bool IsCharacter(GiftType giftType)
    {
        //switch (giftType)
        //{
        //    case GiftType.RandomSkin:
        //        return true;
        //}
        return false;
    }
}

public class Gift
{
    [SerializeField] private Sprite giftSprite;
    [SerializeField] private GameObject giftAnim;
    public virtual Sprite getGiftSprite => giftSprite;
    public virtual GameObject getGiftAnim => giftAnim;

}

public enum GiftType
{
    None = 0,
    RemoveAds = 1,
    Coin = 2,
    UnlimitedHeart = 3,
    Booster_Delete = 4,
    Booster_Reshuffle = 5,
    Booster_Instant = 6,



}

public enum Reason
{
    none = 0,
    play_with_item = 1,
    watch_video_claim_item_main_home = 2,
    daily_login = 3,
    lucky_spin = 4,
    unlock_skin_in_special_gift = 5,
    reward_accumulate = 6,
}

[Serializable]
public class RewardRandom
{
    public int id;
    public GiftType typeItem;
    public int amount;
    public int weight;

    public RewardRandom()
    {
    }
    public RewardRandom(GiftType item, int amount, int weight = 0)
    {
        this.typeItem = item;
        this.amount = amount;
        this.weight = weight;
    }

    public GiftRewardShow GetReward()
    {
        GiftRewardShow rew = new GiftRewardShow();
        rew.type = typeItem;
        rew.amount = amount;

        return rew;
    }
}
