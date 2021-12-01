using OneCode.Domain;
using OneCode.Domain.Repositories;
using OneCode.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace OneCode.EntityFrameworkCore.Repositories
{
    public class CommisionRecordRepository : EfCoreRepository<OneCodeDbContext, CommisionRecord, Guid>, ICommisionRecordRepository
    {
        public CommisionRecordRepository(IDbContextProvider<OneCodeDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
