using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogForm.Entities
{
    public static class Seeder
    {
        public static void SeedDatabase(EFContext context)
        {
            SeedFilter(context);
        }

        #region Фільтри
        private static void SeedFilter(EFContext context)
        {
            #region tblFilterNames - Назви фільтрів
            string[] filterNames = { "Виробник", "Діагональ екрана", "Процесор" };
            foreach (var type in filterNames)
            {
                if (context.FilterNames.SingleOrDefault(f => f.Name == type) == null)
                {
                    context.FilterNames.Add(
                        new FilterName
                        {
                            Name = type
                        });
                    context.SaveChanges();
                }
            }
            #endregion

            #region tblFilterValues - Значення фільтрів
            List<string[]> filterValues = new List<string[]> {
                new string [] { "HP", "Dell", "Apple" },
                new string [] { "13\"", "14\"", "15\"-15.6\"", "16\"-17\"" },
                new string [] { "Intel Core i5", "Intel Core i7", "AMD Ryzen 9" }
            };

            foreach (var items in filterValues)
            {
                foreach (var value in items)
                {
                    if (context.FilterValues
                        .SingleOrDefault(f => f.Name == value) == null)
                    {
                        context.FilterValues.Add(
                            new FilterValue
                            {
                                Name = value
                            });
                        context.SaveChanges();
                    }
                }
            }
            #endregion

            #region Групування фільтрів

            for (int i = 0; i < filterNames.Length; i++)
            {
                foreach (var value in filterValues[i])
                {
                    var nId = context.FilterNames
                        .SingleOrDefault(ben => ben.Name == filterNames[i]).Id;
                    var vId = context.FilterValues
                        .SingleOrDefault(f => f.Name == value).Id;
                    if (context.FilterNameGroups
                        .SingleOrDefault(f => f.FilterValueId == vId &&
                        f.FilterNameId == nId) == null)
                    {
                        context.FilterNameGroups.Add(
                            new FilterNameGroup
                            {
                                FilterNameId = nId,
                                FilterValueId = vId
                            });
                        context.SaveChanges();
                    }
                }
            }

            #endregion


            #region tblProducts - Продукти
            List<string> prods = new List<string>{
                //Path.GetRandomFileName()
             "154muv2f", "154m2fas"
            };
            if (context.Products.SingleOrDefault(f => f.UniqueName == prods[0]) == null)
                context.Products.Add(
                        new Product
                        {
                            UniqueName = prods[0],
                            Price = 32599,
                            Image= "https://hotline.ua/img/tx/262/2620857135.jpg",
                            Name = "Ноутбук HP EliteBook 850 G6 (6XD79EA) Silver"
                        });

            if (context.Products.SingleOrDefault(f => f.UniqueName == prods[1]) == null)
                context.Products.Add(
                  new Product
                  {
                      UniqueName = prods[1],
                      Price = 45999,
                      Image = "https://hotline.ua/img/tx/208/208413187_s265.jpg",
                      Name = "Dell XPS 15 7590 (X7590UTI932S10ND1650W-9S) Silver"
                  });
            context.SaveChanges();

            #endregion


            #region tblFilters -Фільтри
            Filter[] filters =
            {
                new Filter { FilterNameId = 1, FilterValueId=1, ProductId=1 },
                new Filter { FilterNameId = 2, FilterValueId=5, ProductId=1 },

                new Filter { FilterNameId = 1, FilterValueId=2, ProductId=2 },
                new Filter { FilterNameId = 2, FilterValueId=6, ProductId=2 }
            };
            foreach (var item in filters)
            {
                var f = context.Filters.SingleOrDefault(p => p == item);
                if (f == null)
                {
                    context.Filters.Add(new Filter { FilterNameId = item.FilterNameId, FilterValueId = item.FilterValueId, ProductId = item.ProductId });
                    context.SaveChanges();
                }
            }
            #endregion
        }


        #endregion
    }
}
