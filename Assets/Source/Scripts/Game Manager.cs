using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static ObjectPool<Bacteria> BacteriaPool;
    [SerializeField]
    private Bacteria BacteriaPrefab;
    [SerializeField]
    private World WorldPrefab;
    [SerializeField]
    private Vector2 BaseDirection = new Vector2(0, 1);
    [SerializeField]
    private float BaseSpeed = 3f;
    [SerializeField]
    private bool collectionCheck = true;
    [SerializeField]
    private int baseSize = 100;
    [SerializeField]
    private int maxSize = 10000;
    private int aliveBacterias = 0;
    [SerializeField]
    private TMP_Text TextCount;
    [SerializeField]
    private TMP_Text TextBactCoeff;

    private void Awake()
    {
        BacteriaPool = new ObjectPool<Bacteria>(
            OnCreatePool,
            OnGetPool,
            OnReleasePool,
            OnDestroyPool,
            collectionCheck,
            baseSize,
            maxSize);
    }
    private void Start()
    {
        TextBactCoeff.text = Bacteria.BacteriaCoeff.ToString();
        CreatePopulation();
    }
    Bacteria OnCreatePool()
    {
        Bacteria tmp = Instantiate(BacteriaPrefab, transform.position, Quaternion.identity);
        tmp.speed = MyGenerator.GenerateSpeed(BaseSpeed, Bacteria.BacteriaCoeff);
        tmp.direction = MyGenerator.GenerateDirection(BaseDirection, Bacteria.BacteriaCoeff);
        return tmp;
    }
    void OnGetPool(Bacteria obj)
    {
        obj.name = "getted bacteria";
        obj.gameObject.SetActive(true);
        aliveBacterias++;
        TextCount.text = aliveBacterias.ToString();
    }
    void OnReleasePool(Bacteria obj)
    {
        obj.name = "released bacteria";
        obj.gameObject.SetActive(false);
        obj.transform.position = transform.position;
        obj.transform.rotation = Quaternion.identity;
        Debug.Log($"Bacterias before decrement: {aliveBacterias}");
        aliveBacterias--;
        Debug.Log($"Bacterias after decrement: {aliveBacterias}");
        TextCount.text = aliveBacterias.ToString();
    }
    void OnDestroyPool(Bacteria obj)
    {
        obj.name = "destroyed bacteria";
        Destroy(obj.gameObject);
    }
    void CreatePopulation()
    {
        for(int i=0; i < baseSize; i++)
        {
            Debug.Log($"I : {i}");
            Bacteria tmp = BacteriaPool.Get();
            Vector3 dir = Random.onUnitSphere * 0.25f;
            tmp.gameObject.transform.position = dir + WorldPrefab.transform.position;
        }
    }
}
