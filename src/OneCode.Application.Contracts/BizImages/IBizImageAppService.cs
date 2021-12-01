using OneCode.ToolKit.Http;
using System.Threading.Tasks;

namespace OneCode.Application.Contracts
{
    public interface IBizImageAppService
    {

        Task<ResponseReturn> CreateAsync();


        //Task<ListResultDto<BizImageDto>> GetListAsync(Guid id);

        
    }
}
