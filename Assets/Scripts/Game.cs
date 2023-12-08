using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Game : MonoBehaviour
{
    static bool InvertMouse;
    static Font font1;
    static Font font;

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

        font1 = LoadFont("GFX/Courier.ttf", 18);
        font = LoadFont("GFX/pretext.ttf", 128);

    }

    /// <summary>
    /// Sets the screen resolution and full screen mode for 3D graphics.
    /// </summary>
    /// <param name="screenWidth">The width of the screen resolution.</param>
    /// <param name="screenHeight">The height of the screen resolution.</param>
    /// <param name="colourDepth">The colour depth of the graphics (default is 32).</param>
    /// <param name="FullscreenMode">The full screen mode (0 for exclusive full screen, 2 for windowed).</param>
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

    /// <summary>
    /// Loads a font asset from the Resources folder.
    /// </summary>
    /// <param name="fontName">The name of the font asset to load.</param>
    /// <param name="height">The height of the font in pixels. Default is 12.</param>
    /// <param name="bold">Specifies whether the font should be bold. Default is false.</param>
    /// <param name="italic">Specifies whether the font should be italic. Default is false.</param>
    /// <param name="underlined">Specifies whether the font should be underlined. Default is false.</param>
    /// <returns>The loaded font asset, or null if the font is not found.</returns>
    public static Font LoadFont(string fontName, int height = 12, bool bold = false, bool italic = false, bool underlined = false)
    {
        // Load the font from the Resources folder
        Font font = Resources.Load<Font>(fontName);

        if (font == null)
        {
            Debug.LogError("Font not found: " + fontName);
            return null;
        }

        // Unity does not natively support changing a font to bold/italic/underlined dynamically.
        // Usually, you would have a separate font asset for bold/italic versions.
        // You can add additional logic here to load those versions if needed.

        return font;
    }

    void Update()
    {

    }

    /// <summary>
    /// Reads an INI file and returns a dictionary containing the options.
    /// </summary>
    /// <param name="path">The path to the INI file.</param>
    /// <returns>A dictionary containing the options from the INI file.</returns>
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
