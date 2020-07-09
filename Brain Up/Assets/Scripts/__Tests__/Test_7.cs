using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Multiplayer;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

/*
 https://yottabrain.org/android/google-sign-in-developer-error/
https://github.com/playgameservices/play-games-plugin-for-unity
 https://console.firebase.google.com/
https://console.cloud.google.com/apis/credentials


https://answers.unity.com/questions/1357666/google-play-games-services-authentication-fails.html
1. Google Play Console -> Select your app -> Release Management -> App signing -> "Upload certificate" [Thanks to @taimur_azhar for pointing out a mistake in the quoted manual] : copy SHA-1 (dont copy word 'SHA1:')
2. open https://console.developers.google.com/ , select your project -> credentials -> OAuth 2.0 client IDs -> Edit OAuth client -> Signing-certificate fingerprint -> replace the old SHA1 with copied SHA1 ->save.
3. open you game, you should get sign in -> email selection -> select testers email. Google Play Games Services should work fine now."
 
 */

public class Test_7 : MonoBehaviour
{
    private PlayGamesPlatform platform;

    private void MatchDelegate(TurnBasedMatch match, bool shouldAutoLaunch)
    {

    }

    private void InvitationDelegate(Invitation match, bool shouldAutoAccept)
    {

    }

    private void Start()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            // enables saving game progress.
            //.EnableSavedGames()
            // registers a callback to handle game invitations received while the game is not running.
          //  .WithInvitationDelegate(new InvitationReceivedDelegate(InvitationDelegate))
            // registers a callback for turn based match notifications received while the
            // game is not running.
         //   .WithMatchDelegate(new MatchDelegate(MatchDelegate))
            // requests the email address of the player be available.
            // Will bring up a prompt for consent.
         //   .RequestEmail()
            // requests a server auth code be generated so it can be passed to an
            //  associated back end server application and exchanged for an OAuth token.
         //   .RequestServerAuthCode(false)
            // requests an ID token be generated.  This OAuth token can be used to
            //  identify the player to other services such as Firebase.
          //  .RequestIdToken()
            .Build();

        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        platform = PlayGamesPlatform.Activate();
    }

    public void Authenticate()
    {
        // authenticate user:
        PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (result) => {
            // handle results 
            SignInStatus r;
            Debug.Log("platform.Authenticate result: " + result);
        });
    }

    public void Authenticate_2()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
            }
                Debug.Log("Social.Authenticate result: " + success);
        });
    }

    public void ShowLeaderboard()
    {
        Social.Active.CreateLeaderboard();
    }

    public void ShowUserName()
    {
        string userName = platform.GetUserDisplayName();
        Debug.Log("UserName: " + userName);
    }

    public void ShowUserStats()
    {
        platform.GetPlayerStats((commonStatusCodes, playerStats)=>
        {
            Debug.Log("commonStatusCodes: " + commonStatusCodes);
            Debug.LogFormat("Stats.Valid? {0}; Stats: {1}", playerStats.Valid, playerStats);
        });
    }

    public void ShowLocalUserStats()
    {
        ((PlayGamesLocalUser)Social.localUser).GetStats((commonStatusCodes, playerStats) =>
        {
            // -1 means cached stats, 0 is succeess
            // see  CommonStatusCodes for all values.
            if (commonStatusCodes <= 0 && playerStats.HasDaysSinceLastPlayed())
            {
                Debug.Log("It has been " + playerStats.DaysSinceLastPlayed + " days");
            }
        });
    }

    public void ReportScore(InputField scoreField)
    {
        int score = 0;
        if(int.TryParse(scoreField.text, out score))
        {
            Social.ReportScore(score, "CgkIqpazhZ0EEAIQAQ", (bool success) => {
                Debug.Log("Report Score result: " + success);
            });
        }
    }

    public void LoadScores()
    {
        int nrUsersNeed = 5;
        PlayGamesPlatform.Instance.LoadScores(
          GPGSIds.leaderboard_experience,
          LeaderboardStart.PlayerCentered,
          nrUsersNeed,
          LeaderboardCollection.Public,
          LeaderboardTimeSpan.AllTime,
          (data) =>
          {
              Debug.Log("-> Leaderboard data valid: " + data.Valid);
              Debug.Log("-> Leaderboard status: " + data.Status);
              if (data.Valid)
              {
                  Debug.Log("-> Scores count returned: " + data.Scores.Length);

                  //Show LocalUser score
                  IScore playerScore = data.PlayerScore;
                  Debug.LogFormat("LocalPlayer. Name: {0}; Score: {1}",
                          platform.GetUserDisplayName(), playerScore.value);

                  //Get Data about the rest users
                  int nrUsersReal = Math.Min(nrUsersNeed, data.Scores.Length);
                  string[] ids = new string[nrUsersReal];
                  string[] names = new string[nrUsersReal];
                  int[] scores = new int[nrUsersReal];
                  Texture2D[] avatars = new Texture2D[nrUsersReal];

                  for (int a=0; a < nrUsersReal; ++a)
                  {
                      IScore score = data.Scores[a];
                      ids[a] = score.userID;
                      scores[a] = (int)score.value;
                  }

                  PlayGamesPlatform.Instance.LoadUsers(ids, (profiles) =>
                  {
                      for (int a = 0; a < profiles.Length; ++a)
                      {
                          IUserProfile profile = profiles[a];
                          names[a] = profile.userName;
                          avatars[a] = profile.image;

                          Debug.LogFormat("User {0}. Name: {1}; Score: {2}; Id: {3}; Avatar: {4}",
                             a, names[a], scores[a], ids[a], avatars[a]==null);
                      }
                  });

                  

                  //Social.LoadUsers(userIds, profiles => DisplayLeaderboardEntries(scores, profiles));
              }
          });
    }
}
