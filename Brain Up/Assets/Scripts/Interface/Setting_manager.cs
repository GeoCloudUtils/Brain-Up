/// author GEO
using Assets.Scripts.Framework.Sounds;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Setting_manager : MonoBehaviour
{
    public AdScreenManager ad_screen;
    public DOTweenAnimation panel_tween;
    public Button music_button;
    public Button sfx_button;
    public Button removeAds_button;
    public Button close_button;
    public Button restore_purchases;
    public bool musicOn = false;
    public bool sfxOn = false;

    public SoundController soundController;

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

        Debug.LogFormat("Music enabled: {0}; Sounds enabled: {1}", musicOn, sfxOn);
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

        music_button.onClick.AddListener(delegate { ToggleMusicState(); });
        sfx_button.onClick.AddListener(delegate { ToggleSfxState(); });
        close_button.onClick.AddListener(CloseSettings);
        removeAds_button.onClick.AddListener(ShowRemoveAdScreen);
        restore_purchases.onClick.AddListener(RestorePurchases);

        SetMusicState(musicOn);
        SetSfxState(sfxOn);
    }

    private void RestorePurchases()
    {
        Debug.LogWarning("TODO: Implement this!!");
    }

    private void ShowRemoveAdScreen()
    {
        if (ad_screen == null)
            throw new ArgumentNullException();
        else
            ad_screen.gameObject.SetActive(true);
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
        if (!captureEvents || ad_screen.gameObject.activeSelf)
            return;
      //  musicOn = PlayerPrefs.GetString("Music_state") == "ON";
      //  sfxOn = PlayerPrefs.GetString("Sfx_state") == "ON";
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIElement())
                CloseSettings();
        }
    }
    private void SetMusicState(bool state)
    {
        if (!captureEvents)
            return;

        RectTransform t = music_button.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        Image toggle_bg = music_button.transform.GetChild(0).GetComponent<Image>();

        toggle_bg.DOColor(state ? new Color(0.3f, 0.3f, 0.3f, 1f) : new Color(0.5f, 1f, 1f, 1f), 0.1f);
        t.anchoredPosition = new Vector2(state ? -33f : 33f, t.anchoredPosition.y);
        PlayerPrefs.SetString("Music_state", state ? "ON" : "OFF");
        soundController.SetMusicVolume(state ? 0 : 1);
        musicOn = state;
    }

    private void SetSfxState(bool state)
    {
        if (!captureEvents)
            return;

        RectTransform t = sfx_button.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        Image toggle_bg = sfx_button.transform.GetChild(0).GetComponent<Image>();

        toggle_bg.DOColor(state ? new Color(0.3f, 0.3f, 0.3f, 1f) : new Color(0.5f, 1f, 1f, 1f), 0.1f);
        t.anchoredPosition = new Vector2(state ? -33f : 33f, t.anchoredPosition.y);
        PlayerPrefs.SetString("Sfx_state", state ? "ON" : "OFF");
        soundController.SetEffectsVolume(state ? 0 : 1);
        sfxOn = state;
    }

    private void ToggleMusicState() { SetMusicState(!musicOn); }
    private void ToggleSfxState() { SetSfxState(!sfxOn); }
}
