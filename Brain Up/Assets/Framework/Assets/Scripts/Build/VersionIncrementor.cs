/*
 
 Author: Francesco Forno (Fornetto Games)
 Inspired by http://forum.unity3d.com/threads/automatic-version-increment-script.144917/
 
 Edited: Ghercioglo Roman (Romeon0)
 */

/*
Author: Ghercioglo Roman (Romeon0)
*/



using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
    using UnityEditor.Build;
    using UnityEditor.Build.Reporting;
    using UnityEditor.Callbacks;
#endif

namespace Assets.Scripts.Framework.Build
{
#if (UNITY_EDITOR)
    [InitializeOnLoad]
    public class VersionIncrementor : IPreprocessBuildWithReport, IPostprocessBuildWithReport
#else
    public class VersionIncrementor
#endif
    {

        //public static string LoadVersion()
        //{
        //    string version;
        //    string filePath = Application.persistentDataPath + "/BuildVersion";

        //    if (!File.Exists(filePath))
        //        return "v9.9.9(9999)";

        //    FileStream reader = File.Open(filePath, FileMode.Open);
        //    byte[] versionBytes = new byte[300];
        //    int count = reader.Read(versionBytes, 0, versionBytes.Length);
        //    reader.Close();

        //    Array.Resize(ref versionBytes, count);
        //    version = Encoding.ASCII.GetString(versionBytes);

        //    return version;
        //}


#if (UNITY_EDITOR)
        public int callbackOrder => 0;


        //public static void WriteVersion()
        //{

        //    string version = string.Format("v{0}({1})", PlayerSettings.bundleVersion, PlayerSettings.Android.bundleVersionCode);
        //    string filePath = Application.persistentDataPath + "/BuildVersion";

        //    FileStream writer = File.Open(filePath, FileMode.OpenOrCreate);
        //    byte[] versionBytes = Encoding.ASCII.GetBytes(version);
        //    writer.Write(versionBytes, 0, versionBytes.Length);
        //    writer.Close();
        //}

        public void OnPreprocessBuild(BuildReport report)
        {
            string version = IncreaseBuild();
            BuildVersion buildVersion = (BuildVersion)Resources.Load("BuildVersion");
            buildVersion.version = "v" + version;
            Debug.LogFormat("BeforeBuild. Version: {0}", PlayerSettings.bundleVersion);
        }

        public void OnPostprocessBuild(BuildReport report)
        {
            BuildVersion buildVersion = (BuildVersion)Resources.Load("BuildVersion");
            buildVersion.version = "v" + PlayerSettings.bundleVersion;
            Debug.LogFormat("AfterBuild 1. Version: {0}", PlayerSettings.bundleVersion);
        }

        [PostProcessBuild(-100)]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            BuildVersion buildVersion = (BuildVersion)Resources.Load("BuildVersion");
            buildVersion.version = "v" + PlayerSettings.bundleVersion;
            Debug.LogFormat("AfterBuild 2. Version: {0}", PlayerSettings.bundleVersion);
        }

        static string IncrementVersion(int majorIncr, int minorIncr, int buildIncr)
        {
            string[] lines = PlayerSettings.bundleVersion.Split('.');

            int majorVersion = int.Parse(lines[0]) + majorIncr;
            int minorVersion = int.Parse(lines[1]) + minorIncr;
            int build = int.Parse(lines[2]) + buildIncr;

            PlayerSettings.bundleVersion = majorVersion.ToString("0") + "." +
                                           minorVersion.ToString("0") + "." +
                                           build.ToString("0");
            PlayerSettings.Android.bundleVersionCode = majorVersion * 10000 + minorVersion * 1000 + build;

            return PlayerSettings.bundleVersion;
        }

        [MenuItem("Build/Increase Minor Version")]
        private static string IncreaseMinor()
        {
            return IncrementVersion(0, 1, 0);
        }

        [MenuItem("Build/Increase Major Version")]
        private static string IncreaseMajor()
        {
            return IncrementVersion(1, 0, 0);
        }

        private static string IncreaseBuild()
        {
            return IncrementVersion(0, 0, 1);
        }


#endif

    }
}

