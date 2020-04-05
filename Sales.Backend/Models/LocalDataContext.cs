﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Sales.Backend.Models
{
    using Sales.Domain.Models;

    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<Sales.Common.Models.Product> Products { get; set; }
    }
}