using UnityEngine;
using UnityEngine.UI;

namespace Views.States.MainMenuState.Views
{
    public class EquippedItemView : MonoBehaviour
    {
        [SerializeField] private Image itemImage;
        
        [SerializeField] private Image itemRarity;

        public void SetItemIcon(Sprite item)
        {
            itemImage.sprite = item;
        }

        public void SetItemRarity(Color rarityColor)
        {
            itemRarity.color = rarityColor;
        }
        
    }
}