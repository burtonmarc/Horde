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
                BinarySaveSystem.SaveModelData(equipmentModelData);
            }
        }
        
        public ItemData EquippedNecklace
        {
            get => equipmentModelData.EquippedNecklace;
            set
            {
                equipmentModelData.EquippedNecklace = value;
                BinarySaveSystem.SaveModelData(equipmentModelData);
            }
        }
        
        public ItemData EquippedGloves
        {
            get => equipmentModelData.EquippedGloves;
            set
            {
                equipmentModelData.EquippedGloves = value;
                BinarySaveSystem.SaveModelData(equipmentModelData);
            }
        }
        
        public ItemData EquippedArmor
        {
            get => equipmentModelData.EquippedArmor;
            set
            {
                equipmentModelData.EquippedArmor = value;
                BinarySaveSystem.SaveModelData(equipmentModelData);
            }
        }
        
        public ItemData EquippedBelt
        {
            get => equipmentModelData.EquippedBelt;
            set
            {
                equipmentModelData.EquippedBelt = value;
                BinarySaveSystem.SaveModelData(equipmentModelData);
            }
        }
        
        public ItemData EquippedShoes
        {
            get => equipmentModelData.EquippedShoes;
            set
            {
                equipmentModelData.EquippedShoes = value;
                BinarySaveSystem.SaveModelData(equipmentModelData);
            }
        }

        public List<ItemData> InventoryItems
        {
            get => equipmentModelData.InventoryItems;
        }

        public override void AddModelData(IModelData modelData)
        {
            equipmentModelData = modelData as EquipmentModelData;
        }

        public void AddInventoryItem(ItemData itemData)
        {
            equipmentModelData.InventoryItems.Add(itemData);
            // TODO: Add sort to the inventory items
            //equipmentModelData.InventoryItems.Sort(item => item.ItemRarity);
        }
    }
}