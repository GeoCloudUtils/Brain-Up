/// author GEO
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Setting_manager : MonoBehaviour
{
    public DOTweenAnimation panel_tween;
    public Button music_button;
    public Button sfx_button;
    public Button close_button;
    public bool musicOn = false;
    public bool sfxOn = false;

    protected bool captureEvents = true;
    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        if (!PlayerPrefs.HasKey("Music_state"))
            PlayerPrefs.SetString("Music_state", "ON");
        if (!PlayerPrefs.HasKey("Sfx_state"))
            PlayerPrefs.SetString("Sfx_state", "ON");
        musicOn = PlayerPrefs.GetString("Music_state") == "ON";
        sfxOn = PlayerPrefs.GetString("Sfx_state") == "ON";
    }
    private void OnEnable()
    {
        captureEvents = true;
    }
    void Start()
    {
        if (!panel_tween.tween.IsPlaying())
            panel_tween.DOPlay();
        if (!GetComponent<DOTweenAnimation>().tween.IsPlaying())
            GetComponent<DOTweenAnimation>().DOPlay();

        music_button.onClick.AddListener(delegate { SetMusicState(true); });
        sfx_button.onClick.AddListener(delegate { SetSfxState(true); });
        close_button.onClick.AddListener(CloseSettings);

        SetMusicState(false);
        SetSfxState(false);
    }

    private void CloseSettings()
    {
        captureEvents = false;
        panel_tween.transform.DOLocalMoveY(-1400f, 0.25f).SetEase(Ease.InOutBack).OnComplete(() =>
        { panel_tween.transform.parent.gameObject.SetActive(false); });

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
            if (curRaysastResult.gameObject.tag == "setting_screen")
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

    private void Update()
    {
        if (!captureEvents)
            return;
        musicOn = PlayerPrefs.GetString("Music_state") == "ON";
        sfxOn = PlayerPrefs.GetString("Sfx_state") == "ON";
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIElement())
                CloseSettings();
        }
    }
    private void SetMusicState(bool is_event)
    {
        if (!captureEvents)
            return;
        RectTransform t = music_button.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        Image toggle_bg = music_button.transform.GetChild(0).GetComponent<Image>();

        bool isTrue = is_event ? t.anchoredPosition.x > 0f : PlayerPrefs.GetString("Music_state") != "ON";
        toggle_bg.DOColor(!isTrue ? new Color(0.5f, 1f, 1f, 1f) : new Color(0.3f, 0.3f, 0.3f, 1f), 0.1f);
        t.DOLocalMoveX(isTrue ? -33f : 33f, 0.1f).SetEase(Ease.InOutBack);
        if (is_event)
            PlayerPrefs.SetString("Music_state", isTrue ? "OFF" : "ON");
    }

    private void SetSfxState(bool is_event)
    {
        if (!captureEvents)
            return;
        RectTransform t = sfx_button.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        Image toggle_bg = sfx_button.transform.GetChild(0).GetComponent<Image>();

        bool isTrue = is_event ? t.anchoredPosition.x > 0f : PlayerPrefs.GetString("Sfx_state") != "ON";
        toggle_bg.DOColor(!isTrue ? new Color(0.5f, 1f, 1f, 1f) : new Color(0.3f, 0.3f, 0.3f, 1f), 0.1f);
        t.DOLocalMoveX(isTrue ? -33f : 33f, 0.1f).SetEase(Ease.InOutBack);
        if (is_event)
            PlayerPrefs.SetString("Sfx_state", isTrue ? "OFF" : "ON");
    }
}
