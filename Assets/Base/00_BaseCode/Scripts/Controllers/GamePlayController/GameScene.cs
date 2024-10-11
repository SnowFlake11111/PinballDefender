using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using MoreMountains.NiceVibrations;
using EventDispatcher;

public class GameScene : BaseScene
{
    #region Public Variables

    [Header("-----MISC-----")]
    public Button settingBtn;

    #endregion

    #region Private Variables
    [NonSerialized] public int chosenBoosterId;
    bool conveyorIsActivate = false;
    #endregion
    public void Init()
    {
        //ShowBanner();
        settingBtn.onClick.AddListener(delegate { OpenSetting(); });

        GameController.Instance.musicManager.MusicTransition();
    }


    void OpenSetting()
    {
        GameController.Instance.musicManager.PlayClickSound();

        SettingBox.Setup().Show();
    }

    public override void OnEscapeWhenStackBoxEmpty()
    {
        throw new NotImplementedException();
    }

    #region Listener Functions

    #endregion
}
