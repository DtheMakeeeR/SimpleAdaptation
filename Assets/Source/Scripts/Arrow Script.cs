using Unity.Mathematics;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Rotate(Vector3 view)
    {
        Vector3 point = transform.position + view;
        gameObject.transform.rotation.SetLookRotation(point);
    }
}
