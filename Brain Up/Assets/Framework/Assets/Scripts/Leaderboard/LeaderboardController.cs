using Assets.Scripts.Framework.Other;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Assets.Framework.Assets.Scripts.Leaderboard
{
    public class LeaderboardController : Singleton<LeaderboardController>
    {
        private PlayGamesPlatform platform;

        private new void Awake()
        {
            base.Awake();
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
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

            Authenticate(null);
        }

        internal bool IsAuthentificated()
        {
            return platform.IsAuthenticated();
        }

        public void Authenticate(Action<bool> resultCallback, bool silentMode = false)
        {
            var mode = silentMode ? SignInInteractivity.CanPromptOnce : SignInInteractivity.CanPromptAlways;
            PlayGamesPlatform.Instance.Authenticate(mode, (result) => {
                if(result != SignInStatus.Success)
                    Debug.LogWarning("Leaderboard: Authentification failed. Error: " + result);
                resultCallback?.Invoke(result == SignInStatus.Success);
            });
        }

        public void ReceiveScore(int nrUsersNeed, LeaderboardStart startLocation, string leaderboardId, Action<LeaderboardPlayerModel[]> result)
        { 
            platform.LoadScores(
              leaderboardId,
              startLocation,
              nrUsersNeed,
              LeaderboardCollection.Public,
              LeaderboardTimeSpan.AllTime,
              (data) =>
              {
                  Debug.Log("-> Leaderboard data valid: " + data.Valid);
                  Debug.Log("-> Leaderboard status: " + data.Status);
                  if (data.Valid)
                  {
                      int nrUsersReal = Math.Min(nrUsersNeed, data.Scores.Length);
                      Debug.Log("-> Scores count returned: " + nrUsersReal);

                      //Show LocalUser score
                      IScore playerScore = data.PlayerScore;
                      Debug.LogFormat("LocalPlayer. Name: {0}; Score: {1}",
                              platform.GetUserDisplayName(), playerScore.value);

                      //Get Data about the rest users
                      LeaderboardPlayerModel[] players = new LeaderboardPlayerModel[nrUsersReal];
                      string[] ids = new string[nrUsersReal];
                      for (int a = 0; a < nrUsersReal; ++a)
                      {
                          IScore score = data.Scores[a];
                          players[a] = new LeaderboardPlayerModel();
                          players[a].score = (int)score.value;
                          players[a].rank = score.rank;
                          ids[a] = score.userID;
                      }

                      LoadUsers(ids, players, result);
                  }
                  else
                  {
                      Debug.LogWarning("Leaderboard: Leaderboard data is invalid.");
                      result.Invoke(null);
                  }
              });
        }


        internal void ReceiveScoreForLocalUser(string leaderboardId, Action<LeaderboardPlayerModel[]> result)
        {
            platform.LoadScores(
              leaderboardId,
              LeaderboardStart.TopScores,
              1,
              LeaderboardCollection.Public,
              LeaderboardTimeSpan.AllTime,
              (data) =>
              {
                  Debug.Log("-> Leaderboard data valid: " + data.Valid);
                  Debug.Log("-> Leaderboard status: " + data.Status);
                  if (data.Valid)
                  {
                      Debug.Log("-> Scores count returned: " + data.Scores.Length);

                      IScore playerScore = data.PlayerScore;
                      string[] ids = new string[] { playerScore.userID };
                      LeaderboardPlayerModel[] players = new LeaderboardPlayerModel[1];
                      players[0] = new LeaderboardPlayerModel();
                      players[0].score = (int)playerScore.value;
                      players[0].rank = playerScore.rank;
                     
                      LoadUsers(ids, players, result);
                  }
                  else
                  {
                      Debug.LogWarning("Leaderboard: Leaderboard data is invalid.");
                      result.Invoke(null);
                  }
              });
         }

        public void SendScore(int score, string leaderboardId)
        {
            platform.ReportScore(score, leaderboardId, (bool success) => {
                if(!success)
                    Debug.LogWarning("Leaderboard: Score not sent.");
            });
        }




        #region Helpers


        private IEnumerator LoadUsers(string[] ids, LeaderboardPlayerModel[] players, Action<LeaderboardPlayerModel[]> result)
        {
            bool loaded = false;
            bool error = false;
            IUserProfile[] profiles=null;
            platform.LoadUsers(ids, (_profiles) =>
            {
                if (_profiles != null || _profiles.Length == 0)
                {
                    for (int a = 0; a < _profiles.Length; ++a)
                    {
                        IUserProfile profile = _profiles[a];
                        players[a].name = profile.userName;
                        players[a].avatar = profile.image;

                        Debug.LogFormat("User {0}. Name: {1}; Score: {2}; Id: {3}; Avatar: {4}",
                           a, players[a].name, players[a].score, ids[a], players[a].avatar == null);
                    }
                    profiles = _profiles;
                    loaded = true;
                }
                else
                {
                    Debug.LogWarning("Leaderboard: LoadUsers returned null or no profiles.");
                    loaded = true;
                    error = true;
                    result.Invoke(null);
                }
            });

            //Wait to load Users 
            while (!loaded)
                yield return new WaitForSeconds(0.3f);

            if (!error)
            {
                Debug.Log("Leaderboard: Waiting to load avatars started.");
                //Wait to load avatars
                float secondsOfTrying = 15;
                float secondsPerAttempt = 0.3f;
                loaded = false;
                while (secondsOfTrying > 0)
                {
                    int nrAvatarsLoaded = 0;
                    int index = 0;
                    bool allLoaded = true;
                    foreach(var plr in profiles)
                    {
                        if (plr.image == null)
                        {
                            allLoaded = false;
                        }
                        else
                        {
                            players[index].avatar = plr.image;
                            nrAvatarsLoaded++;
                        }
                        ++index;
                    }

                    Debug.LogFormat("Leaderboard: Loaded {0}/{1} avatars. Time remained: {2}",
                        nrAvatarsLoaded, players.Length, secondsOfTrying);

                    if (allLoaded)
                    {
                        loaded = true;
                        break;
                    }

                    secondsOfTrying -= secondsPerAttempt;
                    yield return new WaitForSeconds(secondsPerAttempt);
                }

                Debug.Log("Leaderboard: Waiting to load avatars finished.");
                result.Invoke(loaded ? players : null);
            }

            yield return null;
            
        }

        #endregion

    }
}
