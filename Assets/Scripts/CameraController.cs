using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera gameCamera;
    [SerializeField] float sensetiveOfBias;

    public void Init(UIManager UIManager)
    {
        this.UIManager = UIManager;
    }

    #region private

    Vector3 NULL_VECTOR = new Vector3();
    const float BIAS_HEIGHT = 5;
    const float MIN_HEIGHT = 0;
    const float MAX_HEIGHT = 150;


    UIManager UIManager;
    Vector3 startMousePointInWorld = new Vector3();

    void TryChangeCameraHeigh(Vector3 currentMousePointInWorld)
    {
        float scroll = Input.GetAxis(InputsLibrary.MOUSE_SCROLL_WHEEL);

        if (scroll == 0)
            return;

        Vector3 camPosition = gameCamera.transform.position;
        Vector3 direction = currentMousePointInWorld - camPosition;

        float biasForce = BIAS_HEIGHT / camPosition.y;
        float biasHeight = BIAS_HEIGHT;

        if (scroll > 0)
        {
            biasForce *= -1;
            biasHeight *= -1;
        }

        float newY = camPosition.y + biasHeight;

        if (newY <= MIN_HEIGHT || newY >= MAX_HEIGHT)
            return;

        float newX = camPosition.x - direction.x * biasForce;
        float newZ = camPosition.z - direction.z * biasForce;

        gameCamera.transform.position = new Vector3(newX, newY, newZ);
    }

    void TryChangeCameraPosition(Vector3 currentMousePointInWorld)
    {
        if (currentMousePointInWorld == startMousePointInWorld)
            return;

        Vector3 camPosition = gameCamera.transform.position;
        Vector3 biasForce = currentMousePointInWorld - startMousePointInWorld; //Сила смещения
      
        float newX = camPosition.x - biasForce.x;        
        float newZ = camPosition.z - biasForce.z;
        float newY = camPosition.y;

        gameCamera.transform.position = new Vector3(newX, newY, newZ);
    }

    void Update()
    {
        Vector3 currentMousePointInWorld = Camera.main.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x, Input.mousePosition.y, gameCamera.transform.position.y));

        if (Input.GetButton(InputsLibrary.RIGHT_MOUSE))
            if (startMousePointInWorld != NULL_VECTOR)
            {
                TryChangeCameraPosition(currentMousePointInWorld);
                TryChangeCameraHeigh(currentMousePointInWorld);
            }

        if (Input.GetButtonUp(InputsLibrary.RIGHT_MOUSE))
        {
            startMousePointInWorld = NULL_VECTOR;
            UIManager.UnblockUI();
        }

        if (!UIManager.GetIsMouseEnter())
        {
            TryChangeCameraHeigh(currentMousePointInWorld);

            if (Input.GetButtonDown(InputsLibrary.RIGHT_MOUSE))
            {
                startMousePointInWorld = currentMousePointInWorld;
                UIManager.BlockUI();
            }
        }
    }

    #endregion
}
