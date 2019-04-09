using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Dac.Model;

namespace Public.Dac.Samples
{
    public class ModelReader
    {
        /// <summary>
        /// Takes a TsqlModel and returns an array of schemas contained in the model
        /// </summary>
        private HashSet<string> _schemas;

        public ModelReader()
        {
            _schemas = new HashSet<string>();
        }

        public string[] GetSchema(TSqlModel model)
        {
            foreach (TSqlObject tSqlObject in model.GetObjects(DacQueryScopes.UserDefined))
            {
                ObjectIdentifier id = tSqlObject.Name;

                if (id.HasName && id.Parts.Count > 1)
                {
                    if (_schemas.Contains(id.Parts[0]) == false)
                    {
                        _schemas.Add(id.Parts[0]);
                    }
                }
            }

            return _schemas.ToArray();
        }
    }
}
