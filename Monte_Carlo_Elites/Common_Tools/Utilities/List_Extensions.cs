using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Common_Tools.Utilities
{
    public static class List_Extensions
    {
        
        public static T Random_Item<T>(
            this HashSet<T> set,
            System.Random rand
            )
        {
            int random_selection_index = rand.Next(set.Count);
            return set.ElementAt(random_selection_index);
        }

        public static bool ContainsAll<T>(this List<T> mainList, List<T> containedList)
        {
            foreach (T item in containedList)
            {
                if (mainList.Contains(item) == false)
                {
                    return false;
                }
            }
            return true;
        }

        public static List<List<T>> GetNonSamePairs<T>(this List<T> originalList)
        {
            if (originalList.Count < 2)
            {
                return new List<List<T>>();
            }
            else
            {
                int originalListSize = originalList.Count;
                List<List<T>> listOfPairs = new List<List<T>>();
                for (int i = 0; i < originalListSize; i++)
                {
                    for (int j = 0; j < originalListSize; j++)
                    {
                        if (i != j)
                        {
                            T element1 = originalList[i];
                            T element2 = originalList[j];
                            List<T> pair = new List<T>() {
                                element1, element2
                            };
                            listOfPairs.Add(pair);
                        }
                    }
                }
                return listOfPairs;
            }
        }

        public static void SetFirstItemByIndex<T>(this List<T> items, int index)
        {
            List<T> reorderedItems = new List<T>();
            for (int i = index; i < items.Count; i++)
            {
                reorderedItems.Add(items[i]);
            }
            for (int i = 0; i < index; i++)
            {
                reorderedItems.Add(items[i]);
            }

            items.Clear();

            foreach (var rItem in reorderedItems)
            {
                items.Add(rItem);
            }

            //items = new List<T>(reorderedItems);
        }

        public static void Shuffle<T>(
            this List<T> items,
            System.Random rand
            )
        {
            List<T> shuffledItems = new List<T>();

            for (int i = items.Count - 1; i >= 0; i--)
            {
                shuffledItems.Add(items.Pop_Random_Item(rand));
            }

            items.Clear();
            for (int i = shuffledItems.Count - 1; i >= 0; i--)
            {
                items.Add(shuffledItems.Pop_Item(i));
            }
        }
    }
}