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
    class Test_2 : MonoBehaviour
    {
        private void Start()
        {
            //string filePath = @"F:\Projects\Unity\__BlackArtSoft_Projects__\BrainUp\Brain Up\Assets\Resources\GameData\general-4.xml";
            //FixXml(filePath);
            //ReadXML(filePath.Replace(".xml", "_fixed.xml"));

            string root = @"F:\Projects\Unity\__BlackArtSoft_Projects__\BrainUp\Brain Up\Assets\Resources\GameData\";
            XMLToScriptableObject(root + "general.xml", root + "tmp_file.txt");
            XMLToScriptableObject(root + "general-2.xml", root + "tmp_file.txt");
            XMLToScriptableObject(root + "general-3.xml", root + "tmp_file.txt");
            XMLToScriptableObject(root + "general-4.xml", root + "tmp_file.txt");
        }

        private void XMLToScriptableObject(string filePath, string outFilePath)
        {
            StringBuilder builder = new StringBuilder();
            // Start with XmlReader object  
            //here, we try to setup Stream between the XML file nad xmlReader  
            using (XmlReader reader = XmlReader.Create(filePath))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        /*

                           - question: Answer 1. Hello?
                                answers:
                                - Hey
                                - Aloha
                                - Hello!
                                - Privet!

                         */
                        //return only when you have START tag  
                        switch (reader.Name.ToString())
                        {
                            case "question":
                                //Debug.Log("Question: " + reader.GetAttribute(1));
                                builder.Append("  - question: " + reader.GetAttribute(1)+"\n");
                                builder.Append("    answers: "+"\n");
                                break;
                            case "answer":
                                //Debug.Log("- answer: " + reader.GetAttribute(1));
                                builder.Append("    - " + reader.GetAttribute(1) + "\n");
                                break;
                        }
                    }
                }
            }

            File.AppendAllText(outFilePath, builder.ToString());
        }

        private void FixXml(string filePath)
        {

            string text = File.ReadAllText(filePath);// "<Node a=\"a\"[\"\" b=\"b\"[\"\"/>   <Node2 a=\"a\"[\"\" b=\"b\"[\"\"/>";
            string regEx = "(\\s+[\\w:.-]+\\s*=\\s*\")(([^\"]*)((\")((?!\\s+[\\w:.-]+\\s*=\\s*\"|\\s*(?:/?|\\?)>))[^\"]*)*)\"";
            StringBuilder sb = new StringBuilder();

            int currentPos = 0;
            foreach (Match match in Regex.Matches(text, regEx))
            {
                sb.Append(text.Substring(currentPos, match.Index - currentPos));
                string f = match.Result(match.Groups[1].Value + match.Groups[2].Value.Replace("\"", "&quot;")) + "\"";
                sb.Append(f);
                currentPos = match.Index + match.Length;
            }

            sb.Append(text.Substring(currentPos));

            Console.Write(sb.ToString());

            string outFilePath = filePath.Replace(".xml", "_fixed.xml");
            File.WriteAllText(outFilePath, sb.ToString());
        }

        private void ReadXML(string filePath)
        {
            // Start with XmlReader object  
            //here, we try to setup Stream between the XML file nad xmlReader  
            using (XmlReader reader = XmlReader.Create(filePath))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        //return only when you have START tag  
                        switch (reader.Name.ToString())
                        {
                            case "question":
                                //Console.WriteLine("Name of the Element is : " + reader.ReadString());
                                Debug.Log("Question: " + reader.GetAttribute(1));
                                break;
                            case "answer":
                                //Console.WriteLine("Your Location is : " + reader.ReadString());
                                Debug.Log("- answer: " + reader.GetAttribute(1));
                                break;
                        }
                    }
                    Console.WriteLine("");
                }
            }
        }


    }
}
