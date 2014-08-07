using System;
using System.Collections.Generic;

namespace AJ.UtiliTools
{
    public enum OrderByType
    {
        Asc = 1, Desc = 2
    }

    public class OrderBy
    {
        public OrderByType OrderByType { get; set; }
        public String PropertyName { get; set; }

        public OrderBy(OrderByType orderByType, String propertyName)
        {
            OrderByType = orderByType;
            PropertyName = propertyName;
        }
    }

    public class OrderByGroup
    {
        public List<OrderBy> List { get; set; }

        public OrderByGroup()
        {
            List = new List<OrderBy>();
        }

        public void Add(OrderByType orderByType, String propertyName)
        {
            List.Add(new OrderBy(orderByType, propertyName));
        }
    }
}
