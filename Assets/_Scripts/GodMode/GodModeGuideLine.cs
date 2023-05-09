using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GodModeGuideLine : MonoBehaviour
{
    public Transform target;  
    public GameObject player;  

    private NavMeshPath path;
    private bool guideMode = false;
    private LineRenderer lineRenderer;

    void Start()
    {
        // Creates a new nav mesh path
        path = new NavMeshPath();
        lineRenderer = GetComponent<LineRenderer>();
        // Set up the LineRenderer
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); 
        lineRenderer.startColor = Color.blue;
        lineRenderer.endColor = Color.blue;
    }

    void Update()
    {
        // toggle guide mode
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            guideMode = !guideMode;  
        }
        // calculates best way to the middle of the maze
        if (guideMode)
        {
            NavMesh.CalculatePath(player.transform.position, target.position, NavMesh.AllAreas, path);
            DrawPath();
        }
        else
        {
            // Clear the line when guide mode is off
            lineRenderer.positionCount = 0;  
        }
    }

    void DrawPath()
    {
        if (path.corners.Length < 2) 
            return;

        // Set the positions of the LineRenderer to the corners of the path
        lineRenderer.positionCount = path.corners.Length;
        lineRenderer.SetPositions(path.corners);  
    }
}
