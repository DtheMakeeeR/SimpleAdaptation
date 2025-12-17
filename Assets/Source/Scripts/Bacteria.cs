using System.Collections;
using System.Linq;
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
    public static float BornTime = 1f;
    public static float BacteriaCoeff = 0.25f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Vector2 vector = direction.normalized * speed * Time.deltaTime;
        transform.position = transform.position + new Vector3(vector.x, vector.y, 0);
    }
    private void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.05f);
        bool hasWorldObjects = colliders.Any(c => c.tag == "World");
        if (!hasWorldObjects)
        {
            GameManager.BacteriaPool.Release(this);
        }
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
        Vector2 tmpv = MyGenerator.GenerateDirection(direction, BacteriaCoeff);
        child.direction = tmpv;
    }


    private void OnEnable()
    {
        StartCoroutine(BornCoroutine());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
