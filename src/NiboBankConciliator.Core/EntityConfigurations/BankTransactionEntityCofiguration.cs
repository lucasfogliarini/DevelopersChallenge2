using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NiboBankConciliator.Core.Entities;

namespace NiboBankConciliator.Core.EntityConfigurations
{
    internal sealed class BankTransactionEntityCofiguration : IEntityTypeConfiguration<BankTransaction>
    {
        public void Configure(EntityTypeBuilder<BankTransaction> builder)
        {
        }
    }
}
