using Assets.Scripts.Other;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenRating : MonoBehaviour
{
    public GameObject screen;
    public PlayerRatingView myProfile;
    public PlayerRatingView[] neighbors;
    public PlayerRatingView[] firstThreeTheBest;


    void Show(bool show)
    {
        screen.SetActive(show);
    }
}
