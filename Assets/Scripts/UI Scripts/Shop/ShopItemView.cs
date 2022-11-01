using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private int _price;

    public void Init(string description, int price, Sprite image)
    {
        _text.text = description;
        _price = price;
        _image.sprite = image;
    }
}