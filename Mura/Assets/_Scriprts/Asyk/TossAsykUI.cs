using UnityEngine;
using UnityEngine.UI;

public class TossAsykUI : MonoBehaviour
{
    public TossAsyk tossAsyk;       // ссылка на объект TossAsyk
    public Image resultImage;       // изображение для показа результата
    public Button tossButton;       // кнопка для броска

    [Header("Sprites for each position")]
    public Sprite alshySprite;
    public Sprite taikeSprite;
    public Sprite bukSprite;
    public Sprite shigeSprite;

    void Start()
    {
        tossButton.onClick.AddListener(OnTossButtonClicked);

        // Очистим изображение в начале
        resultImage.sprite = null;
        resultImage.enabled = false;
    }

    void OnTossButtonClicked()
    {
        // Бросаем асык
        tossAsyk.Toss();

        // Показываем спрайт в зависимости от результата
        switch (tossAsyk.tossResult)
        {
            case AsykPosition.Alshy:
                resultImage.sprite = alshySprite;
                break;
            case AsykPosition.Taike:
                resultImage.sprite = taikeSprite;
                break;
            case AsykPosition.Buk:
                resultImage.sprite = bukSprite;
                break;
            case AsykPosition.Shige:
                resultImage.sprite = shigeSprite;
                break;
        }

        resultImage.enabled = true;
    }
}