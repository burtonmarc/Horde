using System;
using ScreenMachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Views.Helpers;

namespace Views.States.MainMenuState.Views
{
    public class MainMenuStateUiView : UiView
    {
        public TextMeshProUGUI UserLevel;
        
        public Button LevelUpButton;
        
        public Button StartGameButton;

        [Header("Equipped Items Panel")]
        public Transform EquippedItemsParentLeft;
        
        public Transform EquippedItemsParentRight;
        
        public EquippedItemView EquippedItemPrefab;
        
        [Header("Inventory Items Panel")]
        public Transform InventoryItemsParent;
        
        public InventoryItemView InventoryItemPrefab;

        public event Action LevelUpClicked;
        public event Action StartGameClicked;

        public override void OnUpdate()
        {
            
        }
        
        private void OnEnable()
        {
            LevelUpButton.onClick.AddListener(FireLevelUpButton);
            StartGameButton.onClick.AddListener(FireStartGameButton);
        }

        private void OnDisable()
        {
            LevelUpButton.onClick.RemoveListener(FireLevelUpButton);
            StartGameButton.onClick.RemoveListener(FireStartGameButton);
        }

        private void OnDestroy()
        {
            
        }

        public void SetUserLevel(int userLevel)
        {
            UserLevel.text = userLevel.ToString();
        }
        
        public void AddEquippedItem(Sprite itemIcon, int itemLevel, string itemRarity, string itemType)
        {
            EquippedItemView equippedItemView;
            
            if (ItemTypeHelper.IsLeftEquipment(itemType))
            {
                equippedItemView = Instantiate(EquippedItemPrefab, EquippedItemsParentLeft);
            }
            else
            {
                equippedItemView = Instantiate(EquippedItemPrefab, EquippedItemsParentRight);
            }
            
            equippedItemView.SetItemIcon(itemIcon);

            equippedItemView.SetItemLevel(itemLevel);
            
            var itemRarityColor = ItemRarityHelper.GetColorByRarity(itemRarity);
            equippedItemView.SetItemRarityColor(itemRarityColor);
        }

        public void AddInventoryItem(Sprite itemIcon, int itemLevel, string itemRarity, string itemType)
        {
            InventoryItemView inventoryItemView;
            
            if (ItemTypeHelper.IsLeftEquipment(itemType))
            {
                inventoryItemView = Instantiate(InventoryItemPrefab, InventoryItemsParent);
            }
            else
            {
                inventoryItemView = Instantiate(InventoryItemPrefab, InventoryItemsParent);
            }
            
            inventoryItemView.SetItemIcon(itemIcon);

            inventoryItemView.SetItemLevel(itemLevel);
            
            var itemRarityColor = ItemRarityHelper.GetColorByRarity(itemRarity);
            inventoryItemView.SetItemRarityColor(itemRarityColor);
        }
        
        private void FireLevelUpButton()
        {
            LevelUpClicked?.Invoke();
        }

        private void FireStartGameButton()
        {
            StartGameClicked?.Invoke();
        }
    }
}