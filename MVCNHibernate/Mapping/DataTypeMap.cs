using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Mapping;
using MVCNHibernate.Entities;

namespace MVCNHibernate.Mapping
{
    public class DataTypeMap : ClassMap<DataType>
    {
        public DataTypeMap()
        {
            Id(x => x.ID);

            Map(x => x.Name);
           
            Table("DataType");
        }
    }
}