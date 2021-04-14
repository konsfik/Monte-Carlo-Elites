using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Tools.Utilities
{
    public static class Array_1D_Extensions__Item_Access
    {
        public static T First_Item<T>(this T[] items)
        {
            return items[0];
        }

        public static T Last_Item<T>(this T[] items)
        {
            return items[items.Length - 1];
        }

        public static T Random_Item<T>(
            this T[] items,
            Random rand
            )
        {
            int random_selection_index = rand.Next(0, items.Length);
            return items[random_selection_index];
        }
    }
}
