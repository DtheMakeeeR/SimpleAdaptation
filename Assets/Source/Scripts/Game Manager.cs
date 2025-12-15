using System;
using UnityEngine;
using UnityEngine.Pool;
public class GameManager : MonoBehaviour
{
    public static ObjectPool<Bacteria> BacteriaPool;
    [SerializeField]
    private Bacteria BacteriaPrefab;
    [SerializeField]
    private bool collectionCheck = true;
    [SerializeField]
    private int baseSize = 1000;
    [SerializeField]
    private int maxSize = 10000;
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
    
    Bacteria OnCreatePool()
    {
        Bacteria tmp = Instantiate(BacteriaPrefab, transform.position, Quaternion.identity);
        return tmp;
    }
    void OnGetPool(Bacteria obj)
    {
        obj.name = "getted bacteria";
        obj.gameObject.SetActive(true);
    }
    void OnReleasePool(Bacteria obj)
    {
        obj.name = "released bacteria";
        obj.gameObject.SetActive(false);
        obj.transform.position = transform.position;
        obj.transform.rotation = Quaternion.identity;
    }
    void OnDestroyPool(Bacteria obj)
    {
        obj.name = "destroyed bacteria";
        Destroy(obj.gameObject);
    }
}
