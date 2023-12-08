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
        int colourDepth = int.Parse(options["colordepth"]);
        bool Fullscreen = bool.Parse(options["fullscreen"]);

        InvertMouse = bool.Parse(options["invert mouse y"]);

        if (Fullscreen)
        {
            Graphics3D(screenWidth, screenHeight, colourDepth);
        }
        else
        {
            Graphics3D(screenWidth, screenHeight, colourDepth, 2);
        }

        //App Title set in player settings

        QualitySettings.antiAliasing = 2;
        Cursor.visible = false;

        //Back buffer maybe unnecessary
    }

    public static int GetINIInt(string filePath, string section, string key)
    {
        // Read the INI file into a dictionary
        Dictionary<string, string> options = ReadIniFile(filePath);

        // Check if the key exists in the dictionary
        if (options.ContainsKey(key))
        {
            // Try to parse the value as an integer
            if (int.TryParse(options[key], out int value))
            {
                return value;
            }
            else
            {
                throw new FormatException($"Value for key '{key}' in section '{section}' is not a valid integer.");
            }
        }
        else
        {
            throw new KeyNotFoundException($"Key '{key}' not found in section '{section}'.");
        }
    }

    void Graphics3D(int screenWidth, int screenHeight, int colourDepth = 32, int FullscreenMode = 0)
    {
        switch (FullscreenMode)
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
