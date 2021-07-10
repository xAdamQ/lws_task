using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// this class is not a good practice because it centralise, decoupling is better
/// for demo purpose 
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Current;

    private void Awake()
    {
        // GameData.Delete();
        GameData.Load();

        Current = this;
        Money = GameData.I.Money;
    }

    public int Money
    {
        get { return int.Parse(moneyText.text); }
        set { moneyText.text = value.ToString(); }
    }

    [SerializeField] private Text moneyText;
}