using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Tools.Utilities
{
    public static class List_Extensions_Item_Access
    {
        public static T First_Item<T>(this List<T> items)
        {
            return items[0];
        }

        public static T Last_Item<T>(this List<T> items)
        {
            return items[items.Count - 1];
        }

        public static T Random_Item<T>(
            this List<T> items,
            Random rand
            )
        {
            int random_selection_index = rand.Next(0, items.Count);
            return items[random_selection_index];
        }

        public static List<T> Random_Items<T>(
            this List<T> items,
            System.Random rand,
            int num_items)
        {
            List<T> random_unique_elements = new List<T>();

            for (int i = 0; i < num_items; i++)
            {
                int random_index = rand.Next(items.Count);

                random_unique_elements.Add(items[random_index]);
            }

            return random_unique_elements;
        }

        public static List<T> Random_Unique_Items<T>(
            this List<T> items,
            System.Random rand,
            int num_items)
        {
            List<T> temp_list = new List<T>(items);

            List<T> random_unique_elements = new List<T>();

            for (int i = 0; i < num_items; i++)
            {
                random_unique_elements.Add(
                    temp_list.Pop_Random_Item(rand)
                    );
            }

            return random_unique_elements;
        }

    }
}
