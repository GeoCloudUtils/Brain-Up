/// author GEO
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class Nav_manager : MonoBehaviour
{
    public DOTweenAnimation header;
    public GameObject tab_level_screen;
    public GameObject setting_screen;
    public GameObject nav_rel_tips;
    public RectTransform nav_bar;
    public Button[] nav_buttons;
    public Button setting_button;
    public GameObject[] screens;

    private int last_targetIndex = -1;
    void Start()
    {
        foreach (Button btn in nav_buttons)
            btn.onClick.AddListener(delegate { DoNavButtonClick(btn); });
        setting_button.onClick.AddListener(OpenSettings);
        screens[2].gameObject.SetActive(true);
        Tween sc_tween = screens[2].GetComponent<DOTweenAnimation>().tween;
        if (!sc_tween.IsPlaying())
            sc_tween.Play();
        DoNavButtonClick(nav_buttons[2]);
    }

    private void OpenSettings()
    {
        if (!setting_screen.activeSelf)
            setting_screen.SetActive(true);
    }

    private void DoNavButtonClick(Button btn)
    {
        int targetIndex = System.Array.IndexOf(nav_buttons, btn);
        header.DORestart();
        if (tab_level_screen.activeSelf)
            tab_level_screen.SetActive(false);
        if (last_targetIndex == targetIndex)
            return;
        nav_rel_tips.SetActive(false);
       
        for (int i = 0; i < screens.Length; i++)
        {
            GameObject g = nav_buttons[i].gameObject;
            if (i != targetIndex)
            {
                screens[i].SetActive(false);
                g.transform.GetChild(0).gameObject.SetActive(false);
                g.transform.GetChild(1).GetComponent<Image>().DOColor(targetIndex == 2 ? Color.white : new Color(0.3f, 0.3f, 0.3f, 1f), 0.1f);
            }
        }
        btn.transform.GetChild(0).gameObject.SetActive(true);
        DOTweenAnimation btn_tween = btn.transform.GetChild(0).GetComponent<DOTweenAnimation>();
        if (btn_tween != null)
        {
            if (!btn_tween.tween.IsPlaying())
                btn_tween.DOPlay();
        }
        if (targetIndex != 2)
        {
            header.GetComponent<Image>().DOFade(1f, 0f);
            btn.transform.GetChild(1).GetComponent<Image>().DOColor(new Color(0f, 208f, 255f, 255f), 0.1f);
            nav_bar.GetComponent<Image>().DOColor(new Color(195f, 195f, 195f, 255f), 0.1f);
        }
        else
        {
            header.GetComponent<Image>().DOFade(0f, 0f);
            nav_rel_tips.SetActive(true);
            nav_bar.GetComponent<Image>().DOFade(0.25f, 0.1f);
            for (int i = 0; i < screens.Length; i++)
            {
                GameObject g = nav_buttons[i].gameObject;
                if (i != targetIndex)
                    g.transform.GetChild(0).gameObject.SetActive(false);
                screens[i].SetActive(false);
                g.transform.GetChild(1).GetComponent<Image>().color = Color.white;
            }
        }
        screens[targetIndex].SetActive(true);
        DOTweenAnimation screen_anim = screens[targetIndex].GetComponent<DOTweenAnimation>();
        if (!screen_anim.tween.IsPlaying())
            screen_anim.DOPlay();
        last_targetIndex = targetIndex;
    }
}
