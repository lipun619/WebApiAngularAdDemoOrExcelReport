using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiAngularAdDemo.DataComponents.Helpers
{
    public static class DataTableToObjectUtility
    {
        public static List<T> AbsoluteConvertToDataTable<T>(this DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = AbsoluteGetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        public static T AbsoluteGetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();
            object value = null;

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (System.Reflection.PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        try
                        {
                            value = dr[column.ColumnName] == DBNull.Value ? null : dr[column.ColumnName];
                        }
                        catch (InvalidCastException)
                        {

                        }
                        pro.SetValue(obj, value, null);
                    }
                    else
                        continue;
                }
            }
            return obj;
        }
    }
}
