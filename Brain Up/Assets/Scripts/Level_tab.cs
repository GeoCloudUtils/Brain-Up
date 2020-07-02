using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Level_tab : MonoBehaviour
{
    public DOTweenAnimation option_Tab;
    public Button closeBtn;
    public Button[] option_buttons;

    protected bool captureEvents = true;

    private Level_button target_button;
    enum UnlockType
    {
        WITH_COINS,
        WITH_AD
    }
    void Start()
    {
        closeBtn.onClick.AddListener(CloseAdScreen);
        foreach (Button r_button in option_buttons)
            r_button.onClick.AddListener(delegate { UnlockButtonClick(r_button); });
    }

    public void setTarget(Level_button target)
    {
        target_button = target;
    }
    private void OnEnable()
    {
        captureEvents = true;
        if (!option_Tab.tween.IsPlaying())
            option_Tab.DOPlay();
    }

    private void UnlockButtonClick(Button r_button)
    {
        RemoveAds(System.Array.IndexOf(option_buttons, r_button));
    }

    protected void RemoveAds(int index)
    {
        UnlockType type = index == 0 ? UnlockType.WITH_COINS : UnlockType.WITH_AD;
        if (target_button != null)
        {
            Debug.Log("Unlock type is: " + type);
            if (type == UnlockType.WITH_AD)
            {
                //target_button.isLocked = false;
                //show ad
            }
            else
            {
                //check coins and do stuff
            }
        }
    }

    private void CloseAdScreen()
    {
        captureEvents = false;
        option_Tab.transform.DOLocalMoveY(-1400f, 0.25f).SetEase(Ease.InOutBack).OnComplete(() =>
        { option_Tab.transform.parent.gameObject.SetActive(false); });
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
