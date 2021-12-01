using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private float _fade_intensity = 0.0f;
    [SerializeField] private float _txt_intensity = 0.0f;
    [SerializeField] public Color _color = Color.black;
    [SerializeField] public SpriteRenderer _sprite;
    [SerializeField] public Text _chapterTitle;

    private void Start()
    {
        _sprite.color = _color;
        _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, 0);
        _chapterTitle.color = new Color(_chapterTitle.color.r, _chapterTitle.color.g, _chapterTitle.color.b, 0);
    }
    public Coroutine StartFadeIn()
    {
        _fade_intensity = 0;
        _txt_intensity = 0;
        StopAllCoroutines();
        return StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, 0);
        _chapterTitle.color = new Color(_chapterTitle.color.r, _chapterTitle.color.g, _chapterTitle.color.b, 0);
        while (_fade_intensity <= 1.0f)
        {
            _fade_intensity += _speed * Time.deltaTime;
            _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, _fade_intensity);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        while (_txt_intensity <= 1.0f)
        {
            _txt_intensity += _speed * Time.deltaTime;
            _chapterTitle.color = new Color(_chapterTitle.color.r, _chapterTitle.color.g, _chapterTitle.color.b, _txt_intensity);
            yield return null;
        }
    }

    public Coroutine StartFadeOut()
    {
        StopAllCoroutines();
        return StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        _chapterTitle.color = new Color(_chapterTitle.color.r, _chapterTitle.color.g, _chapterTitle.color.b, 1);
        while (_fade_intensity >= 0.0f)
        {
            _fade_intensity -= _speed * Time.deltaTime;
            _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, _fade_intensity);
            _chapterTitle.color = new Color(_chapterTitle.color.r, _chapterTitle.color.g, _chapterTitle.color.b, _fade_intensity);
            yield return null;
        }
    }
}


