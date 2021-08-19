using CustomException;
using DTOObjects;
using EntityMetadataBL.Helper;
using EntityMetadataDL;
using EntityMetadataWebAPI.Models.EntityMetadataFacadeModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace EntityMetadataBL
{
    public class EntityConfigBL
    {
        private readonly EntityConfigDL _objEntityConfigDL = null;
        private readonly ExternalSourceBL _objExternalSourceBL = null;

        public EntityConfigBL(EntityConfigDL objEntityConfigDL, ExternalSourceBL objExternalSourceBL)
        {
            _objEntityConfigDL = objEntityConfigDL;
            _objExternalSourceBL = objExternalSourceBL;
        }

        public int SaveEntityConfigs(string jsonData)
        {
            JsonHelper.ValidateJson(jsonData, JsonType.JArray);
            List<EntityMetadataModel> objJsonEntity = JsonConvert.DeserializeObject<List<EntityMetadataModel>>(jsonData);
            List<EntityFieldModel> objEntityFieldList = ParseToEntityFieldModel(objJsonEntity);
            var processedDT = DataTableHelper.ToDataTable<EntityFieldModel>(objEntityFieldList);
            return _objEntityConfigDL.SaveEntityConfig(processedDT);
        }

        /// <summary>
        /// This method fetched fields from two different sources and merge them. Once it's merged then it join with the configuration that's available in the database.
        /// </summary>
        /// <returns>list of EntityMetadataModel</returns>
        public IEnumerable<EntityMetadataModel> GetEntityMetadata()
        {
            DataTable dataTable = _objExternalSourceBL.GetFieldDataTable();
            List<EntityFieldModel> objEntityFieldList = _objEntityConfigDL.GetEntityConfig(dataTable);
            return ParseToEntityMetadataModel(objEntityFieldList);
        }

        /// <summary>
        /// This method parse the EntityFieldModel to EntityMetadataModel
        /// </summary>
        /// <param name="objEntityFieldList"></param>
        /// <returns>list of EntityMetadataModel</returns>
        private List<EntityMetadataModel> ParseToEntityMetadataModel(List<EntityFieldModel> objEntityFieldList)
        {
            List<EntityMetadataModel> objEntityMetadata = new List<EntityMetadataModel>();
            try
            {
                var distinctEntities = objEntityFieldList.Select(x => x.EntityName).Distinct().ToList();

                distinctEntities.ForEach(entity =>
                {
                    List<FieldModel> fieldList = new List<FieldModel>();
                    var entityFieldList = objEntityFieldList.Where(x => x.EntityName == entity).ToList();

                    entityFieldList.ForEach(field =>
                    {
                        fieldList.Add(new FieldModel
                        {
                            FieldName = field.FieldName,
                            IsRequired = field.IsRequired,
                            MaxLength = field.MaxLength,
                            FieldSource = field.FieldSource
                        });
                    });

                    objEntityMetadata.Add(new EntityMetadataModel
                    {
                        EntityName = entity,
                        Fields = fieldList
                    });
                });
            }
            catch(Exception ex)
            {
                throw new HandledException("Internal parsing error check Console log", ex);
            }            

            return objEntityMetadata;
        }

        /// <summary>
        /// This method parse the EntityMetadataModel to EntityFieldModel
        /// </summary>
        /// <param name="objEntityMetadataList"></param>
        /// <returns>list of EntityFieldModel</returns>
        private List<EntityFieldModel> ParseToEntityFieldModel(List<EntityMetadataModel> objEntityMetadataList)
        {
            List<EntityFieldModel> objEntityFieldList = new List<EntityFieldModel>();
            try
            {
                objEntityMetadataList.ForEach(entity => {
                    entity.Fields.ForEach(field =>
                    {
                        objEntityFieldList.Add(new EntityFieldModel
                        {
                            EntityName = entity.EntityName,
                            FieldName = field.FieldName,
                            IsRequired = field.IsRequired,
                            MaxLength = field.MaxLength,
                            FieldSource = field.FieldSource
                        });
                    });
                });
            }
            catch (Exception ex)
            {
                throw new HandledException("Internal parsing error check Console log", ex);
            }
            return objEntityFieldList;
        }

    }
}

