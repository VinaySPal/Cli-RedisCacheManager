using CustomException;
using DataLayerSQL;
using DataLayerSQL.Helper;
using DTOObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace EntityMetadataDL
{
    public class EntityConfigDL : DBCommand<EntityFieldModel>
    {
        /// <summary>
        /// This method fetches the Entity Configuration of fields passed as datatable
        /// </summary>
        /// <param name="fieldsDT"></param>
        /// <returns></returns>
        public List<EntityFieldModel> GetEntityConfig(DataTable fieldsDT)
        {
            try
            {
                fieldsDT.TableName = "Fields";
                CommandParameters cmdParams = new CommandParameters();
                cmdParams.Add(new CommandParameter { Name = "@Fields", Value = fieldsDT, DataTypeInDb = SqlDbType.Structured });

                return base.Get("USP_GetEntityConfiguration", cmdParams, true).ToList<EntityFieldModel>();
            }
            catch (Exception ex)
            {
                throw new HandledException("Failed to retrive entity configuration. Please check Console log", ex);
            }
        }

        /// <summary>
        /// This method Saves(Insert or Update) the Entity Configuration into the database which is passed as datatable
        /// </summary>
        /// <param name="configsDT"></param>
        /// <returns>No of affected rows</returns>
        public int SaveEntityConfig(DataTable configsDT)
        {
            try
            { 
                configsDT.TableName = "EntityConfig";
                CommandParameters cmdParams = new CommandParameters();
                cmdParams.Add(new CommandParameter { Name = "@EntityConfig", Value = configsDT, DataTypeInDb = SqlDbType.Structured });

                return base.ExecuteNonQuery("USP_SaveEntityConfiguration", cmdParams, true);
            }
            catch (Exception ex)
            {
                throw new HandledException("Failed to save the provided json. Please check Console log", ex);
            }
        }

        protected override EntityFieldModel CreateEntity()
        {
            return new EntityFieldModel();
        }

        protected override void Map(IDataRecord record, EntityFieldModel entity)
        {
            entity.EntityName = Convert.ToString(record["EntityName"]);
            entity.FieldName = Convert.ToString(record["FieldName"]);
            entity.IsRequired = Convert.ToBoolean(record["IsRequired"]);
            entity.MaxLength = Convert.ToInt32(record["MaxLength"]);
            entity.FieldSource = DbNullHelper<string>.DBNullToNull(record["FieldSource"]);
        }
    }
}
