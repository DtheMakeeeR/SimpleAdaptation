using System.Collections;
using UnityEngine;

public class Bacteria : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private Vector2 direction;
    [SerializeField]
    private Color color;
    private SpriteRenderer spriteRenderer;
    public static float BornTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vector = direction.normalized * speed * Time.deltaTime;
        transform.position = transform.position + new Vector3(vector.x, vector.y, 0);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("TriggerExit");
        if (collision.gameObject.tag == "World")
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator BornCoroutine()
    {
        yield return new WaitForSeconds(BornTime);
        BornChild();
        yield return BornCoroutine();
    }

    private void BornChild()
    {
        Bacteria child = GameManager.BacteriaPool.Get();
        child.speed = speed;
        child.direction = direction;
    }
}
