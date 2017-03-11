using AutoMapper;
using LightWS.Models;
using LightWS.Cms.Model;
using Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LightWS
{
    public static class Mapper
    {
        private static IMapper _mapper;
        public static void RegisterMappings()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
              

                #region SystemKey
                cfg.CreateMap<SystemKey, SystemKeyViewModel>()
                                .ForMember(dto => dto.Key, opt => opt.MapFrom(src => src.Id))
                                .ForMember(dto => dto.Key, opt => opt.MapFrom(src => src.Key))
                                .ForMember(dto => dto.Value, opt => opt.MapFrom(src => src.Value));

                cfg.CreateMap<SystemKeyViewModel, SystemKey>()
                                .ForMember(dto => dto.Id, opt => opt.MapFrom(src => src.Id))
                                .ForMember(dto => dto.Key, opt => opt.MapFrom(src => src.Key))
                                .ForMember(dto => dto.Value, opt => opt.MapFrom(src => src.Value));

                #endregion

            });
            _mapper = mapperConfiguration.CreateMapper();
        }

      
        #region System key
        public static SystemKeyViewModel MapFrom(SystemKey data)
        {
            return _mapper.Map<SystemKey, SystemKeyViewModel>(data);
        }
        public static SystemKey MapFrom(SystemKeyViewModel data)
        {
            return _mapper.Map<SystemKeyViewModel, SystemKey>(data);
        }
        public static SystemKey MapFrom(SystemKeyViewModel data_view, SystemKey data_entity)
        {
            return _mapper.Map(data_view, data_entity);
        }
        #endregion
    }
}