using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private Text totalFruitsText;
    private Text textPrice;
    private Button buttonBuy;
    private Text textButtonBuy;

    private int price;
    private bool isPurchased;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(itemName + "_Purchased"))
        {
            PlayerPrefs.SetInt(itemName + "_Purchased", 0);
            PlayerPrefs.Save();
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        textPrice = transform.Find("TextPrice").GetComponentInChildren<Text>();
        buttonBuy = GetComponentInChildren<Button>();
        textButtonBuy = buttonBuy.GetComponentInChildren<Text>();
        price = int.Parse(textPrice.text.Replace("Price: ", ""));

        isPurchased = PlayerPrefs.GetInt(itemName + "_Purchased", 0) == 1;

        UpdateUI();

        buttonBuy.onClick.AddListener(OnBuyButtonClick);
    }

    private void LateUpdate()
    {
        UpdateUI();
    }

    public void OnBuyButtonClick()
    {
        if (!isPurchased)
        {
            int totalFruits = PlayerPrefs.GetInt("TotalFruits", 0);

            if (totalFruits >= price)
            {
                totalFruits -= price;

                PlayerPrefs.SetInt("TotalFruits", totalFruits);
                PlayerPrefs.SetString("SelectedPlayerSkin", itemName);
                PlayerPrefs.SetInt(itemName + "_Purchased", 1);
                PlayerPrefs.Save();

                totalFruitsText.text = "TOTAL FRUITS: " + totalFruits;
                isPurchased = true;
                UpdateUI();
            }
            else
            {
                Debug.Log("Not enough Fruits!");
            }
        }
        else
        {
            PlayerPrefs.SetString("SelectedPlayerSkin", itemName);
            PlayerPrefs.Save();
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (PlayerPrefs.GetString("SelectedPlayerSkin") == itemName)
        {
            isPurchased = true;
            textPrice.text = "Bought";
            textButtonBuy.text = "Selected";

            SetButtonColors(new Color(0.0f, 255.0f, 0.0f, 0.5f));
        }
        else if (isPurchased)
        {
            textPrice.text = "Bought";
            textButtonBuy.text = "Select";

            SetButtonColors(new Color(255.0f, 255.0f, 255.0f, 1.0f));
        }
    }

    private void SetButtonColors(Color normalColor)
    {
        ColorBlock buttonColors = buttonBuy.colors;
        buttonColors.normalColor = normalColor;
        buttonColors.pressedColor = normalColor * 0.4f;
        buttonColors.selectedColor = normalColor;
        buttonColors.disabledColor = normalColor;
        buttonBuy.colors = buttonColors;
    }
}
