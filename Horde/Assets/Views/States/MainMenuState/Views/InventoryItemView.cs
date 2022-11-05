using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views.States.MainMenuState.Views
{
    public class InventoryItemView : MonoBehaviour
    {
        [SerializeField] private Image itemImage;

        [SerializeField] private TextMeshProUGUI itemLevel;
        
        [SerializeField] private Image itemRarity;

        public void SetItemIcon(Sprite itemIcon)
        {
            itemImage.sprite = itemIcon;
        }

        public void SetItemLevel(int iLevel)
        {
            itemLevel.gameObject.SetActive(true);
            itemLevel.text = iLevel.ToString();
        }

        public void SetItemRarityColor(Color rarityColor)
        {
            itemRarity.color = rarityColor;
        }
    }
}