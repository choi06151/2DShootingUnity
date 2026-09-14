using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UI_ButtonAnimation))]
public abstract class UI_ButtonParent : MonoBehaviour
{
    private Player _player;
    public Player Player => _player;
    private UI_ButtonAnimation _buttonAnimation;

    private Button _button;
    private Image _myImage;


    [Header("클릭 소리")]
    [SerializeField] private AudioClip _clickSound;

    [Header("버튼 타입")]
    [SerializeField] private bool _isToggle;

    [Header("평소 이미지")]
    [SerializeField] private Sprite _onImage;

    [Header("눌렸을때 이미지")]
    [SerializeField] private Sprite _offImage;


    protected bool _isPressed;

    private void Start()
    {
        _myImage = GetComponent<Image>();
        _buttonAnimation = GetComponent<UI_ButtonAnimation>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }

    public void Init(Player player)
    {
        _player = player;
        InitExecute();
    }

    protected abstract void InitExecute();

    public void OnClick()
    {
        AudioManager.Instance.ActivateEffectAudioClip(_clickSound);
        _buttonAnimation.PlayAnimation();
        ClickExecute();
        _isPressed = !_isPressed;
        SwitchUiSprite(_isPressed);
    }

    protected abstract void ClickExecute();


    private void SwitchUiSprite(bool isOn)
    {
        _myImage.sprite = isOn ? _onImage : _offImage;
    }
}