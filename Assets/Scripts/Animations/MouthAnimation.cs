using UnityEngine;
using System.Collections;

public class MouthAnimation : MonoBehaviour {

	void Update () {
        float dist = Vector2.Distance(PlayerController.instance.GetPlayerPos(), transform.position);
        transform.localScale = Vector3.Lerp(GameParameters.MIN_MOUTH_SCALE, GameParameters.MAX_MOUTH_SCALE, dist/BoundariesController.ScreenHeight);
	}
}
