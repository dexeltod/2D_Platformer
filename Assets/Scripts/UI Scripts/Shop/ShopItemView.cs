using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _price;

    public void Initialize(string title, string description, int price, Sprite image)
    {
        _title.text = title;
        _description.text = description;
        _price.text = price.ToString();
        _image.sprite = image;
    }
}