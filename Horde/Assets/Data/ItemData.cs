using System;

namespace Data
{
    [Serializable]
    public class ItemData
    {
        public string ItemId;

        public string ItemType;

        public string ItemRarity;

        public int ItemLevel;

        public ItemData()
        {
            ItemId = "Lock";
            ItemType = "nil";
            ItemRarity = "nil";
            ItemLevel = 0;
        }
    }
}