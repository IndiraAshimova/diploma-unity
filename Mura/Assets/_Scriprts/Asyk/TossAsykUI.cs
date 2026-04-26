//using System.Collections;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class TossAsykUI : MonoBehaviour
//{
//    [Header("Logic")]
//    public TossAsyk toss;

//    [Header("UI")]
//    public Image playerImage;
//    public Image botImage;
//    public Button tossButton;
//    public TextMeshProUGUI infoText;

//    [Header("Sprites")]
//    public Sprite alshySprite;
//    public Sprite taikeSprite;
//    public Sprite bukSprite;
//    public Sprite shigeSprite;

//    [Header("Animation")]
//    public float animationTime = 2f;
//    public float changeInterval = 0.1f;

//    void Start()
//    {
//        tossButton.onClick.AddListener(StartToss);

//        playerImage.enabled = false;
//        botImage.enabled = false;
//    }

//    void StartToss()
//    {
//        tossButton.interactable = false;

//        StartCoroutine(TossRoutine());
//    }

//    IEnumerator TossRoutine()
//    {
//        bool repeat = true;

//        while (repeat)
//        {
//            yield return StartCoroutine(
//                PlayAnimation()
//            );

//            AsykPosition playerResult =
//                toss.Toss();

//            AsykPosition botResult =
//                toss.Toss();

//            SetFinalSprite(
//                playerImage,
//                playerResult
//            );

//            SetFinalSprite(
//                botImage,
//                botResult
//            );

//            repeat =
//                resolver.IsSame(
//                    playerResult,
//                    botResult
//                );

//            if (repeat)
//            {
//                infoText.text =
//                    "Одинаковые асыки! Переброс!";

//                yield return new WaitForSeconds(1f);
//            }
//            else
//            {
//                PlayerType first =
//                    resolver.DecideFirst(
//                        playerResult,
//                        botResult
//                    );

//                TurnManager.Instance
//                    .SetFirstTurn(first);

//                infoText.text = "";
//            }
//        }
//    }

//    IEnumerator PlayAnimation()
//    {
//        float timer = 0f;

//        while (timer < animationTime)
//        {
//            SetRandomSprite(playerImage);
//            SetRandomSprite(botImage);

//            yield return new WaitForSeconds(
//                changeInterval
//            );

//            timer += changeInterval;
//        }
//    }

//    void SetRandomSprite(Image img)
//    {
//        int rand = Random.Range(0, 4);

//        SetSprite(
//            img,
//            (AsykPosition)rand
//        );
//    }

//    void SetFinalSprite(
//        Image img,
//        AsykPosition result)
//    {
//        SetSprite(img, result);
//    }

//    void SetSprite(
//        Image img,
//        AsykPosition pos)
//    {
//        img.enabled = true;

//        switch (pos)
//        {
//            case AsykPosition.Alshy:
//                img.sprite = alshySprite;
//                break;

//            case AsykPosition.Taike:
//                img.sprite = taikeSprite;
//                break;

//            case AsykPosition.Buk:
//                img.sprite = bukSprite;
//                break;

//            case AsykPosition.Shige:
//                img.sprite = shigeSprite;
//                break;
//        }
//    }
//}