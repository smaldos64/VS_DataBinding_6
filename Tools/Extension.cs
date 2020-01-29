using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataBinding_6.Models;

namespace DataBinding_6.Tools
{
    public static class Extension
    {
        public static int FindIndex<T>(this ObservableCollection<T> SearchCollection, int SearchNumber)
        {
            dynamic SearchCollectionHere = SearchCollection;
            
            for (int CollectionCounter = 0; CollectionCounter < SearchCollection.Count; CollectionCounter++)
            {
                if (((Person)SearchCollectionHere[CollectionCounter]).ID == SearchNumber)
                    return (CollectionCounter);
            }

            return (-1);
        }

        public static int FindIndexFromString(this string SearchString, char SearchChar = ':')
        {
            int Index = -1;
            string NumberString;

            Index = SearchString.IndexOf(SearchChar);

            if (Index >= 0)
            {
                NumberString = SearchString.Substring(0, SearchChar - 1);
                try
                {
                    return (Convert.ToInt32(NumberString));
                }
                catch (Exception Error)
                {
                    return (-1);
                }
            }
            else
            {
                return (-1);
            }
        }
    }
}
