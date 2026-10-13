using UnityEngine;
using System.Collections.Generic;

public class ViewObstructor : MonoBehaviour
{
    [Header("Targets")]
    [SerializeField] private Transform player;
    [SerializeField] private Transform wallsParent;
    [SerializeField] private Transform objectsParent;

    [Header("Tunnel Dimensions")]
    [SerializeField] private float tunnelRadiusX = 2.0f;
    [SerializeField] private float tunnelRadiusY = 2.0f; 

    [Header("Fade Settings")]
    [SerializeField] private float fadeSpeed = 5f;
    [SerializeField] private float fullyInvisibleDistance = 1.5f; 

    [Header("Gizmo Settings")]
    [SerializeField] private Color gizmoColor = new Color(0f, 1f, 1f, 0.4f);

    private Camera cam;

    private struct TrackedElement
    {
        public Renderer renderer;
        public Material runtimeMaterial;
        public float originalAlpha;
    }

    private List<TrackedElement> walls = new List<TrackedElement>();
    private List<TrackedElement> objects = new List<TrackedElement>();

    void Start()
    {
        cam = Camera.main;

        if (wallsParent != null)
        {
            foreach (Renderer ren in wallsParent.GetComponentsInChildren<Renderer>())
            {
                TrackedElement element = new TrackedElement();
                element.renderer = ren;
                element.runtimeMaterial = ren.material;
                element.originalAlpha = element.runtimeMaterial.color.a;
                walls.Add(element);
            }
        }
        if (objectsParent != null)
        {
            foreach (Renderer ren in objectsParent.GetComponentsInChildren<Renderer>())
            {
                TrackedElement element = new TrackedElement();
                element.renderer = ren;
                element.runtimeMaterial = ren.material;
                element.originalAlpha = element.runtimeMaterial.color.a;
                objects.Add(element);
            }
        }
    }

    void Update()
    {
        if (player == null || cam == null) return;
        if (walls.Count == 0 && objects.Count == 0) return;

        // Convert player position to Camera's Local Space
        Vector3 localPlayerPos = cam.transform.InverseTransformPoint(player.position);

        // Loop through every wall
        foreach (TrackedElement wall in walls)
        {
            if (wall.renderer == null) continue;

            // Convert object position to Camera's Local Space
            Vector3 localObjPos = cam.transform.InverseTransformPoint(wall.renderer.transform.position);

            float targetAlpha = wall.originalAlpha;

            // Render wall invisible if closer to camera than player
            if (localObjPos.z < localPlayerPos.z) targetAlpha = 0f;

            // Smoothly fade the material color
            Color curColor = wall.runtimeMaterial.color;
            float newAlpha = Mathf.MoveTowards(curColor.a, targetAlpha, fadeSpeed * Time.deltaTime);
            wall.runtimeMaterial.color = new Color(curColor.r, curColor.g, curColor.b, newAlpha);

            if (newAlpha == 0f) wall.renderer.enabled = false;
            else wall.renderer.enabled = true;
        }

        // Perform the same operation on walls with more specificity to if they're close to the player or not
        foreach (TrackedElement obj in objects)
        {
            if (obj.renderer == null) continue;

            Vector3 localObjPos = cam.transform.InverseTransformPoint(obj.renderer.transform.position);
            float targetAlpha = obj.originalAlpha;

            // First check if object is closer to the camera than the player is
            if (localObjPos.z > 0 && localObjPos.z < localPlayerPos.z)
            {
                // Check if object is within the "view tunnel"
                float diffX = Mathf.Abs(localObjPos.x - localPlayerPos.x);
                float diffY = Mathf.Abs(localObjPos.y - localPlayerPos.y);

                // If they are in the view tunnel:
                if (diffX < tunnelRadiusX && diffY < tunnelRadiusY)
                {
                    // Calculate the distance and make object more transparent the closer the player is
                    float distanceToPlayer = Vector3.Distance(obj.renderer.transform.position, player.position);
                    targetAlpha = Mathf.Clamp01(distanceToPlayer / fullyInvisibleDistance);
                    targetAlpha *= obj.originalAlpha;
                }
            }

            Color curColor = obj.runtimeMaterial.color;
            float newAlpha = Mathf.MoveTowards(curColor.a, targetAlpha, fadeSpeed * Time.deltaTime);
            obj.runtimeMaterial.color = new Color(curColor.r, curColor.g, curColor.b, newAlpha);
        }

    }

    private void OnDrawGizmos()
    {
        // Cache camera in editor if it isn't assigned yet
        if (cam == null) cam = GetComponent<Camera>();
        if (cam == null) cam = Camera.main;

        if (cam == null || player == null) return;

        // Calculate the center path and length of the tunnel
        Vector3 playerPosLocal = cam.transform.InverseTransformPoint(player.position);

        // The tunnel length is the local Z distance to the player
        float tunnelLength = playerPosLocal.z;
        if (tunnelLength <= 0) return;

        // Set the Gizmo matrix to match the Camera's position and rotation
        // This lets us draw a standard Cube aligned perfectly with the camera's local space
        Matrix4x4 originalMatrix = Gizmos.matrix;
        Gizmos.matrix = cam.transform.localToWorldMatrix;

        // Define the bounding box center and size in local space
        // Center is halfway between camera (0,0,0) and the player's local Z depth
        Vector3 localCenter = new Vector3(playerPosLocal.x, playerPosLocal.y, tunnelLength / 2f);

        // Full width/height is 2x the radius fields
        Vector3 localSize = new Vector3(tunnelRadiusX * 2f, tunnelRadiusY * 2f, tunnelLength);

        // Draw the bounding wireframe
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireCube(localCenter, localSize);

        // Optional: Draw a solid transparent overlay on the front and back caps for depth
        Gizmos.color = new Color(gizmoColor.r, gizmoColor.g, gizmoColor.b, gizmoColor.a * 0.2f);
        Gizmos.DrawCube(localCenter, localSize);

        // Restore original matrix so we don't mess up other gizmos in the scene
        Gizmos.matrix = originalMatrix;
    }
}