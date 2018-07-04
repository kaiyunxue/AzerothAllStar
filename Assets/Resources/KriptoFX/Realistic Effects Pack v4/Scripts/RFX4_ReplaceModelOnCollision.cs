using UnityEngine;
using System.Collections;

public class RFX4_ReplaceModelOnCollision : MonoBehaviour
{

    public GameObject PhysicsObjects;

    private bool isCollided = false;
    Transform t;

    private void OnCollisionEnter(Collision collision)
    {
        if (!isCollided)
        {
            isCollided = true;
            PhysicsObjects.SetActive(true);
            var mesh = GetComponent<MeshRenderer>();
            if (mesh != null)
                mesh.enabled = false;
            var rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }
    }

    void OnEnable()
    {
        isCollided = false;
        PhysicsObjects.SetActive(false);
        var mesh = GetComponent<MeshRenderer>();
        if (mesh!=null)
            mesh.enabled = true;
        var rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.detectCollisions = true;
    }
}
