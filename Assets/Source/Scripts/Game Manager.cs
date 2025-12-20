using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using System;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static ObjectPool<Bacteria> BacteriaPool;
    [SerializeField]
    private Bacteria BacteriaPrefab;
    [SerializeField]
    private World WorldPrefab;
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
    [SerializeField]
    private TMP_Text TextWorldCoeff;
    [SerializeField]
    private TMP_Text TextBirthRate;
    [SerializeField]
    private TMP_Text TextChangeRate;
    //[SerializeField]
    //private TMP_InputField InputBirthRate;

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
        TextWorldCoeff.text = WorldPrefab.WorldCoefficent.ToString();
        TextBirthRate.text = Bacteria.BirthRate.ToString();
        TextChangeRate.text = WorldPrefab.ChangeRate.ToString();
        CreatePopulation(baseSize);
    }
    Bacteria OnCreatePool()
    {
        Bacteria tmp = Instantiate(BacteriaPrefab, transform.position, Quaternion.identity);
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
        aliveBacterias--;
        TextCount.text = aliveBacterias.ToString();
    }
    void OnDestroyPool(Bacteria obj)
    {
        obj.name = "destroyed bacteria";
        Destroy(obj.gameObject);
    }
    public void CreatePopulation(int count)
    {
        for(int i=0; i < count; i++)
        {
            Debug.Log($"I : {i}");
            Bacteria tmp = BacteriaPool.Get();
            tmp.speed = MyGenerator.GenerateSpeed(WorldPrefab.speed, Bacteria.BacteriaCoeff);
            tmp.direction = MyGenerator.GenerateDirection(WorldPrefab.direction, Bacteria.BacteriaCoeff);
            Vector3 dir = UnityEngine.Random.onUnitSphere * 0.25f;
            tmp.gameObject.transform.position = dir + WorldPrefab.transform.position;
        }
    }
    public void SetBacteriaCoeff(float value)
    {
        value = (float)Math.Round(value, 2);
        Bacteria.BacteriaCoeff = value;
        TextBactCoeff.text = Bacteria.BacteriaCoeff.ToString();
    }
    public void SetWorldCoeff(float value)
    {
        value = (float)Math.Round(value, 2);
        WorldPrefab.WorldCoefficent = value;
        TextWorldCoeff.text = WorldPrefab.WorldCoefficent.ToString();
    }
    public void SetBirthRate(string value)
    {
        float tmp = float.Parse(value);
        if (tmp < 0) return;
        Bacteria.BirthRate = tmp;
        TextBirthRate.text = Bacteria.BirthRate.ToString();
    }
    public void SetChangeRate(string value)
    {
        float tmp = float.Parse(value);
        if (tmp < 0) return;
        WorldPrefab.ChangeRate = tmp;
        TextChangeRate.text = WorldPrefab.ChangeRate.ToString();
    }
    
}
