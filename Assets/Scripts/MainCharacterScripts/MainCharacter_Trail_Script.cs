using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter_Trail_Script : MonoBehaviour
{
    public int trailResolution;
    LineRenderer lineRenderer;

    Vector3[] lineSegmentPositions;
    Vector3[] lineSegmentVelocities;

    // This would be the distance between the individual points of the line renderer
    public float offset;
    Vector3 facingDirection;
    

    // How far the points 'lag' behind each other in terms of position
    public float lagTime;

    public float zOffSet = 0.0f;



    // Use this for initialization
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = trailResolution;

        lineSegmentPositions = new Vector3[trailResolution];
        lineSegmentVelocities = new Vector3[trailResolution];

        facingDirection = -Vector3.right;

        // Initialize our positions
        for (int i = 0; i < lineSegmentPositions.Length; i++)
        {
            lineSegmentPositions[i] = new Vector3();
            lineSegmentVelocities[i] = new Vector3();

            if (i == 0)
            {
                lineSegmentPositions[i] = transform.position;
            }
            else
            {
                lineSegmentPositions[i] = transform.position + (facingDirection * (offset * i));
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        facingDirection = -Vector3.right;
        facingDirection -= new Vector3(0f, 0f, zOffSet);
        lineSegmentPositions[0] = transform.position;
        lineRenderer.SetPosition(0, lineSegmentPositions[0]);
        for (int i = 1; i < lineSegmentPositions.Length; i++)
        {
            
            // All others will follow the original with the offset that you set up
            lineSegmentPositions[i] = Vector3.SmoothDamp(lineSegmentPositions[i], lineSegmentPositions[i - 1] + (facingDirection * offset), ref lineSegmentVelocities[i], lagTime);
            // Once we're done calculating where our position should be, set the line segment to be in its proper place
            lineRenderer.SetPosition(i, lineSegmentPositions[i]);
        }
    }
}
