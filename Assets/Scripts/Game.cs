using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Game : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Dictionary<string, string> options = ReadIniFile("options.ini");

        // Extracting the values from the dictionary and converting them to the appropriate types.
        int screenWidth = int.Parse(options["width"]);
        int screenHeight = int.Parse(options["height"]);
        int colorDepth = int.Parse(options["colordepth"]);
        bool fullscreen = bool.Parse(options["fullscreen"]);
    }

    // Update is called once per frame
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
