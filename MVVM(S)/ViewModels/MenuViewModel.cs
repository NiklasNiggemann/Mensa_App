﻿using CommunityToolkit.Mvvm.ComponentModel;
using Mensa_App.MVVMS.Models;
using Mensa_App.Classes;
using CommunityToolkit.Mvvm.Input;
using System.Linq;

namespace Mensa_App.MVVMS.ViewModels;

public partial class MenuViewModel : ObservableObject
{
    public MenuModel MenuModel { get; set; }
    public List<Dish> MainMenuView { get; set; }
    public List<Dish> SideMenuView { get; set; }
    public List<Dish> SoupMenuView { get; set; }
    public List<Dish> DessertMenuView { get; set; }

    public MenuViewModel()
    {
        MenuModel = new MenuModel();
        MainMenuView = MenuModel.MainMenu;
        SideMenuView = MenuModel.SideMenu;
        SoupMenuView = MenuModel.SoupMenu;
        DessertMenuView = MenuModel.DessertMenu;
        SettingsModel.UserAllergyIngredientList.CollectionChanged += UserAllergyIngredientList_CollectionChanged;
    }

    private void UserAllergyIngredientList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        foreach (var dish in MainMenuView)
        {
            foreach (var ingredient in dish.Ingredients)
            {
                foreach (var allergyIngredient in SettingsModel.UserAllergyIngredientList)
                {
                    if (ingredient.Name == allergyIngredient)
                    {
                        ingredient.IsAllergic = !ingredient.IsAllergic;
                    }
                }
            }
        }
    }
    [RelayCommand]
    public static void ChangeSelectedDishes(IList<object> selectedDishes)
    {
        foreach (var dish in selectedDishes)
        {
            SelectionModel.SelectedDishes.Remove(dish as Dish);
        }
        foreach (var dish in selectedDishes)
        {
            SelectionModel.SelectedDishes.Add(dish as Dish);
        }
    }
}

