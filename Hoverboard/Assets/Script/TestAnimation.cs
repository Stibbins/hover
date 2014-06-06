/*
 * Created by: Robbin
 * Modified by: 
 * 
 * Description:
 * 
 */

using UnityEngine;
using System.Collections;

public class TestAnimation : MonoBehaviour {

    float offset = 0;

	void Start () {
	
	}
	
	void Update () {
        offset+=0.01f;
        renderer.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
	}
}
