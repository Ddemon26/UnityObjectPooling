# Object Pooling System

This system consists of two main components: `ObjectPool` and `ObjectPoolCreator`. These scripts work together to efficiently manage a pool of reusable objects in Unity, reducing the overhead of instantiating and destroying objects frequently.

## ObjectPool

The `ObjectPool` class is responsible for managing a pool of objects of a specific type. It allows you to get an object from the pool and return it when it's no longer needed.

### Features

- **Automatic Pool Expansion:** The pool can automatically expand by adding a new object when there's only one object left in the pool.
- **Object Activation/Deactivation:** Objects are automatically activated when retrieved from the pool and deactivated when returned.

### Usage

Create an instance of `ObjectPool` by passing a prefab and an initial pool size. Use `GetAndActivateObject` to retrieve an object and `ReturnObject` to return it to the pool.

## ObjectPoolCreator

The `ObjectPoolCreator` class provides an abstract base for creating and managing multiple object pools. It includes the concrete implementation `ObjectPoolCreator`.

### Features

- **Multiple Pools Management:** Manages pools for different prefabs.
- **Pool Initialization:** Initialize pools with a specified size for each prefab.
- **Object Retrieval and Return:** Get objects from pools and return them after use.

### Usage

Create an instance of `ObjectPoolCreator` and use `InitializePool` to create a pool for a prefab. Use `GetPooledObject` to get an object from a pool and `ReturnObjectToPool` to return it.

## Example

```csharp
public class ObjectPoolingExample : MonoBehaviour
{
    public GameObject prefab;
    private ObjectPoolCreator poolCreator;

    void Start()
    {
        poolCreator = new ObjectPoolCreator();
        poolCreator.InitializePool(prefab, 10);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject obj = poolCreator.GetPooledObject(prefab);
            // Use the object...
            poolCreator.ReturnObjectToPool(prefab, obj);
        }
    }
}
```
