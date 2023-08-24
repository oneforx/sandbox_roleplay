using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roleplay.Systems
{
    public static class ListExtensions
    {
		// List<Business>
		public static List<T> ListLinkedTo<T>(this List<Systems.Table> fromList) where T : Table
		{
			List<T> list = new List<T>();
			foreach (Table item in fromList)
			{
				foreach (Link link in Database.Current.GetListOfLinkOfType(Database.Types[item.TableType], typeof(T)))
				{
					if (link.To.Type == typeof(T).Name)
					{
						list.Add(Database.Current.GetTableById<T>(link.To.Id));
					}
				}
			}
			return list;
		}

        public static List<T> ConvertTo<T> (this List<Table> fromList) where T : Table
        {
            List<T> convertedList = new List<T>();
            foreach (Table item in fromList)
            {
                convertedList.Add((T)item);
            }
            return convertedList;
        }

	}
}
