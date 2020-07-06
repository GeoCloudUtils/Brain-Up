using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class levelButton : MonoBehaviour
{
    public bool isBlocked = true;
    public GameObject tweenAnimObject;
    public GameObject lock_src;
    public TextMeshProUGUI levelNumber;
    public void SetNumber(int number)
    {
        levelNumber.SetText(number.ToString());
    }
    public void SetAcces(bool blocked)
    {
        lock_src.SetActive(!blocked);
    }
    public void SetMostAdvanced(bool last)
    {
        tweenAnimObject.SetActive(last);
    }
}
