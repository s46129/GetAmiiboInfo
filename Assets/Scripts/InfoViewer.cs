using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoViewer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI amiiboSeries;
    [SerializeField] TextMeshProUGUI character;
    [SerializeField] TextMeshProUGUI gameSeries;
    [SerializeField] TextMeshProUGUI name;
    [SerializeField] private Image image;
    private string _infoImageURL;
    private Coroutine _coroutine;

    private void OnDisable()
    {
        Dispose();
    }

    public void AssignInfo(AmiiboInfo info)
    {
        amiiboSeries.text = $"amiiboSeries: {info.amiiboSeries}";
        character.text = $"character: {info.character}";
        gameSeries.text = $"gameSeries: {info.gameSeries}";
        name.text = $"name: {info.name}";
        GetImage(info);
    }

    private void GetImage(AmiiboInfo info)
    {
        _infoImageURL = info.image;
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(APIManager.DownloadImage(_infoImageURL, texture =>
        {
            if (texture == null) return;

            var sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height),
                new Vector2(0.5f, 0.5f), 100.0f);
            image.sprite = sprite;
        }));
    }

    public void Dispose()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }
}