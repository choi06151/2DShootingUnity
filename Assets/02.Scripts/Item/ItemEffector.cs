using UnityEngine;

public abstract class ItemEffector : MonoBehaviour, IItemFun
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Init(Item item)
    {
        throw new System.NotImplementedException();
    }

    public abstract void Effect(Player player);
}