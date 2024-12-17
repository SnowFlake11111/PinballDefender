using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using MoreMountains.NiceVibrations;

public class UseProfile : MonoBehaviour
{
    #region Pinball Defender
    public static int LevelProgress
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CAMPAIGN_PROGRESS, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CAMPAIGN_PROGRESS, value);
            PlayerPrefs.Save();
            EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.LEVEL_PROGRESS_CHANGE);
        }
    }

    public static int GameGem
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.GAME_GEM, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.GAME_GEM, value);
            PlayerPrefs.Save();
            EventDispatcher.EventDispatcher.Instance.PostEvent(EventID.CHANGE_GEM);
        }
    }

    public static int CampaignDamageUpgradeCount
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CAMPAIGN_DAMAGE_UPGRADE_COUNT, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CAMPAIGN_DAMAGE_UPGRADE_COUNT, value);
            PlayerPrefs.Save();
        }
    }

    public static int CampaignBounceUpgradeCount
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CAMPAIGN_BOUNCE_UPGRADE_COUNT, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CAMPAIGN_BOUNCE_UPGRADE_COUNT, value);
            PlayerPrefs.Save();
        }
    }

    public static int CampaignMagazineUpgradeCount
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CAMPAIGN_MAGAZINE_UPGRADE_COUNT, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CAMPAIGN_MAGAZINE_UPGRADE_COUNT, value);
            PlayerPrefs.Save();
        }
    }

    public static int CampaignReloadSpeedUpgradeCount
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CAMPAIGN_RELOAD_SPEED_UPGRADE_COUNT, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CAMPAIGN_RELOAD_SPEED_UPGRADE_COUNT, value);
            PlayerPrefs.Save();
        }
    }

    public static int CampaignCreditsGainRateUpgradeCount
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CAMPAIGN_CREDITS_GAIN_RATE_UPGRADE_COUNT, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CAMPAIGN_CREDITS_GAIN_RATE_UPGRADE_COUNT, value);
            PlayerPrefs.Save();
        }
    }

    public static int CampaignMaxCreditsUpgradeCount
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CAMPAIGN_MAX_CREDITS_UPGRADE_COUNT, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CAMPAIGN_MAX_CREDITS_UPGRADE_COUNT, value);
            PlayerPrefs.Save();
        }
    }

    public static int CampaignGateHealthUpgradeCount
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CAMPAIGN_GATE_HEALTH_UPGRADE_COUNT, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CAMPAIGN_GATE_HEALTH_UPGRADE_COUNT, value);
            PlayerPrefs.Save();
        }
    }

    public static int CampaignBallTextureChoice
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CAMPAIGN_BALL_TEXTURE_CHOICE, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CAMPAIGN_BALL_TEXTURE_CHOICE, value);
            PlayerPrefs.Save();
        }
    }

    public static int CampaignBallTrailChoice
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CAMPAIGN_BALL_TRAIL_CHOICE, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CAMPAIGN_BALL_TRAIL_CHOICE, value);
            PlayerPrefs.Save();
        }
    }

    public static int CampaignSlot1Unit
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CAMPAIGN_SLOT_1_UNIT, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CAMPAIGN_SLOT_1_UNIT, value);
            PlayerPrefs.Save();
        }
    }

    public static int CampaignSlot2Unit
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CAMPAIGN_SLOT_2_UNIT, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CAMPAIGN_SLOT_2_UNIT, value);
            PlayerPrefs.Save();
        }
    }

    public static int CampaignSlot3Unit
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CAMPAIGN_SLOT_3_UNIT, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CAMPAIGN_SLOT_3_UNIT, value);
            PlayerPrefs.Save();
        }
    }

    public static int CampaignSlot4Unit
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CAMPAIGN_SLOT_4_UNIT, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CAMPAIGN_SLOT_4_UNIT, value);
            PlayerPrefs.Save();
        }
    }

    public static int CampaignSlot5Unit
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.CAMPAIGN_SLOT_5_UNIT, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CAMPAIGN_SLOT_5_UNIT, value);
            PlayerPrefs.Save();
        }
    }

    public static int MultiplayerPlayer_1BallTextureChoice
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.MULTIPLAYER_PLAYER_1_BALL_TEXTURE_CHOICE, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.MULTIPLAYER_PLAYER_1_BALL_TEXTURE_CHOICE, value);
            PlayerPrefs.Save();
        }
    }

    public static int MultiplayerPlayer_1BallTrailChoice
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.MULTIPLAYER_PLAYER_1_BALL_TRAIL_CHOICE, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.MULTIPLAYER_PLAYER_1_BALL_TRAIL_CHOICE, value);
            PlayerPrefs.Save();
        }
    }

    public static int MultiplayerPlayer_2BallTextureChoice
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.MULTIPLAYER_PLAYER_2_BALL_TEXTURE_CHOICE, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.MULTIPLAYER_PLAYER_2_BALL_TEXTURE_CHOICE, value);
            PlayerPrefs.Save();
        }
    }

    public static int MultiplayerPlayer_2BallTrailChoice
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.MULTIPLAYER_PLAYER_2_BALL_TRAIL_CHOICE, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.MULTIPLAYER_PLAYER_2_BALL_TRAIL_CHOICE, value);
            PlayerPrefs.Save();
        }
    }

    public static int MultiplayerPlayer_1Slot1Unit
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.MULTIPLAYER_PLAYER_1_SLOT_1_UNIT, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.MULTIPLAYER_PLAYER_1_SLOT_1_UNIT, value);
            PlayerPrefs.Save();
        }
    }

    public static int MultiplayerPlayer_1Slot2Unit
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.MULTIPLAYER_PLAYER_1_SLOT_2_UNIT, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.MULTIPLAYER_PLAYER_1_SLOT_2_UNIT, value);
            PlayerPrefs.Save();
        }
    }

    public static int MultiplayerPlayer_1Slot3Unit
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.MULTIPLAYER_PLAYER_1_SLOT_3_UNIT, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.MULTIPLAYER_PLAYER_1_SLOT_3_UNIT, value);
            PlayerPrefs.Save();
        }
    }

    public static int MultiplayerPlayer_1Slot4Unit
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.MULTIPLAYER_PLAYER_1_SLOT_4_UNIT, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.MULTIPLAYER_PLAYER_1_SLOT_4_UNIT, value);
            PlayerPrefs.Save();
        }
    }

    public static int MultiplayerPlayer_1Slot5Unit
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.MULTIPLAYER_PLAYER_1_SLOT_5_UNIT, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.MULTIPLAYER_PLAYER_1_SLOT_5_UNIT, value);
            PlayerPrefs.Save();
        }
    }

    public static int MultiplayerPlayer_2Slot1Unit
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.MULTIPLAYER_PLAYER_2_SLOT_1_UNIT, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.MULTIPLAYER_PLAYER_2_SLOT_1_UNIT, value);
            PlayerPrefs.Save();
        }
    }

    public static int MultiplayerPlayer_2Slot2Unit
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.MULTIPLAYER_PLAYER_2_SLOT_2_UNIT, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.MULTIPLAYER_PLAYER_2_SLOT_2_UNIT, value);
            PlayerPrefs.Save();
        }
    }

    public static int MultiplayerPlayer_2Slot3Unit
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.MULTIPLAYER_PLAYER_2_SLOT_3_UNIT, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.MULTIPLAYER_PLAYER_2_SLOT_3_UNIT, value);
            PlayerPrefs.Save();
        }
    }

    public static int MultiplayerPlayer_2Slot4Unit
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.MULTIPLAYER_PLAYER_2_SLOT_4_UNIT, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.MULTIPLAYER_PLAYER_2_SLOT_4_UNIT, value);
            PlayerPrefs.Save();
        }
    }

    public static int MultiplayerPlayer_2Slot5Unit
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.MULTIPLAYER_PLAYER_2_SLOT_5_UNIT, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.MULTIPLAYER_PLAYER_2_SLOT_5_UNIT, value);
            PlayerPrefs.Save();
        }
    }

    public static bool WarriorUnlocked
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.WARRIOR_UNLOCKED, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.WARRIOR_UNLOCKED, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static bool RangerUnlocked
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.RANGER_UNLOCKED, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.RANGER_UNLOCKED, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static bool MageUnlocked
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.MAGE_UNLOCKED, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.MAGE_UNLOCKED, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static bool EnforcerUnlocked
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.ENFORCER_UNLOCKED, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.ENFORCER_UNLOCKED, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static bool DemonUnlocked
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.DEMON_UNLOCKED, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.DEMON_UNLOCKED, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static bool MonsterUnlocked
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.MONSTER_UNLOCKED, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.MONSTER_UNLOCKED, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static bool HealerUnlocked
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.HEALER_UNLOCKED, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.HEALER_UNLOCKED, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static bool BerserkerUnlocked
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.BERSERKER_UNLOCKED, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.BERSERKER_UNLOCKED, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static bool BloodMageUnlocked
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.BLOODMAGE_UNLOCKED, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.BLOODMAGE_UNLOCKED, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static bool KingUnlocked
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.KING_UNLOCKED, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.KING_UNLOCKED, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static int ScoreBattleMatchesCounter
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.SCORE_BATTLE_MATCHES_COUNTER, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.SCORE_BATTLE_MATCHES_COUNTER, value);
            PlayerPrefs.Save();
        }
    }

    public static int DefenderBattleMatchesCounter
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.DEFENDER_BATTLE_MATCHES_COUNTER, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.DEFENDER_BATTLE_MATCHES_COUNTER, value);
            PlayerPrefs.Save();
        }
    }

    public static bool Achievement_FinishedAct1
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.ACHIEVEMENT_FINISHED_ACT_1, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.ACHIEVEMENT_FINISHED_ACT_1, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static bool Achievement_FinishedAct2
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.ACHIEVEMENT_FINISHED_ACT_2, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.ACHIEVEMENT_FINISHED_ACT_2, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static bool Achievement_KillWith10thBounce
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.ACHIEVEMENT_KILL_WITH_10TH_BOUNCE, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.ACHIEVEMENT_KILL_WITH_10TH_BOUNCE, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static bool Achievement_Completed3ScoreBattleMatches
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.ACHIEVEMENT_COMPLETED_3_SCORE_BATTLE_MATCHES, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.ACHIEVEMENT_COMPLETED_3_SCORE_BATTLE_MATCHES, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static bool Achievement_Completed3DefenderBattleMatches
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.ACHIEVEMENT_COMPLETED_3_DEFENDER_BATTLE_MATCHES, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.ACHIEVEMENT_COMPLETED_3_DEFENDER_BATTLE_MATCHES, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static bool Achievement_FullyUpgradedDamage
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.ACHIEVEMENT_FULLY_UPGRADED_DAMAGE, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.ACHIEVEMENT_FULLY_UPGRADED_DAMAGE, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static bool Achievement_FullyUpgradedBounce
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.ACHIEVEMENT_FULLY_UPGRADED_BOUNCE, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.ACHIEVEMENT_FULLY_UPGRADED_BOUNCE, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static bool Achievement_FullyUpgradedReloadSpeed
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.ACHIEVEMENT_FULLY_UPGRADED_RELOAD_SPEED, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.ACHIEVEMENT_FULLY_UPGRADED_RELOAD_SPEED, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static bool Achievement_FullyUpgradedMagazine
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.ACHIEVEMENT_FULLY_UPGRADED_MAGAZINE, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.ACHIEVEMENT_FULLY_UPGRADED_MAGAZINE, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static bool Achievement_FullyUpgradedCreditsGainRate
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.ACHIEVEMENT_FULLY_UPGRADED_CREDITS_GAIN_RATE, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.ACHIEVEMENT_FULLY_UPGRADED_CREDITS_GAIN_RATE, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static bool Achievement_FullyUpgradedMaxCredits
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.ACHIEVEMENT_FULLY_UPGRADED_MAX_CREDITS, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.ACHIEVEMENT_FULLY_UPGRADED_MAX_CREDITS, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static bool Achievement_FullyUpgradedGateHealth
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.ACHIEVEMENT_FULLY_UPGRADED_GATE_HEALTH, 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.ACHIEVEMENT_FULLY_UPGRADED_GATE_HEALTH, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
    #endregion

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
            return PlayerPrefs.GetInt(StringHelper.CURRENT_LEVEL, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.CURRENT_LEVEL, value);
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
    public static int NumberOfAdsInPlay
    {
        get
        {
            return PlayerPrefs.GetInt(StringHelper.NUMBER_OF_ADS_IN_PLAY, 0);
        }
        set
        {
            PlayerPrefs.SetInt(StringHelper.NUMBER_OF_ADS_IN_PLAY, value);
            PlayerPrefs.Save();
        }
    }
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


