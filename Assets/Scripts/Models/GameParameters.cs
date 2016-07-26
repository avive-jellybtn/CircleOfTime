using UnityEngine;
using System.Collections;

public static class GameParameters
{
    public const string BULLET_LAYER = "Bullet";
    public const string ENEMYBULLET_LAYER = "EnemyBullet";
    public const string ENEMY_LAYER = "Enemy";
    public const string PLAYER_LAYER = "Player";
    public const string POWERUP_LAYER = "Powerup";

    public static Color32 START_PLAYER_COLOR = new Color32(236, 240, 241, 255);
    public static Color32 END_PLAYER_COLOR = new Color32(52, 152, 219, 255);
    public static Color32 ENEMY_OUTLINE_HIT_COLOR = new Color32(231, 76, 60, 255);

    public static Vector3 MIN_MOUTH_SCALE = new Vector3(0.2f, 0.2f, 1f);
    public static Vector3 MAX_MOUTH_SCALE = new Vector3(0.4f, 0.1f, 1f);

    public const float MIN_BULLET_SPEED = 0.3f;
    public const float MAX_BULLET_SPEED = 5f;

    public const float PLAYER_ROTATE_SPEED = 150f;

    public const KeyCode UP_KEYCODE = KeyCode.UpArrow;
    public const KeyCode DOWN_KEYCODE = KeyCode.DownArrow;
    public const KeyCode LEFT_KEYCODE = KeyCode.LeftArrow;
    public const KeyCode RIGHT_KEYCODE = KeyCode.RightArrow;
    public const KeyCode SHOOT_KEYCODE = KeyCode.Space;
}
