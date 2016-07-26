using UnityEngine;
using System.Collections;

public static class GameParameters
{
    public const string BULLET_LAYER = "Bullet";
    public const string ENEMY_LAYER = "Enemy";
    public const string PLAYER_LAYER = "Player";

    public static Color32 START_PLAYER_COLOR = new Color32(236, 240, 241, 255);
    public static Color32 END_PLAYER_COLOR = new Color32(52, 152, 219, 255);

    public static Vector3 MIN_MOUTH_SCALE = new Vector3(0.2f, 0.2f, 1f);
    public static Vector3 MAX_MOUTH_SCALE = new Vector3(0.4f, 0.1f, 1f);

    public const float MIN_BULLET_SPEED = 0.3f;
    public const float MAX_BULLET_SPEED = 5f;

    public const float MIN_MUSIC_PITCH = 0.75f;
    public const float MAX_MUSIC_PITCH = 1f;
}
