using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    [SerializeField] CameraController cameraController;
    [SerializeField] UIManager UIManager;
    [SerializeField] PointsManager pointsManager;
    [SerializeField] Buyer[] buyers;

    void Start ()
    {
        Cursor.lockState = CursorLockMode.Confined;

        UIManager.Init();
        cameraController.Init(UIManager);
        pointsManager.Init();
        foreach(Buyer buyer in buyers)
            buyer.Init(pointsManager);
	}

    #region private

    #endregion
}
