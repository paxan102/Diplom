using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image blockForUI;
    
    public void Init()
    {
        blockForUI.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!EventSystem.current.alreadySelecting)
            EventSystem.current.SetSelectedGameObject(this.gameObject);
        isMouseEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseEnter = false;
    }

    public bool GetIsMouseEnter()
    {
        return isMouseEnter;
    }

    public void BlockUI()
    {
        blockForUI.gameObject.SetActive(true);
    }

    public void UnblockUI()
    {
        blockForUI.gameObject.SetActive(false);
    }

    #region private

    bool isMouseEnter = false;

    #endregion
}