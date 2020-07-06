using Assets.Scripts.Games;
using Assets.Scripts.Games.Gamedata.TripleValueList;
using Assets.Scripts.Games.GameData.MultipleAnswersQuestion;
using Assets.Scripts.Games.GameData.Question_;
using Assets.Scripts.Games.GameData.SimpleWordsDictionary;
using Assets.Scripts.Screens;
using UnityEngine;
public class UnitTest_Games : MonoBehaviour
{
    private void Start()
    {
        int[] games = (int[])typeof(GameId).GetEnumValues();
        int[] diffs = (int[])typeof(GameDifficulty).GetEnumValues();
        Debug.LogWarningFormat("------ Games test started");
        foreach (GameId game in games)
        {
            ControllerGlobal.Instance.currProgress = 0;
            foreach (GameDifficulty diff in diffs)
            {
                Debug.LogWarningFormat("Testing game '{0}' with difficulty {1}", game, diff);
                ControllerGlobal.Instance.SetGameDifficulty(diff);
                ControllerGlobal.Instance.StartGame(game);

                bool isNotValid = false;
                int level = 0;
                int attempts = 3000;
                while (--attempts > 0)
                {
                    bool canAdvance = false;
                    try
                    {
                        canAdvance = ControllerGlobal.Instance.Advance();
                    }
                    catch (UnityException)
                    {
                        isNotValid = true;
                        break;
                    }
                    if (canAdvance)
                        Debug.LogWarningFormat("Game Level: '{0}'", level++);
                    else break;
                }

                if (isNotValid)
                {
                    Debug.LogWarningFormat("Advancing not valid. Skipping game.");
                    break;
                }

                if(attempts==0)
                    Debug.LogError("Game Level finished by attempts!");
            }
        }

        Debug.LogWarningFormat("------ Games test finished!!!");
    }
}
