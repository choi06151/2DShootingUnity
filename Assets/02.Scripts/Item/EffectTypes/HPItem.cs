using UnityEngine;

public class HPItem : ItemEffector
{
    [Header("체력 증가량")] [SerializeField] float _hpUpAmount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public override void Effect(Player player)
    {
        player.TakeHp(_hpUpAmount);
    }
}