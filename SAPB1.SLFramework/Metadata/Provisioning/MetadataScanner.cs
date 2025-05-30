using System.Reflection;
using SAPB1.SLFramework.Abstractions.Attributes;
using SAPB1.SLFramework.Abstractions.Models;

namespace SAPB1.SLFramework.Metadata
{
    /// <summary>
    /// Scans assemblies for UDT and UDF attributes to produce metadata definitions.
    /// </summary>
    public static class MetadataScanner
    {
        /// <summary>
        /// Scans loaded assemblies for classes decorated with UdtAttribute
        /// and returns corresponding UserTableMD definitions.
        /// </summary>
        public static IEnumerable<UserTablesMD> ScanUserTables(IServiceProvider serviceProvider)
        {
            var tables = new List<UserTablesMD>();
            var types = AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(a => a.GetTypes())
                            .Where(t => t.GetCustomAttribute<UdtAttribute>() != null);

            foreach (var type in types)
            {
                var attr = type.GetCustomAttribute<UdtAttribute>();
                tables.Add(new UserTablesMD
                {
                    TableName = attr.TableName,
                    TableDescription = attr.Description,
                    TableType = attr.TableType,
                    Archivable = attr.Archivable,
                    ArchiveDateField = attr.ArchiveDateField
                });
            }
            return tables;
        }

        /// <summary>
        /// Scans loaded assemblies for properties decorated with UdfAttribute
        /// and returns corresponding UserFieldMD definitions.
        /// </summary>
        public static IEnumerable<UserFieldMD> ScanUserFields(IServiceProvider serviceProvider)
        {
            var fields = new List<UserFieldMD>();
            var types = AppDomain.CurrentDomain.GetAssemblies()
                            .SelectMany(a => a.GetTypes())
                            .Where(t => t.GetCustomAttribute<UdtAttribute>() != null);

            foreach (var type in types)
            {
                var udtAttr = type.GetCustomAttribute<UdtAttribute>();
                var props = type.GetProperties()
                                .Where(p => p.GetCustomAttribute<UdfAttribute>() != null);

                foreach (var prop in props)
                {
                    var udfAttr = prop.GetCustomAttribute<UdfAttribute>();
                    fields.Add(new UserFieldMD
                    {
                        TableName = udtAttr.TableName,
                        FieldID = udfAttr.FieldID,
                        Name = udfAttr.Name,
                        Type = udfAttr.Type,
                        Size = udfAttr.Size,
                        Description = udfAttr.Description,
                        SubType = udfAttr.SubType,
                        Mandatory = udfAttr.Mandatory,
                        LinkedTable = udfAttr.LinkedTable,
                        DefaultValue = udfAttr.DefaultValue,
                        EditSize = udfAttr.EditSize,
                        LinkedUDO = udfAttr.LinkedUDO,
                        LinkedSystemObject = udfAttr.LinkedSystemObject,
                        ValidValuesMD = Array.Empty<ValidValueMD>()
                    });
                }
            }
            return fields;
        }
    }
}