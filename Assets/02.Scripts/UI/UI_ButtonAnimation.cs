using UnityEngine;

public class UI_ButtonAnimation : MonoBehaviour
{
    [Header("눌릴때 버튼 최대 크기")]
    [SerializeField] private float _scale;

    [Header("애니메이션 시간")]
    [SerializeField] private float _bumpDuration;

    [Header("클릭시 애니메이션")]
    [SerializeField] private AnimationCurve _bumpCurve;

    private const float BumpScale = 1.2f;
    private bool _isBump;
    private float _elapsedTime;

    private void Update()
    {
        if (!_isBump) return;

        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > _bumpDuration)
        {
            transform.localScale = Vector3.one;
            _isBump = false;
            return;
        }

        float time = _elapsedTime / _bumpDuration;
        float curveValue = _bumpCurve.Evaluate(time);
        transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * BumpScale, curveValue);
    }

    public void PlayAnimation()
    {
        _isBump = true;
        _elapsedTime = 0.0f;
    }
}