using System;

[Serializable]
public class ObjectData
{
    public string objectName = "";
    public ObjectType objectType;
    public bool isUnlocked;
    public bool isSelecting;

    [Serializable]
    public class PurchaseData
    {
        public PurchaseType purchaseType;
        public int purchasePar;
    }
}

public enum ObjectType
{
    Paddle = 0,
    Ball = 1
}

public enum PurchaseType
{
    Default = 0,
    Shop = 1,
    ClearLevel = 2,
}