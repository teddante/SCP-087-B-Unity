using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameTest
{
    // A Test behaves as an ordinary method
    [Test]
    public void GameTestSimplePasses()
    {
        // Use the Assert class to test conditions
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator GameTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
    
    [Test]
    public void LoadFont_ValidFontName_ReturnsFont()
    {
        // Arrange
        
        Game game = new Game();
        string fontName = "Arial";

        // Act
        Font font = game.LoadFont(fontName);

        // Assert
        Assert.IsNotNull(font);
        Assert.AreEqual(fontName, font.name);
    }

    [Test]
    public void LoadFont_InvalidFontName_ReturnsNull()
    {
        // Arrange
        Game game = new Game();
        string fontName = "NonExistentFont";

        // Act
        Font font = game.LoadFont(fontName);

        // Assert
        Assert.IsNull(font);
    }
}