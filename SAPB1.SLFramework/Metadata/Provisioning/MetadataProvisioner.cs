﻿using SAPB1.SLFramework.Abstractions.Interfaces;
using SAPB1.SLFramework.Abstractions.Models;

namespace SAPB1.SLFramework.Metadata.Provisioning
{
    /// <summary>
    /// Ensures that User Defined Tables (UDTs) and User Defined Fields (UDFs)
    /// are created or updated in SAP B1 based on scanned metadata definitions.
    /// </summary>
    public class MetadataProvisioner : IMetadataProvisioner
    {
        private readonly IServiceLayerRepository<UserTablesMD> _tableRepo;
        private readonly IServiceLayerRepository<UserFieldsMD> _fieldRepo;
        private readonly IEnumerable<UserTablesMD> _tables;
        private readonly IEnumerable<UserFieldsMD> _fields;
        public MetadataProvisioner(
            IServiceLayerRepository<UserTablesMD> tableRepo,
            IServiceLayerRepository<UserFieldsMD> fieldRepo,
            IEnumerable<UserTablesMD> tables,
            IEnumerable<UserFieldsMD> fields)
        {
            _tableRepo = tableRepo ?? throw new ArgumentNullException(nameof(tableRepo));
            _fieldRepo = fieldRepo ?? throw new ArgumentNullException(nameof(fieldRepo));
            _tables = tables ?? throw new ArgumentNullException(nameof(tables));
            _fields = fields ?? throw new ArgumentNullException(nameof(fields));
        }

        /// <summary>
        /// Creates or updates UDTs and UDFs in SAP B1.
        /// </summary>
        public async Task EnsureAsync(CancellationToken cancellationToken = default)
        {
            // Provision UDTs first
            foreach (var udt in _tables)
            {
                // use primary key of UDT is TableName
                var exists = await _tableRepo.ExistsAsync(x => x.TableName == udt.TableName);
                if (!exists)
                {
                    await _tableRepo.AddAsync(udt);
                }
                else
                {
                    await _tableRepo.UpdateAsync(udt.TableName, udt);
                }
            }

            // Provision UDFs next
            foreach (var udf in _fields)
            {
                var exists = await _fieldRepo.ExistsAsync(x => x.TableName == udf.TableName && x.Name == udf.Name);
                if (!exists)
                {
                    await _fieldRepo.AddAsync(udf);
                }
                else
                {
                    var existingField = await _fieldRepo.FirstAsync(x => x.TableName == udf.TableName && x.Name == udf.Name);
                    string fieldKey = $"TableName='{existingField.TableName}',FieldID={existingField.FieldID}";
                    await _fieldRepo.UpdateAsync(fieldKey, udf);
                }
            }
        }
    }
}
