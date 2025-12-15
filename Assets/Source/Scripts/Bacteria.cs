using System.Collections;
using UnityEngine;

public class Bacteria : MonoBehaviour
{
    [SerializeField]
    public float speed;
    [SerializeField]
    public Vector2 direction = new Vector2(0, 1);
    [SerializeField]
    private Color color;
    private SpriteRenderer spriteRenderer;
    public static float BornTime = 2.5f;
    public static float BacteriaCoeff = 0.25f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        StartCoroutine(BornCoroutine());
    }

    void Update()
    {
        Vector2 vector = direction.normalized * speed * Time.deltaTime;
        transform.position = transform.position + new Vector3(vector.x, vector.y, 0);
    }

    IEnumerator BornCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(BornTime);
            BornChild();
        }
    }
    private void BornChild()
    {
        Bacteria child = GameManager.BacteriaPool.Get();
        child.gameObject.transform.position = transform.position;
        child.gameObject.transform.rotation= transform.rotation;
        float tmpf = MyGenerator.GenerateSpeed(speed, BacteriaCoeff);
        child.speed = tmpf;
        Debug.Log($"Generated speed {tmpf}");
        Vector2 tmpv = MyGenerator.GenerateDirection(direction, BacteriaCoeff);
        Debug.Log($"Generated vector {tmpv}");
        child.direction = tmpv;
    }

    

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("TriggerExit");
        if (collision.gameObject.tag == "World")
        {
            gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
