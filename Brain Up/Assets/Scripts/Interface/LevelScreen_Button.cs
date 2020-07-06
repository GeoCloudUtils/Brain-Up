using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelScreen_Button : MonoBehaviour
{
    public int startLevelIndex;
    public int endLevelIndex;
    public bool isLocked = true;
    public bool complete = false;
    public bool inProgress = false;
    public GameObject[] tempImages;
    public TextMeshProUGUI indicatorText;

    private void Start()
    {
        SetLevelProperties();
    }

    private void SetLevelProperties()
    {
        tempImages[0].transform.GetChild(0).gameObject.SetActive(inProgress && !complete);
        tempImages[0].SetActive(inProgress);
        tempImages[1].SetActive(isLocked);
        if (isLocked)
            indicatorText.SetText("0/56");
        else
            indicatorText.SetText(complete ? "100%" : "17/56");
    }
}
