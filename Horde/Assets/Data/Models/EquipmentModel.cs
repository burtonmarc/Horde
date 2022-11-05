using System;
using System.Collections.Generic;

namespace Data.Models
{
    [Serializable]
    public class EquipmentModel : IModel
    {
        public ItemData EquippedWeapon;

        public ItemData EquippedNecklace;

        public ItemData EquippedGloves;

        public ItemData EquippedArmor;

        public ItemData EquippedBelt;

        public ItemData EquippedShoes;

        public List<ItemData> InventoryItems;

        public EquipmentModel()
        {
            EquippedWeapon = new ItemData
            {
                ItemId = "Shuriken",
                ItemLevel = 5,
                ItemRarity = "Common",
                ItemType = "Weapon"
            };
            
            EquippedNecklace = new ItemData
            {
                ItemId = "Lock",
                ItemLevel = 0,
                ItemRarity = "Common",
                ItemType = "Necklace"
            };
            
            EquippedGloves = new ItemData
            {
                ItemId = "Lock",
                ItemLevel = 0,
                ItemRarity = "Common",
                ItemType = "Gloves"
            };

            EquippedArmor = new ItemData
            {
                ItemId = "Armor",
                ItemLevel = 8,
                ItemRarity = "Uncommon",
                ItemType = "Armor"
            };
            
            EquippedBelt = new ItemData
            {
                ItemId = "Lock",
                ItemLevel = 0,
                ItemRarity = "Common",
                ItemType = "Belt"
            };
            
            EquippedShoes = new ItemData
            {
                ItemId = "Lock",
                ItemLevel = 0,
                ItemRarity = "Common",
                ItemType = "Shoes"
            };

            InventoryItems = new List<ItemData>
            {
                new ItemData
                {
                    ItemId = "Armor",
                    ItemLevel = 1,
                    ItemRarity = "Uncommon",
                    ItemType = "Armor"
                },
                new ItemData
                {
                    ItemId = "Shuriken",
                    ItemLevel = 1,
                    ItemRarity = "Legendary",
                    ItemType = "Weapon"
                },
                new ItemData
                {
                    ItemId = "Armor",
                    ItemLevel = 1,
                    ItemRarity = "Uncommon",
                    ItemType = "Armor"
                },
                new ItemData
                {
                    ItemId = "Armor",
                    ItemLevel = 1,
                    ItemRarity = "Uncommon",
                    ItemType = "Armor"
                }
            };
        }
    }
}