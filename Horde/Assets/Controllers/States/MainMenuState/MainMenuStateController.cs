using System;
using System.Threading.Tasks;
using Catalogs.Scripts.Configs;
using Controllers.States.GameplayState;
using Data;
using Data.Models;
using Game.States.MainMenu;
using ScreenMachine;
using UnityEditor.Build.Content;
using UnityEngine;
using Views.States.GameplayState;
using Views.States.MainMenuState;
using Views.States.MainMenuState.Views;
using Object = UnityEngine.Object;

namespace Controllers.States.MainMenuState
{
    public class MainMenuStateController : BaseStateController<MainMenuStateUiView, MainMenuWorldView>, IStateBase, IPreloadable
    {
        protected override string StateId { get; }

        private UserModel userModel;

        private GameObject camera;
        
        public MainMenuStateController(Context context) : base(context)
        {
            StateId = "MainMenu";
            userModel = context.UserModel;
        }
        
        public void OnCreate()
        {
            UiView.Init();
            WorldView.Init();
            
            UiView.SetUserLevel(userModel.Level);
            AddEquippedItemsToView();
            AddInventoryItemsToView();

            UiView.LevelUpClicked += LevelUp;
            UiView.StartGameClicked += PresentGameplayState;

            CreateCameraController();
        }

        public void OnBringToFront()
        {
            
        }

        public void OnSendToBack()
        {
            
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        private void AddEquippedItemsToView()
        {
            var equipmentModel = userModel.EquipmentModel;
            
            AddEquippedItem(equipmentModel.EquippedWeapon);
            AddEquippedItem(equipmentModel.EquippedNecklace);
            AddEquippedItem(equipmentModel.EquippedGloves);
            AddEquippedItem(equipmentModel.EquippedArmor);
            AddEquippedItem(equipmentModel.EquippedBelt);
            AddEquippedItem(equipmentModel.EquippedShoes);
        }

        private void AddEquippedItem(ItemData itemData)
        {
            var itemIcon = Preloader.GetAsset<Sprite>(Context.CatalogsHolder.ItemsCatalog.GetCatalogEntry(itemData.ItemId).ItemIcon);
            UiView.AddEquippedItem(itemIcon, itemData.ItemLevel, itemData.ItemRarity, itemData.ItemType);
        }

        private void AddInventoryItemsToView()
        {
            foreach (var inventoryItem in userModel.EquipmentModel.InventoryItems)
            {
                var itemIcon = Preloader.GetAsset<Sprite>(Context.CatalogsHolder.ItemsCatalog.GetCatalogEntry(inventoryItem.ItemId).ItemIcon);
                UiView.AddInventoryItem(itemIcon, inventoryItem.ItemLevel, inventoryItem.ItemRarity, inventoryItem.ItemType);
            }
        }
        
        private void CreateCameraController()
        {
            var cameraReference = GetStateAsset<CameraConfig>().Camera;
            camera = Object.Instantiate(cameraReference, WorldView.transform);
        }
        
        private void LevelUp()
        {
            userModel.Level++;
            UiView.SetUserLevel(userModel.Level);
        }

        private void PresentGameplayState()
        {
            PresentState(new GameplayStateController(Context));
        }

        public Task Preload()
        {
            Preloader = Context.AssetLoaderFactory.CreateLoader(StateId);
            
            Preloader.AddReference(Context.CatalogsHolder.WeaponsCatalog.GetCatalogEntry("Shuriken").WeaponConfig);

            Preloader.AddReference(Context.CatalogsHolder.ItemsCatalog.GetCatalogEntry(userModel.EquipmentModel.EquippedWeapon.ItemId).ItemIcon);
            Preloader.AddReference(Context.CatalogsHolder.ItemsCatalog.GetCatalogEntry(userModel.EquipmentModel.EquippedNecklace.ItemId).ItemIcon);
            Preloader.AddReference(Context.CatalogsHolder.ItemsCatalog.GetCatalogEntry(userModel.EquipmentModel.EquippedGloves.ItemId).ItemIcon);
            Preloader.AddReference(Context.CatalogsHolder.ItemsCatalog.GetCatalogEntry(userModel.EquipmentModel.EquippedArmor.ItemId).ItemIcon);
            Preloader.AddReference(Context.CatalogsHolder.ItemsCatalog.GetCatalogEntry(userModel.EquipmentModel.EquippedBelt.ItemId).ItemIcon);
            Preloader.AddReference(Context.CatalogsHolder.ItemsCatalog.GetCatalogEntry(userModel.EquipmentModel.EquippedShoes.ItemId).ItemIcon);

            var task = Preloader.LoadAsync();

            return task;
        }
    }
}