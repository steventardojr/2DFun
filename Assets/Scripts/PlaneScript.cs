using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour {

	private float gridSize = 10.0f;
	private Vector3 playerGridPosition = new Vector3(0,0,0) ;
    private Vector3 currentPlayerPosition;
	private GameObject[] planes;
	public Rigidbody player;

	void Awake() {
        
		currentPlayerPosition = player.transform.position;
        CreatePlanarGrid(playerGridPosition);


    }


    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		currentPlayerPosition = player.transform.position;
        if (GetGridPosition(currentPlayerPosition, gridSize) != playerGridPosition)
        {
            DestroyPlanarGrid();
            playerGridPosition = GetGridPosition(currentPlayerPosition, gridSize);
            CreatePlanarGrid(playerGridPosition);
        }


        

        //if


    }

	public Mesh CreateMesh(float x, float y, float z, float size)
	{
		Mesh m = new Mesh();
		m.name = "ScriptedMesh";
        
		m.vertices = new Vector3[] {
			new Vector3((size / 2) + x * size, y, (size / 2) + z * size),
			new Vector3((size / 2) + x * size, y, (size / 2) + (z - 1) * size),
            new Vector3((size / 2) + (x - 1) * size, y, (size / 2) + (z - 1) * size),
            new Vector3((size / 2) + (x - 1) * size, y, (size / 2) + z * size)
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

	public GameObject CreatePlane (float x, float y, float z, float size)
	{
		GameObject plane = new GameObject("Plane");

		Mesh mesh = CreateMesh(x, y, z, size);
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

    public Vector3 GetGridPosition (Vector3 position, float gridSize)
    {
        Vector3 gridPosition = new Vector3(Mathf.Round(position.x / gridSize), 0.0f, Mathf.Round(position.z / gridSize));
        

        return gridPosition;
    }

    public void CreatePlanarGrid (Vector3 center)
    {
        planes = new GameObject[9];
        planes[0] = CreatePlane(center.x, 0.0f, center.z, gridSize);
        planes[1] = CreatePlane(center.x + 1.0f, 0.0f, center.z + 1.0f, gridSize);
        planes[2] = CreatePlane(center.x, 0.0f, center.z + 1.0f, gridSize);
        planes[3] = CreatePlane(center.x + 1.0f, 0.0f, center.z, gridSize);
        planes[4] = CreatePlane(center.x - 1.0f, 0.0f, center.z - 1.0f, gridSize);
        planes[5] = CreatePlane(center.x, 0.0f, center.z - 1.0f, gridSize);
        planes[6] = CreatePlane(center.x - 1.0f, 0.0f, center.z, gridSize);
        planes[7] = CreatePlane(center.x - 1.0f, 0.0f, center.z + 1.0f, gridSize);
        planes[8] = CreatePlane(center.x + 1.0f, 0.0f, center.z - 1.0f, gridSize);
    }

    public void DestroyPlanarGrid ()
    {
        for (int i = 0; i < planes.Length; i++)
        {
            GameObject.Destroy(planes[i]);
        }
    }

}
