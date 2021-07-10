using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private Text priceText;
    [SerializeField] private Button itemButton;

    private int Price
    {
        get { return int.Parse(priceText.text); }
    }

    private void SetBought()
    {
        itemButton.GetComponent<Image>().color = Color.green;
        itemButton.transform.GetChild(0).GetComponent<Text>().text = "use";
    }

    private void Start()
    {
        if (GameData.I.BoughtItemsIds.Contains(transform.GetSiblingIndex()))
        {
            SetBought();
        }
    }

    public void ButtonClick()
    {
        if (GameManager.Current.Money >= Price && !GameData.I.BoughtItemsIds.Contains(transform.GetSiblingIndex()))
        {
            GameManager.Current.Money = GameData.I.Money -= Price;

            GameData.I.BoughtItemsIds.Add(transform.GetSiblingIndex());

            GameData.Save();

            SetBought();
        }
    }
}