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
            JSONToScriptableObject(root + @"\Raw\history-easy.json", root + "history-easy.txt");
            JSONToScriptableObject(root + @"\Raw\history-medium.json", root + "history-medium.txt");
            JSONToScriptableObject(root + @"\Raw\history-hard.json", root + "history-hard.txt");
        }

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

    }
}
