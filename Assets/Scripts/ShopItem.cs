using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private Text totalFruitsText;
    private int price;
    private Text textPrice;
    private Button buttonBuy;
    private Text textButtonBuy;

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

                totalFruitsText.text = "TOTAL FRUITS: " + PlayerPrefs.GetInt("TotalFruits", 0);
                isPurchased = PlayerPrefs.GetInt(itemName + "_Purchased", 0) == 1;
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

            ColorBlock buttonColors = buttonBuy.colors;
            buttonColors.normalColor = new Color(0.0f, 255.0f, 0.0f, 0.5f);
            buttonColors.pressedColor = new Color(0.0f, 100.0f, 0.0f, 0.5f);
            buttonColors.selectedColor = new Color(0.0f, 255.0f, 0.0f, 0.5f);
            buttonColors.disabledColor = new Color(0.0f, 255.0f, 0.0f, 0.5f);
            buttonBuy.colors = buttonColors;
        }
        else if (isPurchased)
        {
            textPrice.text = "Bought";
            textButtonBuy.text = "Select";

            ColorBlock buttonColors = buttonBuy.colors;
            buttonColors.normalColor = new Color(255.0f, 255.0f, 255.0f, 1.0f);
            buttonColors.pressedColor = new Color(100.0f, 100.0f, 100.0f, 1.0f);
            buttonColors.selectedColor = new Color(255.0f, 255.0f, 255.0f, 1.0f);
            buttonColors.disabledColor = new Color(255.0f, 255.0f, 255.0f, 1.0f);
            buttonBuy.colors = buttonColors;
        }
    }
}
