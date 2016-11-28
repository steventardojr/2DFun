using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour {

	private float gridSize = 10.0f;
	private Vector3 gridPosition = new Vector3(0,0,0) ;
	private Vector3 currentPlayerPosition;
	private Plane[] planes;
	public Rigidbody player;

	void Awake() {
		currentPlayerPosition = player.transform.position;

		CreatePlane (gridSize * gridPosition.x + gridSize, 0.0f, gridSize * gridPosition.z + gridSize);
		//CreatePlane (gridSize*gridPosition.x, 0.0f, gridSize*gridPosition.z);
		//CreatePlane (gridSize*gridPosition.x, 0.0f, gridSize*gridPosition.z);
		//CreatePlane (gridSize*gridPosition.x, 0.0f, gridSize*gridPosition.z);
		//CreatePlane (gridSize*gridPosition.x, 0.0f, gridSize*gridPosition.z);
		//CreatePlane (gridSize*gridPosition.x, 0.0f, gridSize*gridPosition.z);
		//CreatePlane (gridSize*gridPosition.x, 0.0f, gridSize*gridPosition.z);
		//CreatePlane (gridSize*gridPosition.x, 0.0f, gridSize*gridPosition.z);
		//CreatePlane (gridSize*gridPosition.x, 0.0f, gridSize*gridPosition.z);

	}


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		currentPlayerPosition = player.transform.position;
		
	}

	public Mesh CreateMesh(float width, float height, float length)
	{
		Mesh m = new Mesh();
		m.name = "ScriptedMesh";
		m.vertices = new Vector3[] {
			new Vector3(-width, height, -length),
			new Vector3(-width, height, length),
			new Vector3(width, height, length),
			new Vector3(width, height, -length)
		};
		m.uv = new Vector2[] {
			new Vector2 (0, 0),
			new Vector2 (0, 1),
			new Vector2(1, 1),
			new Vector2 (1, 0)
		};
		m.triangles = new int[] { 0, 1, 2, 0, 2, 3};
		m.RecalculateNormals();

		return m;
	}

	public GameObject CreatePlane (float width, float height, float length)
	{
		GameObject plane = new GameObject("Plane");

		Mesh mesh = CreateMesh(width, height, length);
		MeshFilter meshFilter = (MeshFilter)plane.AddComponent(typeof(MeshFilter));
		MeshCollider meshCollider = (MeshCollider)plane.AddComponent (typeof(MeshCollider));
		MeshRenderer meshRenderer = plane.AddComponent(typeof(MeshRenderer)) as MeshRenderer;

		meshFilter.mesh = mesh;
		meshCollider.sharedMesh = mesh;
		meshRenderer.material.shader = Shader.Find ("Particles/Additive");

		Texture2D tex = new Texture2D(1, 1);
		tex.SetPixel(0, 0, Color.red);
		tex.Apply();

		meshRenderer.material.mainTexture = tex;
		meshRenderer.material.color = Color.red;
		return plane;
	}

}
