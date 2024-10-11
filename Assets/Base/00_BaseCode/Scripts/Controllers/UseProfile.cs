using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MoreMountains.NiceVibrations;

public class UseProfile : MonoBehaviour
{
    public static bool FirstLoading
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.LOADING_COMPLETE, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.LOADING_COMPLETE, value ? 1 : 0);    
            PlayerPrefs.Save();
        }
    }

    public static int CurrentLevel
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CURRENT_LEVEL);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CURRENT_LEVEL, value);
            PlayerPrefs.Save();
        }
    }

    public static bool Reshuffle
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.RETRY_SHUFFLE, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.RETRY_SHUFFLE, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static string BoughtDecorations
    {
        get
        {
            return PlayerPrefs.GetString(StringHelper.BOUGHT_DECORATIONS);
        }
        set
        {
            PlayerPrefs.SetString(StringHelper.BOUGHT_DECORATIONS, value);
            PlayerPrefs.Save();
        }
    }

    public static int ChefStartingPos
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CHEF_STARTING_POS, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CHEF_STARTING_POS , value);
            PlayerPrefs.Save();
        }
    }

    public static DateTime TimeSinceLastExit
    {
        get
        {
            if(PlayerPrefs.HasKey(StringHelper.TIME_SINCE_LAST_EXIT))
            {
                var temp = Convert.ToInt64(PlayerPrefs.GetString(StringHelper.TIME_SINCE_LAST_EXIT));
                return DateTime.FromBinary(temp);
            }
            else
            {
                var temp = DateTime.UtcNow;
                PlayerPrefs.SetString(StringHelper.TIME_SINCE_LAST_EXIT, temp.ToBinary().ToString());
                PlayerPrefs.Save();
                return temp;
            }
        }
        set
        {
            PlayerPrefs.SetString(StringHelper.TIME_SINCE_LAST_EXIT, value.ToBinary().ToString());
            PlayerPrefs.Save();
        }
    }

    public static float RemainingCounterSinceLastExit
    {
        get
        {
            return PlayerPrefs.GetFloat(StringHelper.REMAINING_COUNTER_SINCE_SINCE_LAST_EXIT, 0);
        }
        set
        {
            PlayerPrefs.SetFloat(StringHelper.REMAINING_COUNTER_SINCE_SINCE_LAST_EXIT, value);
            PlayerPrefs.Save();
        }
    }

    public static float CurrentCounter
    {
        get
        {
            return PlayerPrefs.GetFloat(StringHelper.CURRENT_COUNTER, 0);
        }
        set
        {
            PlayerPrefs.SetFloat(StringHelper.CURRENT_COUNTER, value);
            PlayerPrefs.Save();
        }
    }

    public static float RemainingTimeHeartCooldown
    {
        get
        {
            return PlayerPrefs.GetFloat(StringHelper.REMAINING_TIME_FOR_HEART_COOLDOWN, 0);
        }
        set
        {
            PlayerPrefs.SetFloat(StringHelper.REMAINING_TIME_FOR_HEART_COOLDOWN, value);
            PlayerPrefs.Save();
        }
    }

    public static int Heart
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.HEART, 5);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.HEART, value);
            PlayerPrefs.Save();
            EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.HEART_UPDATE);
        }
    }

    public static int MaxHeart
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.MAX_HEART, 5);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.MAX_HEART, value);
            PlayerPrefs.Save();
        }
    }

    public static int Star
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.STAR, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.STAR, value);
            PlayerPrefs.Save();
            EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.CHANGE_STAR);
        }
    }

    public static int DeleteTwoMistakes
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.DELETE_TWO_MISTAKES);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.DELETE_TWO_MISTAKES, value);
            PlayerPrefs.Save();
            EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.BOOSTER_COUNT_CHANGED);
        }
    }

    public static int ReshuffleCake
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.RESHUFFLE_CAKE);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.RESHUFFLE_CAKE, value);
            PlayerPrefs.Save();
            EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.BOOSTER_COUNT_CHANGED);
        }
    }

    public static int InstantOrder
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.INSTANT_ORDER);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.INSTANT_ORDER, value);
            PlayerPrefs.Save();
            EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.BOOSTER_COUNT_CHANGED);
        }
    }

    public static float RemainingTimeForUnlimitedHeart
    {
        get
        {
            return PlayerPrefs.GetFloat(StringHelper.REMAINING_TIME_FOR_UNLIMITED_HEART, 0);
        }
        set
        {
            PlayerPrefs.SetFloat(StringHelper.REMAINING_TIME_FOR_UNLIMITED_HEART, value);
            PlayerPrefs.Save();
            EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.HEART_UPDATE);
        }
    }

    public static float RemainingTimeForAdsCoolDownUnlimitedHeart
    {
        get
        {
            return PlayerPrefs.GetFloat(StringHelper.REMAINING_TIME_FOR_ADS_COOLDOWN_UNLIMITED_HEART, 0);
        }
        set
        {
            PlayerPrefs.SetFloat(StringHelper.REMAINING_TIME_FOR_ADS_COOLDOWN_UNLIMITED_HEART, value);
            PlayerPrefs.Save();
        }
    }

    public static int EggRanking
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.SCORE_RANKING, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.SCORE_RANKING, value);
            PlayerPrefs.Save();
        }
    }

    #region ChestDataKey
    public static int EggChest
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.EGG_CHEST, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.EGG_CHEST, value);
            PlayerPrefs.Save();
        }
    }
    public static int LevelEggChest
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.LEVEL_EGG_CHEST, 1);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.LEVEL_EGG_CHEST, value);
            PlayerPrefs.Save();
        }
    }
    public static int LevelOfLevelChest
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.LEVEL_OF_LEVEL_CHEST, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.LEVEL_OF_LEVEL_CHEST, value);
            PlayerPrefs.Save();
        }
    }
    public static int LevelOfBirdChest
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.LEVEL_OF_BIRD_CHEST, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.LEVEL_OF_BIRD_CHEST, value);
            PlayerPrefs.Save();
        }
    }

    public static int CurrentLevelOfLevelChest
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CURRENT_LEVEL_OF_LEVEL_CHEST, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CURRENT_LEVEL_OF_LEVEL_CHEST, value);
            PlayerPrefs.Save();
        }
    }
    public static int CurrentLevelOfBirdChest
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CURRENT_LEVEL_OF_BIRD_CHEST, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CURRENT_LEVEL_OF_BIRD_CHEST, value);
            PlayerPrefs.Save();
        }
    }
    #endregion

    public static int Coin
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.COIN, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.COIN, value);
            PlayerPrefs.Save();
            EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.CHANGE_COIN);
        }
    }
    

    public static int DestroyScewBooster
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.REDO_BOOSTER, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.REDO_BOOSTER, value);
            PlayerPrefs.Save();
         
        }
    }
    public static int DrillBooster
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.SUPORT_BOOSTER, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.SUPORT_BOOSTER, value);
            PlayerPrefs.Save();

        }
    }


    public static int NumberOfDisplayedInterstitialD0_D1
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.NUMBER_OF_DISPLAYED_INTERST_ITIAL_D0_D1_KEY, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.NUMBER_OF_DISPLAYED_INTERST_ITIAL_D0_D1_KEY, value);
            PlayerPrefs.Save();

        }
    }

    public static int NumberOfDisplayedInterstitialD1
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.NUMBER_OF_DISPLAYED_INTERST_ITIAL_D1_KEY, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.NUMBER_OF_DISPLAYED_INTERST_ITIAL_D1_KEY, value);
            PlayerPrefs.Save();

        }
    }

    public static int NumberRewardShowed
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.NUMBER_REWARD_SHOWED, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.NUMBER_REWARD_SHOWED, value);
            PlayerPrefs.Save();

        }
    }
    public static int NumberInterShowed
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.NUMBER_INTER_SHOWED, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.NUMBER_INTER_SHOWED, value);
            PlayerPrefs.Save();

        }
    }

    public int LevelUnlock
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CURRENT_LEVEL_PLAY, 1);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CURRENT_LEVEL_PLAY, value);
            PlayerPrefs.Save();
        }
    }
    public bool IsRemoveAds
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.REMOVE_ADS, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.REMOVE_ADS, value ? 1 : 0);
            PlayerPrefs.Save();
            //EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.REMOVE_ADS);
        }
    }
    public bool OnVibration
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.ONOFF_VIBRATION, 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.ONOFF_VIBRATION, value ? 1 : 0);
            MMVibrationManager.SetHapticsActive(value);
            PlayerPrefs.Save();
        }
    }
    public bool OnSound
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.ONOFF_SOUND, 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.ONOFF_SOUND, value ? 1 : 0);
            GameController.Instance.musicManager.SetSoundVolume(value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
    public bool OnMusic
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.ONOFF_MUSIC, 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.ONOFF_MUSIC, value ? 1 : 0);
            GameController.Instance.musicManager.SetMusicVolume(value ? 0.7f : 0);
            PlayerPrefs.Save();
        }
    }
    public static bool IsFirstTimeInstall
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.FIRST_TIME_INSTALL, 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.FIRST_TIME_INSTALL, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
    public static int RetentionD
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.RETENTION_D, 0);
        }
        set
        {
            if (value < 0)
                value = 0;

            PlayerPrefs.SetInt(StringHelper.RETENTION_D, value);
            PlayerPrefs.Save();
        }
    }
    public static int DaysPlayed
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.DAYS_PLAYED, 1);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.DAYS_PLAYED, value);
            PlayerPrefs.Save();
        }
    }
    public static int PayingType
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.PAYING_TYPE, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.PAYING_TYPE, value);
            PlayerPrefs.Save();
        }
    }
    public static DateTime FirstTimeOpenGame
    {
        get
        {
            if (PlayerPrefs.HasKey(StringHelper.FIRST_TIME_OPEN_GAME))
            {
                var temp = Convert.ToInt64(PlayerPrefs.GetString(StringHelper.FIRST_TIME_OPEN_GAME));
                return DateTime.FromBinary(temp);
            }
            else
            {
                var newDateTime = UnbiasedTime.Instance.Now().AddDays(-1);
                PlayerPrefs.SetString(StringHelper.FIRST_TIME_OPEN_GAME, newDateTime.ToBinary().ToString());
                PlayerPrefs.Save();
                return newDateTime;
            }
        }
        set
        {
            PlayerPrefs.SetString(StringHelper.FIRST_TIME_OPEN_GAME, value.ToBinary().ToString());
            PlayerPrefs.Save();
        }
    }
    public static DateTime LastTimeOpenGame
    {
        get
        {
            if (PlayerPrefs.HasKey(StringHelper.LAST_TIME_OPEN_GAME))
            {
                var temp = Convert.ToInt64(PlayerPrefs.GetString(StringHelper.LAST_TIME_OPEN_GAME));
                return DateTime.FromBinary(temp);
            }
            else
            {
                var newDateTime = UnbiasedTime.Instance.Now().AddDays(-1);
                PlayerPrefs.SetString(StringHelper.LAST_TIME_OPEN_GAME, newDateTime.ToBinary().ToString());
                PlayerPrefs.Save();
                return newDateTime;
            }
        }
        set
        {
            PlayerPrefs.SetString(StringHelper.LAST_TIME_OPEN_GAME, value.ToBinary().ToString());
            PlayerPrefs.Save();
        }
    }
    public static DateTime LastTimeResetSalePackShop
    {
        get
        {
            if (PlayerPrefs.HasKey(StringHelper.LAST_TIME_RESET_SALE_PACK_SHOP))
            {
                var temp = Convert.ToInt64(PlayerPrefs.GetString(StringHelper.LAST_TIME_RESET_SALE_PACK_SHOP));
                return DateTime.FromBinary(temp);
            }
            else
            {
                var newDateTime = UnbiasedTime.Instance.Now().AddDays(-1);
                PlayerPrefs.SetString(StringHelper.LAST_TIME_RESET_SALE_PACK_SHOP, newDateTime.ToBinary().ToString());
                PlayerPrefs.Save();
                return newDateTime;
            }
        }
        set
        {
            PlayerPrefs.SetString(StringHelper.LAST_TIME_RESET_SALE_PACK_SHOP, value.ToBinary().ToString());
            PlayerPrefs.Save();
        }
    }
    public static bool CanShowRate
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CAN_SHOW_RATE, 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CAN_SHOW_RATE, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
    public static int NumberOfAdsInPlay;
    public static int NumberOfAdsInDay
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.NUMBER_OF_ADS_IN_DAY, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.NUMBER_OF_ADS_IN_DAY, value);
            PlayerPrefs.Save();
        }
    }
    public static DateTime LastTimeOnline
    {
        get
        {
            if (PlayerPrefs.HasKey(StringHelper.LAST_TIME_ONLINE))
            {
                var temp = Convert.ToInt64(PlayerPrefs.GetString(StringHelper.LAST_TIME_ONLINE));
                return DateTime.FromBinary(temp);
            }
            else
            {
                var newDateTime = UnbiasedTime.Instance.Now().AddDays(-1);
                PlayerPrefs.SetString(StringHelper.LAST_TIME_ONLINE, newDateTime.ToBinary().ToString());
                PlayerPrefs.Save();
                return newDateTime;
            }
        }
        set
        {
            PlayerPrefs.SetString(StringHelper.LAST_TIME_ONLINE, value.ToBinary().ToString());
            PlayerPrefs.Save();
        }
    }

    public string ListSave
    {
        get
        {
            return PlayerPrefs.GetString(GameData.LEVEL_SAVE);
        }
        set
        {
            PlayerPrefs.SetString(GameData.LEVEL_SAVE, value);
            PlayerPrefs.Save();
        }
    }
    public void SetDateTimeReciveDailyGift(DateTime value)
    {
        PlayerPrefs.SetString(StringHelper.DATE_RECIVE_GIFT_DAILY, value.ToBinary().ToString());
    }
    public DateTime GetDateTimeReciveDailyGift()
    {
        return GetDateTime(StringHelper.DATE_RECIVE_GIFT_DAILY, DateTime.MinValue);
    }
    public DateTime GetDateTime(string key, DateTime defaultValue)
    {
        string @string = PlayerPrefs.GetString(key);
        DateTime result = defaultValue;
        if (!string.IsNullOrEmpty(@string))
        {
            long dateData = Convert.ToInt64(@string);
            result = DateTime.FromBinary(dateData);
        }
        return result;
    }


}


