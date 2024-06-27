using App.Core.ViewModels.Dictionaries;
using App.Data.Entities.Dictionaries;
using AutoMapper;

namespace App.Core.Requests.Handlers.Mapping {

public class DictionaryToViewProfile : Profile
{
    public DictionaryToViewProfile()
    {
        CreateMap<UserType, UserTypeView>();
    }
}
}