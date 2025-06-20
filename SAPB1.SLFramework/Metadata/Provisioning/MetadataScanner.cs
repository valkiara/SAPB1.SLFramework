using System.Reflection;
using SAPB1.SLFramework.Abstractions.Attributes;
using SAPB1.SLFramework.Abstractions.Models;

namespace SAPB1.SLFramework.Metadata.Provisioning
{
    /// <summary>
    /// Scans assemblies for UDT, UDF, and SapTable attributes to produce metadata definitions.
    /// </summary>
    public static class MetadataScanner
    {
        /// <summary>
        /// Scans loaded assemblies for classes decorated with UdtAttribute
        /// and returns corresponding UserTablesMD definitions.
        /// </summary>
        public static IEnumerable<UserTablesMD> ScanUserTables(IServiceProvider serviceProvider)
        {
            var tables = new List<UserTablesMD>();

            // Find all types that have [Udt]
            var typesWithUdt = AppDomain.CurrentDomain.GetAssemblies()
                                .SelectMany(a => a.GetTypes())
                                .Where(t => t.GetCustomAttribute<UdtAttribute>() != null);

            foreach (var type in typesWithUdt)
            {
                var udtAttr = type.GetCustomAttribute<UdtAttribute>()!;
                tables.Add(new UserTablesMD
                {
                    TableName = udtAttr.TableName,
                    TableDescription = udtAttr.Description,
                    TableType = udtAttr.TableType,
                    Archivable = udtAttr.Archivable,
                    ArchiveDateField = udtAttr.ArchiveDateField
                });
            }

            return tables;
        }

        /// <summary>
        /// Scans loaded assemblies for properties decorated with UdfAttribute
        /// on types marked with either UdtAttribute (UDT) or SapTableAttribute (system table),
        /// and returns corresponding UserFieldsMD definitions.
        /// </summary>
        public static IEnumerable<UserFieldsMD> ScanUserFields(IServiceProvider serviceProvider)
        {
            var fields = new List<UserFieldsMD>();

            // Grab all types that have either [Udt] or [SapTable]
            var types = AppDomain.CurrentDomain.GetAssemblies()
                             .SelectMany(a => a.GetTypes())
                             .Where(t => t.GetCustomAttribute<UdtAttribute>() != null
                                      || t.GetCustomAttribute<SapTableAttribute>() != null);

            foreach (var type in types)
            {
                // If the class has [Udt], that takes precedence.
                var udtAttr = type.GetCustomAttribute<UdtAttribute>();
                // Otherwise, if it has [SapTable], we use that
                var sapAttr = type.GetCustomAttribute<SapTableAttribute>();

                // Determine which TableName to use:
                string tableName;
                if (udtAttr != null)
                {
                    tableName = $"@{udtAttr.TableName}";
                }
                else
                {
                    // sapAttr is guaranteed non‐null here (by the Where() above)
                    tableName = sapAttr!.TableName;
                }

                // Now find all properties on this type that have [Udf]
                var udfProps = type.GetProperties()
                                   .Where(p => p.GetCustomAttribute<UdfAttribute>() != null);

                foreach (var prop in udfProps)
                {
                    var udfAttr = prop.GetCustomAttribute<UdfAttribute>()!;

                    fields.Add(new UserFieldsMD
                    {
                        // Use the system table name or UDT name from above
                        TableName = tableName,

                        // Copy over all the Udf‐specific metadata
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
                        ValidValuesMD = prop.GetCustomAttributes<ValidValueAttribute>()
                            .Select(v => new ValidValueMD
                            {
                                Value = v.Value,
                                Description = v.Description
                            })
                            .ToArray()
                    });
                }
            }

            return fields;
        }
    }
}
