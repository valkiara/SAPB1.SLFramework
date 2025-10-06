using System.Reflection;
using SAPB1.SLFramework.Abstractions.Attributes;
using SAPB1.SLFramework.Abstractions.Models;

namespace SAPB1.SLFramework.Metadata.Provisioning
{
    public static class MetadataScanner
    {
        /// <summary>
        /// Optional: limit scanning to assemblies that match this predicate.
        /// Example: asm => asm.GetName().Name!.StartsWith("SAPB1.", StringComparison.Ordinal)
        /// or      asm => asm == typeof(SomeKnownType).Assembly
        /// </summary>
        public static Func<Assembly, bool>? AssemblyFilter { get; set; }

        /// <summary>
        /// Plug in your logger if you want diagnostics from LoaderExceptions.
        /// </summary>
        public static Action<string>? Log { get; set; }

        public static IEnumerable<UserTablesMD> ScanUserTables(IServiceProvider _)
        {
            var tables = new List<UserTablesMD>();

            foreach (var type in GetCandidateTypes(t =>
                    t.GetCustomAttribute<UdtAttribute>(inherit: true) != null))
            {
                var udtAttr = type.GetCustomAttribute<UdtAttribute>(inherit: true)!;

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

        public static IEnumerable<UserFieldsMD> ScanUserFields(IServiceProvider _)
        {
            var fields = new List<UserFieldsMD>();

            foreach (var type in GetCandidateTypes(t =>
                    t.GetCustomAttribute<UdtAttribute>(inherit: true) != null ||
                    t.GetCustomAttribute<SapTableAttribute>(inherit: true) != null))
            {
                var udtAttr = type.GetCustomAttribute<UdtAttribute>(inherit: true);
                var sapAttr = type.GetCustomAttribute<SapTableAttribute>(inherit: true);

                var tableName = udtAttr != null ? $"@{udtAttr.TableName}" : sapAttr!.TableName;

                var props = SafeGetProperties(type);
                foreach (var prop in props)
                {
                    var udfAttr = prop.GetCustomAttribute<UdfAttribute>(inherit: true);
                    if (udfAttr is null) continue;

                    var validValues = prop.GetCustomAttributes<ValidValueAttribute>(inherit: true)
                        .Select(v => new ValidValueMD { Value = v.Value, Description = v.Description })
                        .ToArray();

                    fields.Add(new UserFieldsMD
                    {
                        TableName = tableName,
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
                        ValidValuesMD = validValues
                    });
                }
            }

            return fields;
        }

        // ---------- helpers ----------

        private static IEnumerable<Type> GetCandidateTypes(Func<Type, bool> predicate)
        {
            var asms = AppDomain.CurrentDomain.GetAssemblies()
                          .Where(a => (AssemblyFilter == null) || AssemblyFilter(a));

            foreach (var asm in asms)
            {
                Type[] types = SafeGetTypes(asm);
                foreach (var t in types)
                {
                    if (t is null) continue;
                    if (t.IsAbstract || t.IsGenericTypeDefinition) continue;

                    if (predicate(t))
                        yield return t;
                }
            }
        }

        private static Type[] SafeGetTypes(Assembly asm)
        {
            try
            {
                return asm.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                // Log each loader exception once for diagnostics
                if (Log != null)
                {
                    Log($"[MetadataScanner] Partial type load from '{asm.FullName}'. " +
                        $"Loaded: {ex.Types?.Count(t => t != null) ?? 0}. " +
                        $"Errors: {ex.LoaderExceptions?.Length ?? 0}.");

                    if (ex.LoaderExceptions != null)
                    {
                        foreach (var le in ex.LoaderExceptions)
                            Log($"  - {le.GetType().Name}: {le.Message}");
                    }
                }

                // Return the types that did load
                return ex.Types?.Where(t => t != null).ToArray() ?? Array.Empty<Type>();
            }
            catch (Exception ex)
            {
                Log?.Invoke($"[MetadataScanner] Failed to read types from '{asm.FullName}': {ex.Message}");
                return Array.Empty<Type>();
            }
        }

        private static IEnumerable<PropertyInfo> SafeGetProperties(Type t)
        {
            try
            {
                // DeclaredOnly avoids inherited duplicates; add Instance/Public as needed
                return t.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            }
            catch (Exception ex)
            {
                Log?.Invoke($"[MetadataScanner] Failed to read properties of '{t.FullName}': {ex.Message}");
                return Array.Empty<PropertyInfo>();
            }
        }
    }
}
