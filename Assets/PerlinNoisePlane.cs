﻿     using UnityEngine;
    using System.Collections;
    public class PerlinNoisePlane : MonoBehaviour {
    public float power = 3.0f;
    public float scale = 1.0f;
    public float speed = 1.0f;
    private Vector2 v2SampleStart = new Vector2(0f, 0f);
    void Start () {
    MakeSomeNoise ();
    }
    void Update () {
		MakeSomeNoise();
	
  /*   if (Input.GetKeyDown (KeyCode.Space)) {
    v2SampleStart = new Vector2(Random.Range (0.0f, 100.0f), Random.Range (0.0f, 100.0f));
    MakeSomeNoise();
		}
    } */
	}
    void MakeSomeNoise() {
		MeshFilter mf = GetComponent<MeshFilter>();
		Vector3[] vertices = mf.mesh.vertices;
		for (int i = 0; i < vertices.Length; i++) {
			float xCoord = v2SampleStart.x + vertices[i].x * scale * Time.time * Time.deltaTime * speed;
			float yCoord = v2SampleStart.y + vertices[i].z * scale * Time.time* Time.deltaTime * speed;
			vertices[i].y = (Mathf.PerlinNoise (xCoord, yCoord) - 0.5f) * power;
		}
		mf.mesh.vertices = vertices;
		mf.mesh.RecalculateBounds();
		mf.mesh.RecalculateNormals();
    }
}
