using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Game : MonoBehaviour
{
    static bool InvertMouse;

    void Awake()
    {
        Dictionary<string, string> options = ReadIniFile("options.ini");

        int screenWidth = int.Parse(options["width"]);
        int screenHeight = int.Parse(options["height"]);
        int colorDepth = int.Parse(options["colordepth"]);
        bool fullscreen = bool.Parse(options["fullscreen"]);

        InvertMouse = bool.Parse(options["invert mouse y"]);

        if (fullscreen == true)
        {
            Graphics3D(screenWidth, screenHeight, colorDepth);
        }
        else
        {
            Graphics3D(screenWidth, screenHeight, colorDepth, 2);
        }
    }

    void Graphics3D(int screenWidth, int screenHeight, int colorDepth = 32, int mode = 0)
    {
        switch (mode)
        {
            case 0:
                Screen.SetResolution(screenWidth, screenHeight, FullScreenMode.ExclusiveFullScreen);
                break;
            case 2:
                Screen.SetResolution(screenWidth, screenHeight, FullScreenMode.Windowed);
                break;
            default:
                Screen.SetResolution(screenWidth, screenHeight, FullScreenMode.ExclusiveFullScreen);
                break;
        }
    }

    void Update()
    {

    }

    static Dictionary<string, string> ReadIniFile(string path)
    {
        Dictionary<string, string> options = new Dictionary<string, string>();
        string currentSection = "";

        foreach (var line in File.ReadAllLines(path))
        {
            string trimmedLine = line.Trim();

            // Skip comments and empty lines
            if (trimmedLine.StartsWith(";") || trimmedLine == String.Empty)
                continue;

            // Check if the line is a section header
            if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
            {
                currentSection = trimmedLine.Substring(1, trimmedLine.Length - 2).ToLower();
                continue;
            }

            string[] parts = trimmedLine.Split('=');
            if (parts.Length == 2)
            {
                string key = parts[0].Trim().ToLower();
                string value = parts[1].Trim().ToLower();
                if (currentSection == "options")
                {
                    options[key] = value;
                }
            }
        }

        return options;
    }
}
