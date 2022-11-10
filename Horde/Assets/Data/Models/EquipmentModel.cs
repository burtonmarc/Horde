using System;
using System.Collections.Generic;

namespace Data.Models
{
    [Serializable]
    public class EquipmentModelData : IModelData
    {
        public ItemData EquippedWeapon;
        public ItemData EquippedNecklace;
        public ItemData EquippedGloves;
        public ItemData EquippedArmor;
        public ItemData EquippedBelt;
        public ItemData EquippedShoes;
        public List<ItemData> InventoryItems;

        public EquipmentModelData()
        {
            InventoryItems = new List<ItemData>();
        }
    }
    
    public class EquipmentModel : SaveableBaseModel, IModel
    {
        private EquipmentModelData equipmentModelData;
        
        // References
        
        // Unsaved Data
        
        // Saved Data
        public ItemData EquippedWeapon
        {
            get => equipmentModelData.EquippedWeapon;
            set
            {
                equipmentModelData.EquippedWeapon = value;
                SaveSystem.SaveModelData(equipmentModelData);
            }
        }
        
        public ItemData EquippedNecklace
        {
            get => equipmentModelData.EquippedNecklace;
            set
            {
                equipmentModelData.EquippedNecklace = value;
                SaveSystem.SaveModelData(equipmentModelData);
            }
        }
        
        public ItemData EquippedGloves
        {
            get => equipmentModelData.EquippedGloves;
            set
            {
                equipmentModelData.EquippedGloves = value;
                SaveSystem.SaveModelData(equipmentModelData);
            }
        }
        
        public ItemData EquippedArmor
        {
            get => equipmentModelData.EquippedArmor;
            set
            {
                equipmentModelData.EquippedArmor = value;
                SaveSystem.SaveModelData(equipmentModelData);
            }
        }
        
        public ItemData EquippedBelt
        {
            get => equipmentModelData.EquippedBelt;
            set
            {
                equipmentModelData.EquippedBelt = value;
                SaveSystem.SaveModelData(equipmentModelData);
            }
        }
        
        public ItemData EquippedShoes
        {
            get => equipmentModelData.EquippedShoes;
            set
            {
                equipmentModelData.EquippedShoes = value;
                SaveSystem.SaveModelData(equipmentModelData);
            }
        }

        public List<ItemData> InventoryItems
        {
            get => equipmentModelData.InventoryItems;
        }

        public EquipmentModel()
        {
        }
        
        public override void AddModelData(IModelData modelData)
        {
            equipmentModelData = modelData as EquipmentModelData;
            
            AddTestingData();
        }
        
        public void AddTestingData()
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

            AddInventoryItem(new ItemData
            {
                ItemId = "Armor",
                ItemLevel = 1,
                ItemRarity = "Uncommon",
                ItemType = "Armor"
            });
            
            AddInventoryItem(new ItemData
            {
                ItemId = "Shuriken",
                ItemLevel = 1,
                ItemRarity = "Legendary",
                ItemType = "Weapon"
            });
            
            AddInventoryItem(new ItemData
            {
                ItemId = "Armor",
                ItemLevel = 1,
                ItemRarity = "Uncommon",
                ItemType = "Armor"
            });
            
            AddInventoryItem(new ItemData
            {
                ItemId = "Armor",
                ItemLevel = 1,
                ItemRarity = "Uncommon",
                ItemType = "Armor"
            });
        }
        
        public void AddInventoryItem(ItemData itemData)
        {
            equipmentModelData.InventoryItems.Add(itemData);
            // TODO: Add sort to the inventory items
            //equipmentModelData.InventoryItems.Sort(item => item.ItemRarity);
        }
    }
}