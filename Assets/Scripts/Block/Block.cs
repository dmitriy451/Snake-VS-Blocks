using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(SpriteRenderer))]
public class Block : MonoBehaviour
{
    [SerializeField] private Vector2Int _destroyPriceRange;
    [SerializeField] private Color[] _colors;

    private SpriteRenderer _spriteRenderer;
    private int _destroyPrice;
    private int _filling;
    public event UnityAction<int> FillingUpdated;
    private int LeftToFill => _destroyPrice - _filling;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _destroyPrice = Random.Range(_destroyPriceRange.x, _destroyPriceRange.y);
        FillingUpdated?.Invoke(LeftToFill);
        SetColor(LeftToFill);
    }

    private void Update()
    {
    }

    private void SetColor(int leftToFill)
    {
        float procent = 1f / 255f;
        float colorStep = (255f - 180f) / 10f;
        if (leftToFill >= 0 && leftToFill < 10)
        {
            Color color = new Color((180f + colorStep * leftToFill)*procent, 1f, 180f * procent, 1f);
            _spriteRenderer.color = color;
        }
        else if (leftToFill >= 10 && leftToFill < 20)
        {
            Color color = new Color(1f, (255f - colorStep * (leftToFill - 10f)) * procent, 180f * procent, 1f);
            _spriteRenderer.color = color;
        }
        else if (leftToFill >= 20 && leftToFill < 30)
        {
            Color color = new Color(1f, 180f * procent, (180f + colorStep * (leftToFill - 20f)) * procent, 1f);
            _spriteRenderer.color = color;
        }
        else if (leftToFill >= 30 && leftToFill < 40)
        {
            Color color = new Color((255f - colorStep * (leftToFill - 30f)) * procent, 180f * procent, 1f, 1f);
            _spriteRenderer.color = color;
        }
        else
        {
            Color color = new Color(180f * procent, 180f * procent, 1f, 1f);
            _spriteRenderer.color = color;
        }
    }

    public void Fill()
    {
        _filling++;
        SetColor(LeftToFill);
        FillingUpdated?.Invoke(LeftToFill);
        if (_filling == _destroyPrice)
        {
            Destroy(gameObject);
        }
    }
}
