using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera gameCamera;
    [SerializeField] float sensetiveOfBias;

    public void Init(UIBase UI)
    {
        this.UI = UI;
    }

    #region private

    Vector3 NULL_VECTOR = new Vector3();
    Vector3 CENTER_OF_SCREEN = new Vector3(Screen.width / 2, Screen.height / 2);
    const float BIAS_HEIGHT = 5;


    UIBase UI;
    Vector3 prevMousePoint = new Vector3();

    void TryChangeCameraHeigh(Vector3 mouseInWorld)
    {
        float scroll = Input.GetAxis(InputsLibrary.MOUSE_SCROLL_WHEEL);

        if (scroll == 0)
            return;

        Vector3 camPosition = gameCamera.transform.position;
        Vector3 direction = mouseInWorld - camPosition;

        float newX = camPosition.x;
        float newY = camPosition.y;
        float newZ = camPosition.z;

        float biasForce = BIAS_HEIGHT / camPosition.y;

        if (scroll < 0)
        {
            newX -= direction.x * biasForce;
            newZ -= direction.z * biasForce;
            newY += BIAS_HEIGHT;
        }
        else
        {
            newX += direction.x * biasForce;
            newZ += direction.z * biasForce;
            newY -= BIAS_HEIGHT;
        }

        gameCamera.transform.position = new Vector3(newX, newY, newZ);
    }

    void TryChangeCameraPosition(Vector3 mouseInWorld)
    {
        if (mouseInWorld == prevMousePoint)
            return;

        Vector3 camPosition = gameCamera.transform.position;
        Vector3 biasForce = mouseInWorld - prevMousePoint; //Сила смещения
      
        float newX = camPosition.x - biasForce.x;        
        float newZ = camPosition.z - biasForce.z;
        float newY = camPosition.y;

        gameCamera.transform.position = new Vector3(newX, newY, newZ);
    }

    void Update()
    {
        Vector3 mouseInWorld = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameCamera.transform.position.y));

        if (Input.GetButton(InputsLibrary.RIGHT_MOUSE))
            if (prevMousePoint != NULL_VECTOR)
            {
                TryChangeCameraPosition(mouseInWorld);
                TryChangeCameraHeigh(mouseInWorld);
            }

        if (Input.GetButtonUp(InputsLibrary.RIGHT_MOUSE))
        {
            prevMousePoint = NULL_VECTOR;
            UI.UnblockUI();
        }

        if (!UI.GetIsMouseEnter())
        {
            TryChangeCameraHeigh(mouseInWorld);

            if (Input.GetButtonDown(InputsLibrary.RIGHT_MOUSE))
            {
                prevMousePoint = mouseInWorld;
                UI.BlockUI();
            }
        }
    }

    #endregion
}
