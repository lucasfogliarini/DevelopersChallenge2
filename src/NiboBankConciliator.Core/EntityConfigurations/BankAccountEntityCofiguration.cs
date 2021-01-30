using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NiboBankConciliator.Core.Entities;

namespace NiboBankConciliator.Core.EntityConfigurations
{
    internal sealed class BankAccountEntityCofiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
        }
    }
}
