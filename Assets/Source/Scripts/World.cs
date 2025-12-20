using System.Collections;
using UnityEngine;
using TMPro;
public class World : MonoBehaviour
{
    [SerializeField]
    private ArrowScript arrow;
    [SerializeField]
    public float speed;
    [SerializeField]
    public Vector2 direction = new Vector2(0, 1);
    [SerializeField]
    public float ChangeRate;
    [SerializeField]
    public float WorldCoefficent;
    [SerializeField]
    private TMP_Text TextDirection;
    [SerializeField]
    private TMP_Text TextSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Coroutine coroutine;
    void Start()
    {
        TextDirection.text = direction.ToString();
        TextSpeed.text = speed.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vector = direction.normalized * speed * Time.deltaTime;
        transform.position = transform.position + new Vector3(vector.x, vector.y, 0);
        if(coroutine == null) coroutine = StartCoroutine(ChangeCoroutine());
    }

    IEnumerator ChangeCoroutine()
    {
        Debug.Log("Change");
        yield return new WaitForSeconds(ChangeRate);
        Change();
        coroutine = null;
    }
    private void Change()
    {
        speed = MyGenerator.GenerateSpeed(speed, WorldCoefficent);
        direction = MyGenerator.GenerateDirection(direction, WorldCoefficent);

        arrow.Rotate(direction);

        TextDirection.text = direction.ToString();
        TextSpeed.text = speed.ToString();
    }
    public void MakeChange()
    {
        if(coroutine != null) StopCoroutine(coroutine);
        Change();
        coroutine = StartCoroutine(ChangeCoroutine());
    }
}
