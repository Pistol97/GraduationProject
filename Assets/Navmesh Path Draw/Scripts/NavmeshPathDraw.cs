﻿using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer))]

public class NavmeshPathDraw : MonoBehaviour
{
    public Transform[] destination;
    public bool recalculatePath = true;
    public float recalculationTime = 0.1f;

    NavMeshPath path;
    LineRenderer lr;

    float time = 0f;
    bool stopped = false;

    [SerializeField] public int currentPoints = 0;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.useWorldSpace = true;
        path = new NavMeshPath();

        if(lr.materials.Length == 0){
            lr.material = Resources.Load("material/path_horror", typeof(Material)) as Material;
        }

        Draw();
    }
    
    //draw the path
    public void Draw()
    {
        if (destination == null) return;
        stopped = false;
        
        RaycastHit downHit;
        Vector3 validatedDesPos;
        Vector3 validatedOriginPos;

        /*GET THE NAVMESH POSITION BELOW DESTINATION AND ORIGIN IN ORDER TO PRINT THE PATH*/
        //validate destination position
        if (Physics.Raycast(destination[currentPoints].position, -Vector3.up, out downHit, Mathf.Infinity)) {
            validatedDesPos = new Vector3(destination[currentPoints].position.x, downHit.transform.position.y, destination[currentPoints].position.z);
        }else{
            validatedDesPos = destination[currentPoints].position;
        }

        //validate origin position
        if (Physics.Raycast(transform.position, -Vector3.up, out downHit, Mathf.Infinity)) {
            validatedOriginPos = new Vector3(transform.position.x, downHit.transform.position.y, transform.position.z);
        }else{
            validatedOriginPos = transform.position;
        }

        NavMesh.CalculatePath(validatedOriginPos, validatedDesPos, NavMesh.AllAreas, path);
        Vector3[] corners = path.corners;
        
        lr.positionCount = corners.Length;
        lr.SetPositions(corners);

    }


    //stop drawing the path
    public void Stop()
    {
        stopped = true;
        lr.positionCount = 0;
    }

    //recalculate the route ONCE every frame if enabled
    void Update()
    {
        if (!recalculatePath) return;
        if (!stopped) time += Time.deltaTime;

        if (time >= recalculationTime && !stopped)
        {
            time = 0f;
            currentPoints = PlayerPrefs.GetInt("Gate");
            Draw();
        }
    }
}
