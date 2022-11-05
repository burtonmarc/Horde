namespace Views.Helpers
{
    public static class ItemTypeHelper
    {
        private const string weapon = "Weapon";

        private const string necklace = "Necklace";

        private const string gloves = "Gloves";

        private const string armor = "Armor";

        private const string belt = "Belt";

        private const string shoes = "Shoes";

        public static bool IsLeftEquipment(string itemType)
        {
            return itemType == weapon || itemType == necklace || itemType == gloves;
        }
    }
}