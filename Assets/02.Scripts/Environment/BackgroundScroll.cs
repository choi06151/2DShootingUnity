using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    private Material _material;
    private float _offsetY;
    [SerializeField] private float _scrollSpeed;

    void Start()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        _offsetY += _scrollSpeed * Time.deltaTime;
        _material.mainTextureOffset = new Vector2(0, _offsetY);
    }
}