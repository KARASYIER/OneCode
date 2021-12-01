using OneCode.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace OneCode.Domain
{
    public class TagManager : IDomainService
    {

        private ITagRepository _tagRepository;

        public TagManager(ITagRepository tagRepository)
        {
            this._tagRepository = tagRepository;
        }

        public async Task<Tag> CreateAsync(Tag tag)
        {
            await CheckSameNameAsync(tag.Name);

            return await _tagRepository.InsertAsync(tag);
        }

        protected async Task CheckSameNameAsync(string tagName)
        {
            var tag = await _tagRepository.GetByNameAsync(tagName);

            if (tag != null)
            {
                throw new OneCodeBizException("该标签名已存在");
            }

        }
    }
}
