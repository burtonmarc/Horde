using System;
using System.Collections.Generic;

namespace Data.Models
{
    [Serializable]
    public class EquipmentTitleData : ISerializableData
    {
        public ItemData EquippedWeapon;
        public ItemData EquippedNecklace;
        public ItemData EquippedGloves;
        public ItemData EquippedArmor;
        public ItemData EquippedBelt;
        public ItemData EquippedShoes;
        public List<ItemData> InventoryItems;
    }
    
    [Serializable]
    public class EquipmentUserData : ISerializableData
    {
        public ItemData EquippedWeapon;
        public ItemData EquippedNecklace;
        public ItemData EquippedGloves;
        public ItemData EquippedArmor;
        public ItemData EquippedBelt;
        public ItemData EquippedShoes;
        public List<ItemData> InventoryItems;

        public EquipmentUserData(EquipmentTitleData equipmentTitleData)
        {
            EquippedWeapon = equipmentTitleData.EquippedWeapon;
            EquippedNecklace = equipmentTitleData.EquippedNecklace;
            EquippedGloves = equipmentTitleData.EquippedGloves;
            EquippedArmor = equipmentTitleData.EquippedArmor;
            EquippedBelt = equipmentTitleData.EquippedBelt;
            EquippedShoes = equipmentTitleData.EquippedShoes;
            InventoryItems = equipmentTitleData.InventoryItems;
        }
    }
    
    public class EquipmentModel : SaveableBaseModel, IModel
    {
        private EquipmentUserData equipmentUserData;
        
        // References
        
        // Unsaved Data
        
        // Saved Data
        public ItemData EquippedWeapon
        {
            get => equipmentUserData.EquippedWeapon;
            set
            {
                equipmentUserData.EquippedWeapon = value;
                UserDataUpdater.UpdateUserData(equipmentUserData);
            }
        }
        
        public ItemData EquippedNecklace
        {
            get => equipmentUserData.EquippedNecklace;
            set
            {
                equipmentUserData.EquippedNecklace = value;
                UserDataUpdater.UpdateUserData(equipmentUserData);
            }
        }
        
        public ItemData EquippedGloves
        {
            get => equipmentUserData.EquippedGloves;
            set
            {
                equipmentUserData.EquippedGloves = value;
                UserDataUpdater.UpdateUserData(equipmentUserData);
            }
        }
        
        public ItemData EquippedArmor
        {
            get => equipmentUserData.EquippedArmor;
            set
            {
                equipmentUserData.EquippedArmor = value;
                UserDataUpdater.UpdateUserData(equipmentUserData);
            }
        }
        
        public ItemData EquippedBelt
        {
            get => equipmentUserData.EquippedBelt;
            set
            {
                equipmentUserData.EquippedBelt = value;
                UserDataUpdater.UpdateUserData(equipmentUserData);
            }
        }
        
        public ItemData EquippedShoes
        {
            get => equipmentUserData.EquippedShoes;
            set
            {
                equipmentUserData.EquippedShoes = value;
                UserDataUpdater.UpdateUserData(equipmentUserData);
            }
        }

        public List<ItemData> InventoryItems
        {
            get => equipmentUserData.InventoryItems;
        }

        public override void AddModelData(ISerializableData userData)
        {
            equipmentUserData = userData as EquipmentUserData;
        }

        public void AddInventoryItem(ItemData itemData)
        {
            equipmentUserData.InventoryItems.Add(itemData);
            // TODO: Add sort to the inventory items
            //equipmentModelData.InventoryItems.Sort(item => item.ItemRarity);
        }
    }
}