using UnityEngine;

public class SpeedItem : ItemEffector
{
    [Header("속도 증가량")] [SerializeField] private float _speedPlusMultiplier;

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
        player.TakeSpeedUp(_speedPlusMultiplier);
    }
}