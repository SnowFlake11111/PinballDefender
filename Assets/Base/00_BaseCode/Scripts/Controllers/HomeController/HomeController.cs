using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeController : Singleton<HomeController>
{
    public HomeScene homeScene;

    [Space]
    [Header("Ball Preview rooms")]
    public GameObject previewRoom_1;
    public GameObject previewRoom_2;

    [Space]
    [Header("Ball reference")]
    public Ball room_1Ball;
    public Ball room_2Ball;

    protected override void OnAwake()
    {
      //  GameController.Instance.currentScene = SceneType.MainHome;

    }

    private void Start()
    {
        homeScene.Init();
        GameController.Instance.FinishSceneTransition();
        GameController.Instance.musicManager.ChangeMusic(0, 1);
    }

    public void ActivatePreviewRoom_1()
    {
        previewRoom_1.SetActive(true);
    }

    public void ActivatePreviewRoom_2()
    {
        previewRoom_2.SetActive(true);
    }

    public void DeactivatePreviewRoom_1()
    {
        previewRoom_1.SetActive(false);
    }

    public void DeactivatePreviewRoom_2()
    {
        previewRoom_2.SetActive(false);
    }

    public void UpdateTextureToBall_1(int id)
    {
        room_1Ball.AssignNewTexture(GameController.Instance.gameModeData.GetBallSkin(id));
    }

    public void UpdateTextureToBall_2(int id)
    {
        room_2Ball.AssignNewTexture(GameController.Instance.gameModeData.GetBallSkin(id));
    }

    public void UpdateTrailBall_1(int id)
    {
        room_1Ball.AssignNewTrail(GameController.Instance.gameModeData.GetBallTrail(id));
    }

    public void UpdateTrailBall_2(int id)
    {
        room_2Ball.AssignNewTrail(GameController.Instance.gameModeData.GetBallTrail(id));
    }
}
