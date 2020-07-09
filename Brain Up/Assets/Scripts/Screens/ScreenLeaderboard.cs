using Assets.Framework.Assets.Scripts.Leaderboard;
using Assets.Scripts.Other;
using GooglePlayGames.BasicApi;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenLeaderboard : MonoBehaviour
{
    public GameObject screen;
    public LeaderboardPlayerView myProfile;
    public LeaderboardPlayerView[] neighbors;
    public LeaderboardPlayerView[] firstThreeTheBest;
    public GameObject myProfileOverlay;
    public GameObject neighborsOverlay;
    public GameObject firstThreeTheBestOverlay;
    public GameObject notAuthentificatedOverlay;
    public GameObject authentificatingOverlay;
    public TMP_Text message;
    //
    private LeaderboardController _controller;
    //tmp
    public Texture2D testTexture;




    private void Start()
    {
        _controller = LeaderboardController.Instance;
    }

    public void Show(bool show)
    {
        if(!_controller.IsAuthentificated())
        {
            notAuthentificatedOverlay.SetActive(true);
            return;
        }

        myProfileOverlay.SetActive(true);
        neighborsOverlay.SetActive(true);
        firstThreeTheBestOverlay.SetActive(true);


        string leadId = GPGSIds.leaderboard_experience;
        int neighboursCount = neighbors.Length;
        int firstPlayersCount = firstThreeTheBest.Length;

        _controller.ReceiveScoreForLocalUser(leadId, (data) =>
        {
            if (data != null)
            {
                Debug.LogWarning("* LocalPlayer data received. Filling...");
                Fill(data, new LeaderboardPlayerView[] { myProfile });
                myProfileOverlay.SetActive(false);
            }
            else
            {
                Debug.LogWarning("* LocalPlayer data received. Data is null!");
                //data = new LeaderboardPlayerModel[] {
                //    new LeaderboardPlayerModel("RomkaTest",testTexture, 111, 40390) };
                //Fill(data, new LeaderboardPlayerView[] { myProfile });
            }
        });

        _controller.ReceiveScore(neighboursCount, LeaderboardStart.PlayerCentered, leadId, (players) =>
        {
            if (players != null)
            {
                Debug.LogWarning("* Neighbours data received. Filling...");
                Fill(players, neighbors);
                neighborsOverlay.SetActive(false);
            }
            else
            {
                Debug.LogWarning("* Neighbours data received. Data is null.");
                //players = new LeaderboardPlayerModel[] {
                //    new LeaderboardPlayerModel("Neighbour_1",testTexture, 111, 34),
                //    new LeaderboardPlayerModel("Neighbour_2",testTexture, 754, 789),
                //    new LeaderboardPlayerModel("Neighbour_3",testTexture, 343, 11553),
                //    new LeaderboardPlayerModel("Neighbour_4",testTexture, 890, 46463),
                //    new LeaderboardPlayerModel("Neighbour_5",testTexture, 34, 456),
                //};
                //Fill(players, neighbors);
            }

        });

        _controller.ReceiveScore(firstPlayersCount, LeaderboardStart.TopScores, leadId, (players) =>
        {
            if (players != null)
            {
                Debug.LogWarning("* First 3 data received. Filling...");
                Fill(players, firstThreeTheBest);
                firstThreeTheBestOverlay.SetActive(false);
            }
            else
            {
                Debug.LogWarning("* First 3 data received. Data is null.");
                //players = new LeaderboardPlayerModel[] {
                //    new LeaderboardPlayerModel("FirstPlayer_1",testTexture, 1, 3),
                //    new LeaderboardPlayerModel("FirstPlayer_2",testTexture, 2, 79),
                //    new LeaderboardPlayerModel("FirstPlayer_3",testTexture, 3, 1153),
                //};
                //Fill(players, firstThreeTheBest);
            }
        });


        screen.SetActive(show);
    }


    public void OnSignInClicked()
    {
        StartCoroutine(AuthentificateCoroutine());
    }


    #region Helpers

    private IEnumerator AuthentificateCoroutine()
    {
        authentificatingOverlay.SetActive(true);
        message.text = "Signing in...";
        bool processed = false;
        bool signed = false;
        _controller.Authenticate((success) =>
            {
                if (success)
                    signed = true;
                processed = true;
            });

        while (!processed)
            yield return new WaitForSeconds(0.5f);

        if (signed)
            message.text = "Succes! Loading data...";
        else
            message.text = "Can't sign in.";
        yield return new WaitForSeconds(2f);
        if (signed)
        {
            notAuthentificatedOverlay.SetActive(false);
            Show(true);
        }
        authentificatingOverlay.SetActive(false);
    }

    private void Fill(LeaderboardPlayerModel[] models, LeaderboardPlayerView[] views)
    {
        int count = models.Length;
        for (int a = 0; a < views.Length; ++a)
        {
            var view = views[a];

            if (a < count)
            {
                var model = models[a];
                view.avatar.sprite = TextureToSprite(model.avatar);
                view.coins.text = model.score.ToString();
                view.name.text = model.name;
                view.place.text = model.rank.ToString();
                view.gameObject.SetActive(true);
            }
            else
                view.gameObject.SetActive(false);
        }
    }


    private Sprite TextureToSprite(Texture2D tex)
    {
        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
    }

    #endregion

}
