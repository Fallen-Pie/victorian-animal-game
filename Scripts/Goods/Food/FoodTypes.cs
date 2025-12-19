using System;

namespace VictorianAnimalGame.Scripts.Goods.Food;

[Flags]
public enum FoodTypes
{
    Meat = 0b_0000_0001,
    Fish = 0b_0000_0010,
    Fruits = 0b_0000_0100,
    Vegetables = 0b_0000_1000,
    Tubers = 0b_0001_0000,
    Dairy = 0b_0010_0000,
    Honey = 0b_0100_0000,
    Nuts = 0b_1000_0000,
    
    Carnivore = Meat | Fish,
    Vegan = Fruits | Vegetables | Tubers | Nuts,
    Vegetarian = Vegan | Dairy | Honey,
    Pescatarian = Fish | Vegetarian,
    Omnivore = Carnivore | Vegetarian
}