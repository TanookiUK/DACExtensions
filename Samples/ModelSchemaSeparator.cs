using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.SqlServer.Dac.Model;

namespace Public.Dac.Samples
{
    public static class ModelSchemaSeparator
    {
        /// <summary>
        /// Takes a TSqlModel and creates a dacpac per schema in the model
        /// </summary>
        public static string GetFilePathInCurrentDirectory(string filename)
        {
            return Path.Combine(Environment.CurrentDirectory, filename);
        }

        public static void CreateDacpacPerSchema(TSqlModel model)
        {
            ModelReader modelReader = new ModelReader();

            foreach(string schema in modelReader.GetSchema(model))
            {
                SchemaBasedFilter schemaBasedFilter = new SchemaBasedFilter(schema);

                schemaBasedFilter.Filtering = SchemaBasedFilter.FilterType.Include;

                ModelFilterer modelFilterer = new ModelFilterer(schemaBasedFilter);

                string packagePath = GetFilePathInCurrentDirectory(schema + ".dacpac");

                Console.WriteLine("Creating DacPac for schema: '" + schema + "'");

                modelFilterer.CreateFilteredDacpac(model, packagePath);
            }
        }
    }
}
