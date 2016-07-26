using UnityEngine;
using System.Collections;

public class ForceNoRotation : MonoBehaviour {

	void Update () {
        transform.rotation = Quaternion.identity;
	}
}
