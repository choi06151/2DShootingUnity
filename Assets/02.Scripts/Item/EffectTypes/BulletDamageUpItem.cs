using UnityEngine;

public class BulletDamageUpItem : ItemEffector
{
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
        player.TakeDamageUp();
    }
}