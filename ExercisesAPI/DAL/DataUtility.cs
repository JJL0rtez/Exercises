using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using ExercisesAPI.DAL;
using ExercisesAPI.DAL.DomainClasses;
namespace ServerAPI.DAL
{
    public class DataUtility
    {
        private AppDbContext _db;
        public DataUtility(AppDbContext context)
        {
            _db = context;
        }
        public bool loadNutritionInfoFromWebToDb(string stringJson)
        {
            bool categoriesLoaded = false;
            bool menuItemsLoaded = false;
            try
            {
                // an element that is typed as dynamic is assumed to support any operation
                dynamic objectJson = JsonSerializer.Deserialize<Object>(stringJson);
                categoriesLoaded = loadCategories(objectJson);
                menuItemsLoaded = loadMenuItems(objectJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return categoriesLoaded && menuItemsLoaded;
        }
        private bool loadCategories(dynamic jsonObjectArray)
        {
            bool loadedCategories = false;
            try
            {
                // clear out the old rows
                _db.Categories.RemoveRange(_db.Categories);
                _db.SaveChanges();
                List<String> allCategories = new List<String>();
                foreach (JsonElement element in jsonObjectArray.EnumerateArray())
                {
                    if (element.TryGetProperty("CATEGORY", out JsonElement menuItemJson))
                    {
                        allCategories.Add(menuItemJson.GetString());
                    }
                }
                IEnumerable<String> categories = allCategories.Distinct<String>();
                foreach (string catname in categories)
                {
                    Category cat = new Category();
                    cat.Name = catname;
                    _db.Categories.Add(cat);
                    _db.SaveChanges();
                }
                loadedCategories = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in data  Utility - " + ex.Message);
            }
            return loadedCategories;
        }
        private bool loadMenuItems(dynamic jsonObjectArray)
        {
            bool loadedItems = false;
            try
            {
                List<Category> categories = _db.Categories.ToList();
                // clear out the old
                _db.MenuItems.RemoveRange(_db.MenuItems);
                _db.SaveChanges();
                foreach (JsonElement element in jsonObjectArray.EnumerateArray())
                {
                    MenuItem item = new MenuItem();
                    item.Calories = Convert.ToInt32(element.GetProperty("CAL").GetString());
                    item.Carbs = Convert.ToInt32(element.GetProperty("CARB").GetString());
                    item.Cholesterol = Convert.ToInt32(element.GetProperty("CHOL").GetString());
                    item.Fat = Convert.ToSingle(element.GetProperty("FAT").GetString());
                    item.Fibre = Convert.ToInt32(element.GetProperty("FBR").GetString());
                    item.Protein = Convert.ToInt32(element.GetProperty("PRO").GetString());
                    item.Salt = Convert.ToInt32(element.GetProperty("SALT").GetString());
                    item.Description = element.GetProperty("ITEM").GetString();
                    string cat = element.GetProperty("CATEGORY").GetString();
                    // add the FK here
                    foreach (Category category in categories)
                    {
                        if (category.Name == cat)
                        {
                            item.Category = category;
                            break;
                        }
                    }
                    _db.MenuItems.Add(item);
                    _db.SaveChanges();
                }
                loadedItems = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DataUtitity lower - " + ex.Message);
            }
            return loadedItems;
        }
    }
}

