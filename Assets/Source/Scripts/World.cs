using System.Collections;
using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector2 direction;
    [SerializeField]
    private float coolDown;
    public float WorldCoefficent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Coroutine coroutine;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vector = direction.normalized * speed * Time.deltaTime;
        transform.position = transform.position + new Vector3(vector.x, vector.y, 0);
        if(coroutine == null) coroutine = StartCoroutine(Change());
    }

    IEnumerator Change()
    {
        Debug.Log("Change");
        yield return new WaitForSeconds(coolDown);
        int speedSign = Random.value >= 0.5 ? 1 : -1;
        int rotSign = Random.value >= 0.5 ? 1 : -1;
        Debug.Log($"speedSign: {speedSign}");
        Debug.Log($"rotSign: {rotSign}");
        speed = speed + speed*WorldCoefficent*speedSign;
        direction = Random.onUnitSphere;
        coroutine = null;
    }
}
