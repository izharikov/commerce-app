using System;
using System.Reflection;
using Commerce.Feature.Db.Catalog.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Commerce.Feature.Db.Common.DesignTime
{
    public class CatalogDbContextFactory : DefaultDesignTimeContextFactory<CatalogDbContext>
    {
        
    }
}