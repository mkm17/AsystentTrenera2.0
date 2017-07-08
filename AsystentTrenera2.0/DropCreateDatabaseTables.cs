using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AsystentTrenera2.Models;
using System.Data.Entity.Infrastructure;
using System.Transactions;

namespace AsystentTrenera2
{


    public class DropCreateDatabaseTables : IDatabaseInitializer<AsystentTrenera2Context>
    {

        #region IDatabaseInitializer<AsystentTrenera2Context> Members



        public void InitializeDatabase(AsystentTrenera2Context context)

    {

      bool dbExists;

      using (new TransactionScope(TransactionScopeOption.Suppress))

      {

        dbExists = context.Database.Exists();

      }

      if (dbExists)

      {       

        // remove all tables

        context.Database.ExecuteSqlCommand("EXEC sp_MSforeachtable @command1 = \"DROP TABLE ?\"");

 

        // create all tables

        var dbCreationScript = ((IObjectContextAdapter)context).ObjectContext.CreateDatabaseScript();

        context.Database.ExecuteSqlCommand(dbCreationScript);

            

        Seed(context);

        context.SaveChanges();

      }

      else

      {

          throw new ApplicationException("No database instance");

      }

    }



        #endregion



        #region Methods



        protected virtual void Seed(AsystentTrenera2Context context)
        {

            /// TODO: put here your seed creation
       
            

        }



        #endregion

    }


}