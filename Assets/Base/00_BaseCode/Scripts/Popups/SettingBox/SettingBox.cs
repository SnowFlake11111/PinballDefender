using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class SettingBox : BaseBox
{
    #region instance
    private static SettingBox instance;
    public static SettingBox Setup( bool isSaveBox = false, Action actionOpenBoxSave = null)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<SettingBox>(PathPrefabs.SETTING_BOX));
            instance.Init();
        }

        instance.InitState();
        return instance;
    }
    #endregion
    #region Var

    [Header("-----BUTTONS-----")]
    [SerializeField] private Button btnClose;
  

    [SerializeField] private Button btnVibration;
    [SerializeField] private Button btnMusic;
    [SerializeField] private Button btnSound;

    public Button returnToMenu;

    [Header("-----SLIDER (NOT INTERACTABLE)-----")]
    [SerializeField] private Slider vibrationSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    
 
    #endregion
    private void Init()
    {
        btnClose.onClick.AddListener(delegate { OnClickButtonClose(); }); 
        btnVibration.onClick.AddListener(delegate { OnClickBtnVibration(); });
        btnMusic.onClick.AddListener(delegate { OnClickBtnMusic(); });
        btnSound.onClick.AddListener(delegate { OnClickBtnSound(); });

        returnToMenu.onClick.AddListener(delegate { ReturnToMenu(); });

    }
    private void InitState()
    {
        SetUpBtn();
    }
    private void SetUpBtn()
    {
        if (GameController.Instance.useProfile.OnVibration)
        {
            vibrationSlider.DOValue(1, 0.25f).SetEase(Ease.Linear);
          //  btnVibration.GetComponent<Image>().sprite = spriteBtnOn;
        }
        else
        {
            vibrationSlider.DOValue(0, 0.25f).SetEase(Ease.Linear);
            // btnVibration.GetComponent<Image>().sprite = spriteBtnOff;
        }

        if (GameController.Instance.useProfile.OnMusic)
        {
            musicSlider.DOValue(1, 0.25f).SetEase(Ease.Linear);
            //    btnMusic.GetComponent<Image>().sprite = spriteBtnOn;
        }
        else
        {
            musicSlider.DOValue(0, 0.25f).SetEase(Ease.Linear);
            //  btnMusic.GetComponent<Image>().sprite = spriteBtnOff;
        }

        if (GameController.Instance.useProfile.OnSound)
        {
            soundSlider.DOValue(1, 0.25f).SetEase(Ease.Linear);
            // btnSound.GetComponent<Image>().sprite = spriteBtnOn;
        }
        else
        {
            soundSlider.DOValue(0, 0.25f).SetEase(Ease.Linear);
            //  btnSound.GetComponent<Image>().sprite = spriteBtnOff;
        }
        //imageVibration.SetNativeSize();
        //imageMusic.SetNativeSize();
        //imageSound.SetNativeSize();

        if(SceneManager.GetActiveScene().name != "GamePlay")
        {
            returnToMenu.gameObject.SetActive(false);
        }
        else
        {
            returnToMenu.gameObject.SetActive(true);
        }
    }

  
    private void OnClickBtnVibration()
    {
        GameController.Instance.musicManager.PlayClickSound();

        if (GameController.Instance.useProfile.OnVibration)
        {
            GameController.Instance.useProfile.OnVibration = false;
        }
        else
        {
            GameController.Instance.useProfile.OnVibration = true;
        }
        SetUpBtn();
    }

    private void OnClickBtnMusic()
    {
        GameController.Instance.musicManager.PlayClickSound();

        if (GameController.Instance.useProfile.OnMusic)
        {
            GameController.Instance.useProfile.OnMusic = false;
        }
        else
        {
            GameController.Instance.useProfile.OnMusic = true;
        }
        SetUpBtn();
    }
    private void OnClickBtnSound()
    {
        GameController.Instance.musicManager.PlayClickSound();

        if (GameController.Instance.useProfile.OnSound)
        {
            GameController.Instance.useProfile.OnSound = false;
        }
        else
        {
            GameController.Instance.useProfile.OnSound = true;
        }
        SetUpBtn();
    }

    void ReturnToMenu()
    {
        //To do: open pop up warning about losing heart
        //SceneManager.LoadScene("HomeScene");

        GameController.Instance.musicManager.PlayClickSound();
    }


    private void OnClickButtonClose()
    {
        GameController.Instance.musicManager.PlayClickSound();

        if (SceneManager.GetActiveScene().name == "HomeScene")
        {
            
        }

        if (SceneManager.GetActiveScene().name == "GamePlay")
        {
            
        }
        Close();
    }

    private void OnClickRestorePurchase()
    {
        GameController.Instance.iapController.RestorePurchases();
    }    

}
