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
public class Test_4 : MonoBehaviour
{
    private void Start()
    {
        //string filePath = @"F:\Projects\Unity\__BlackArtSoft_Projects__\BrainUp\Brain Up\Assets\Resources\GameData\general-4.xml";
        //FixXml(filePath);
        //ReadXML(filePath.Replace(".xml", "_fixed.xml"));

        string root = @"F:\Projects\Unity\__BlackArtSoft_Projects__\BrainUp\Brain Up\Assets\Resources\GameData\";
        XMLToScriptableObject(root + @"\Raw\WorldCities.xml", root + "WorldCities.txt");
    }

    private void XMLToScriptableObject(string filePath, string outFilePath)
    {
        Debug.Log("XMLToScriptableObject started!");
        StringBuilder builder = new StringBuilder();
        using (XmlReader reader = XmlReader.Create(filePath))
        {
            int counter = 0;
            int nrRow = 0; 
            while (reader.Read())
            {

                if (reader.HasValue)
                {
                   // Debug.LogFormat("Name: {0}; Value: {1}", reader.Name.ToString(), reader.Value);
                    if (counter == 1)
                    {
                        builder.Append("  - question: " + reader.Value + "\n");
                        builder.Append("    answers: " + "\n");
                    }
                    else if (counter == 13)
                    {
                        builder.Append("    - " + reader.Value + "\n");
                    }
                    ++counter;
                }

                
                switch (reader.Name.ToString())
                {
                    case "Row":
                      //  Debug.LogFormat("-----------");
                        counter = 0;
                        ++nrRow;
                        break;
                }

                //if (nrRow == 100) break;
            }
        }

        File.WriteAllText(outFilePath, builder.ToString());
        Debug.Log("XMLToScriptableObject end!");
    }
}
