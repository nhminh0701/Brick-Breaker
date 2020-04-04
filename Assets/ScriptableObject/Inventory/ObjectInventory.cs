using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Game Play Object/Inventory/ Inventory Object")]
public class ObjectInventory : ScriptableObject
{
    public List<GamePlayObjFactory> factories;

    public GamePlayObjFactory SelectingObject
    {
        get
        {
            GamePlayObjFactory returnObj = factories.Find(factory => factory.objectData.isSelecting == true);

            if (returnObj == null)
            {
                return SelectObject();
            } else return returnObj;
        }
    }

    public ObjectData[] ListObjectData
    {
        get
        {
            ObjectData[] listObjectData = new ObjectData[factories.Count];
            for (int index = 0; index < listObjectData.Length; index++)
            {
                listObjectData[index] = factories[index].objectData;
            }
            return listObjectData;
        }
        set
        {
            for (int index = 0; index < factories.Count; index++)
            {
                factories[index].objectData = value[index];
            }
        }
    }

    public GamePlayObjFactory UnlockObject(string objectName = "Default")
    {
        GamePlayObjFactory returnObj = 
            factories.Find(factory => factory.objectData.objectName == objectName);
        returnObj.objectData.isUnlocked = true;
        SaveLoadData.Instance.SaveDataToPersistent();
        return returnObj;
    }

    public GamePlayObjFactory SelectObject(string objectName = "Default")
    {
        GamePlayObjFactory returnObj = null;
        for (int index = 0; index < factories.Count; index++)
        {
            factories[index].objectData.isSelecting =
                factories[index].objectData.objectName == objectName && factories[index].objectData.isUnlocked;

            if (factories[index].objectData.objectName == objectName && factories[index].objectData.isUnlocked)
            {
                returnObj = factories[index];
            }
        }

        return returnObj;
    }

    public void ResetData()
    {
        for (int index = 0; index < factories.Count; index++)
        {
            //factories[index].objectData.isUnlocked = 
            //    factories[index].objectData.objectName == "Default";
            factories[index].objectData.isUnlocked =
                index <= 0;
            SelectObject();
        }
    }
}
