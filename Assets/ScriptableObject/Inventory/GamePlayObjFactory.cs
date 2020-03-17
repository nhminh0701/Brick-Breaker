using UnityEngine;

[CreateAssetMenu(fileName = "DefaultFactory",menuName = "Factory/Default")]
public class GamePlayObjFactory : ScriptableObject
{
    [SerializeField] protected GameObject prefab;
    [SerializeField] protected Material material;

    public ObjectData objectData;

    public virtual GameObject CreateObject(Transform transform)
    {
        GameObject objectToReturn = Instantiate(prefab, transform);
        objectToReturn.GetComponent<Renderer>().material = material;

        return objectToReturn;
    }

    public virtual GameObject CreateObject(Vector3 position)
    {
        GameObject objectToReturn = Instantiate(prefab, position, Quaternion.identity);
        objectToReturn.GetComponent<Renderer>().material = material;

        return objectToReturn;
    }
}
