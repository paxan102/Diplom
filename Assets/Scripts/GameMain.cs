using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    [SerializeField] CameraController cameraController;
    [SerializeField] UIBase UI;

    // Use this for initialization
    void Start ()
    {
        Cursor.lockState = CursorLockMode.Confined;

        UI.Init();
        cameraController.Init(UI);
        
	}

    // Update is called once per frame
    #region private

    #endregion
}
