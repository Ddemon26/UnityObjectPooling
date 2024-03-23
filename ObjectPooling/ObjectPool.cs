public class ObjectPool
{
    private readonly Queue<GameObject> objectPool = new Queue<GameObject>();
    private readonly GameObject prefab;
    private readonly Transform parentContainer;

    public ObjectPool(GameObject prefab, int poolSize, Transform parentContainer)
    {
        this.prefab = prefab;
        this.parentContainer = parentContainer;

        for (int i = 0; i < poolSize; i++)
        {
            objectPool.Enqueue(CreatePooledObject());
        }
    }

    public GameObject GetAndActivateObject()
    {
        if (objectPool.Count == 0)
        {
            objectPool.Enqueue(CreatePooledObject());
        }
        GameObject obj = ActivateObject(objectPool.Dequeue());
        obj.transform.SetParent(null); // Optionally, unparent for activation if needed for positioning etc.
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.transform.SetParent(parentContainer);
        obj.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        obj.SetActive(false);
        objectPool.Enqueue(obj);
    }

    private GameObject CreatePooledObject()
    {
        GameObject obj = GameObject.Instantiate(prefab, parentContainer);
        obj.SetActive(false);
        return obj;
    }

    private GameObject ActivateObject(GameObject obj)
    {
        obj.SetActive(true);
        return obj;
    }
}