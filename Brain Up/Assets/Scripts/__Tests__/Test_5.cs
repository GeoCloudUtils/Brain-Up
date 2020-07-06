using Assets.Scripts.Games.Gamedata.TripleValueList;
using Assets.Scripts.Games.GameData.MultipleAnswersQuestion;
using Assets.Scripts.Games.GameData.Question_;
using Assets.Scripts.Games.GameData.SimpleWordsDictionary;
using System.IO;
using UnityEngine;
public class Test_5 : MonoBehaviour
{
    private void Start()
    {
        MultipleAnswersQuestion capitals = Resources.Load<MultipleAnswersQuestion>("GameData/Capitals");
        SimpleWordsDictionary countries = Resources.Load<SimpleWordsDictionary>("GameData/CountriesFlags");
        SimpleWordsDictionary continents = Resources.Load<SimpleWordsDictionary>("GameData/CountriesContinents");

        string root = @"F:\Projects\Unity\__BlackArtSoft_Projects__\BrainUp\Brain Up\Assets\Resources\GameData\";
        string outFilePath = root + @"\Raw\CountriesData.txt";

        string data = "";
        for (int a = 0; a < countries.words.Length; ++a)
        {
            string country = countries.words[a];
            string continent = continents.words[a];

            string capital = null;
            foreach (Question q in capitals.questions)
            {
                if (q.question == country)
                {
                    capital = q.answers[0];
                    break;
                }
            }
            if (capital != null)
            {
                data += string.Format("  - item1: {0}\n    item2: {1}\n    item3: {2}\n", country, capital, continent);
            }
            else
                Debug.LogError("Not found capital for: " + country);
        }

        File.WriteAllText(outFilePath, data);
    }
}
