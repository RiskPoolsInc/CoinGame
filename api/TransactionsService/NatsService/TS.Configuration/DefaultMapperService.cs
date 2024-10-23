using AutoMapper;
using SilentNotary.Common.Mapping;

namespace TS.Configuration
{
    public class DefaultMapperService<TDest, TDto> : IMapperService<TDest, TDto>
    {
        private readonly IMapper _mapper;

        public DefaultMapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDest GetFrom(TDto model, object mappingData = null)
        {
            return _mapper.Map<TDest>(model);
        }
    }
}