// **author Geo
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AdScreenManager : MonoBehaviour
{
    public DOTweenAnimation removeAds_screen;
    public Button closeAdsBtn;
    public Button[] removeButtons;

    protected bool captureEvents = true;

    enum RemoveType
    {
        SIMPLE,
        REWARDED
    }
    void Start()
    {
        closeAdsBtn.onClick.AddListener(CloseAdScreen);
        foreach (Button r_button in removeButtons)
            r_button.onClick.AddListener(delegate { RemoveButtonClick(r_button); });
        captureEvents = true;
        if (!removeAds_screen.tween.IsPlaying())
            removeAds_screen.DOPlay();
    }

    private void RemoveButtonClick(Button r_button)
    {
        RemoveAds(System.Array.IndexOf(removeButtons, r_button));
    }

    protected void RemoveAds(int index)
    {
        RemoveType type = index == 0 ? RemoveType.SIMPLE : RemoveType.REWARDED;
        // do stuff
    }

    private void CloseAdScreen()
    {
        captureEvents = false;
        removeAds_screen.transform.DOLocalMoveY(-1400f, 0.25f).SetEase(Ease.InOutBack).OnComplete(() =>
        { removeAds_screen.transform.parent.gameObject.SetActive(false); });
    }

    private void Update()
    {
        if (!captureEvents)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIElement())
                CloseAdScreen();
        }
    }
    public static bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }
    public static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.tag == "ad_screen")
                return true;
        }
        return false;
    }
    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }

}
