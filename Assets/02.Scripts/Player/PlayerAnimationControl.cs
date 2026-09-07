using UnityEngine;

public class PlayerAnimationControl : MonoBehaviour, IPlayerFun
{
    private Player _player;
    private Animator _animator;
    static readonly int ANIM_Play = Animator.StringToHash("x");

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Init(Player player)
    {
        _player = player;
    }

    public void UpdateMoveAnimation(int input)
    {
        _animator.SetInteger(ANIM_Play, input);
    }
}