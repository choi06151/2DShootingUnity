using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private MaterialPropertyBlock _mpb;

    private float _offsetY;

    [SerializeField] private float _scrollSpeed;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _mpb = new MaterialPropertyBlock();
    }

    private void Update()
    {
        _offsetY += _scrollSpeed * Time.deltaTime;

        _renderer.GetPropertyBlock(_mpb);

        _mpb.SetVector(
            "_MainTex_ST",
            new Vector4(1, 1, 0, _offsetY)
        );

        _renderer.SetPropertyBlock(_mpb);
    }
}