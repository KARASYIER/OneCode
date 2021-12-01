using OneCode.Application.Contracts;
using OneCode.Domain;
using OneCode.Domain.Repositories;
using OneCode.Dtos;
using OneCode.Tags.Dtos;
using OneCode.ToolKit.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneCode.Application
{
    public class TagAppService : OneCodeAppService, ITagAppService
    {
        private TagManager _tagManager;
        private ITagRepository _tagRepository;

        public TagAppService(
            TagManager tagManager,
            ITagRepository tagRepository
            )
        {
            this._tagManager = tagManager;
            this._tagRepository = tagRepository;
        }

        public async Task<ResponseReturn> CreateAsync(CreateOrUpdateTagInputDto input)
        {
            var tag = new Tag(GuidGenerator.Create())
            {
                Name = input.Name
            };

            return ResponseReturn.ReturnSuccess(
                    data: ObjectMapper.Map<Tag, TagDto>(await _tagManager.CreateAsync(tag))
                    );

        }

        public async Task<ResponseReturn> UpdateAsync(Guid id, CreateOrUpdateTagInputDto input)
        {
            var tag = await _tagRepository.GetAsync(id);

            //if (tag == null) return FailedSingleResult<TagDto>("没有查询到相关数据");

            ObjectMapper.Map(input, tag);

            tag = await _tagRepository.UpdateAsync(tag);

            return ResponseReturn.ReturnSuccess(
                    data: ObjectMapper.Map<Tag, TagDto>(tag)
                    );
        }

        public async Task<ResponseReturn> DeleteAsync(Guid id)
        {
            await _tagRepository.DeleteAsync(id);

            return ResponseReturn.ReturnSuccess();
        }

        public async Task<ResponseReturn> GetAsync(Guid id)
        {
            var tag = await _tagRepository.GetAsync(id);

            return ResponseReturn.ReturnSuccess(
                    data: ObjectMapper.Map<Tag, TagDto>(tag)
                );
        }

        public async Task<ResponseReturn> GetListAsync(GetListInputDto input)
        {
            var total = await _tagRepository.GetCountAsync(input.Filter);

            var tags = await _tagRepository.GetListAsync(input.Filter,
                                                         input.PageNo,
                                                         input.PageSize);

            return ResponseReturn.ReturnSuccess(
                     data: new PagedListResultDto<TagDto>
                     {
                         Items = ObjectMapper.Map<List<Tag>, List<TagDto>>(tags),
                         PageNo = input.PageNo,
                         PageSize = input.PageSize,
                         TotalCount = total
                     }
                );
        }
    }
}
