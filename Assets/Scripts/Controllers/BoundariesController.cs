using UnityEngine;
using System.Collections;

public static class BoundariesController
{
    private static Vector3 _screenSize;
    private static float _screenWidth;
    private static float _screenHeight;

    public static float ScreenWidth
    {
        get
        {
            return _screenWidth;
        }
    }

    public static float ScreenHeight
    {
        get
        {
            return _screenHeight;
        }
    }

    static BoundariesController()
    {
        _screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
        _screenWidth = _screenSize.x;
        _screenHeight = _screenSize.y;
    }
}
