using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using UnityEngine;

namespace Assets.Scripts.__Tests__
{
    class Test_3 : MonoBehaviour
    {
        private void Start()
        {
            //string filePath = @"F:\Projects\Unity\__BlackArtSoft_Projects__\BrainUp\Brain Up\Assets\Resources\GameData\general-4.xml";
            //FixXml(filePath);
            //ReadXML(filePath.Replace(".xml", "_fixed.xml"));

            string root = @"F:\Projects\Unity\__BlackArtSoft_Projects__\BrainUp\Brain Up\Assets\Resources\GameData\";
            //JSONToScriptableObject(root + @"\Raw\history-easy.json", root + "history-easy.txt");
            //JSONToScriptableObject(root + @"\Raw\history-medium.json", root + "history-medium.txt");
            //JSONToScriptableObject(root + @"\Raw\history-hard.json", root + "history-hard.txt");

            JSONToScriptableObject_2(root + @"\Raw\Capitals.json", root + "Capitals.txt");
        }

        //History, General
        private void JSONToScriptableObject(string filePath, string outFilePath)
        {
            string text = File.ReadAllText(filePath);
            dynamic json = JsonConvert.DeserializeObject(text);

            StringBuilder builder = new StringBuilder();


            foreach (var result  in json.results)
            {
                string question = result.question;
                string correct = result.correct_answer;
                string[] wrong = result.incorrect_answers.ToObject<string[]>();

                List<string> list = wrong.ToList();
                list.Insert(0, correct);
                string[] all = list.ToArray();

                //Replace special chars
                question = question.Replace("&#039;", "'");
                question = question.Replace("&quot;", "\"");
                question = question.Replace(":", "?");
                question = question.Trim();
                for (int a=0; a < all.Length; ++a)
                {
                    all[a] = all[a].Replace("&#039;", "'");
                    all[a] = all[a].Replace("&quot;", "\"");
                    all[a] = question.Trim();
                }

                builder.Append("  - question: " + question + "\n");
                builder.Append("    answers: " + "\n");
                foreach(string answer in all)
                {
                    builder.Append("    - " + answer + "\n");
                }
            }


            File.WriteAllText(outFilePath, builder.ToString());
        }

        //Capitals
        private void JSONToScriptableObject_2(string filePath, string outFilePath)
        {
            string text = File.ReadAllText(filePath);
            dynamic json = JsonConvert.DeserializeObject(text);

            StringBuilder builder = new StringBuilder();

            int c = 0;
            foreach (var data in json)
            {
               // if (c++ == 40) break;

                string countryName = null;
                string capitalName = null;
                string continentName = null;
                JObject obj = data;
                foreach (JProperty p in obj.Properties())
                {
                  //  Debug.Log("PropName: |" + p.Name + "|");

                    if (p.Name == "CountryName")
                        countryName = p.Value.ToString();
                    else if (p.Name == "CapitalName")
                        capitalName = p.Value.ToString();
                    else if (p.Name == "ContinentName")
                        continentName = p.Value.ToString();
                }
                // Debug.LogFormat("Values: {0} {1} {2}", countryName, capitalName, continentName);

                if (countryName == "N/A" || capitalName == "N/A" || continentName == "N/A")
                {
                    Debug.LogFormat("Skipped: {0} {1} {2}", countryName, capitalName, continentName);
                    continue;
                }

                string question = countryName;
                string[] all = new string[] { capitalName, continentName };
               // Debug.LogFormat("Values 2: {0} {1} {2}", all[0], all[1], question);

                //Replace special chars
                //question = question.Replace("&#039;", "'");
                //question = question.Replace("&quot;", "\"");
                //question = question.Replace(":", "?");
                //question = question.Trim();
                //for (int a = 0; a < all.Length; ++a)
                //{
                //    all[a] = all[a].Replace("&#039;", "'");
                //    all[a] = all[a].Replace("&quot;", "\"");
                //    all[a] = question.Trim();
                //}

                builder.Append("  - question: " + question + "\n");
                builder.Append("    answers: " + "\n");
                foreach (string answer in all)
                {
                    builder.Append("    - " + answer + "\n");
                }
            }

           // Debug.Log(builder.ToString());

            File.WriteAllText(outFilePath, builder.ToString());
        }

    }
}
