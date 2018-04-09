using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStore.Services
{
    /// <summary>
    /// Based on AutoMapper: https://github.com/AutoMapper/AutoMapper/wiki/Getting-started
    /// See above link if help is needed
    /// </summary>
    public class MessageTypeConverter
    {
        private static MessageTypeConverter sMessageTypeConverter = new MessageTypeConverter();

        public static MessageTypeConverter Instance
        {
            get
            {
                return sMessageTypeConverter;
            }
        }



        public MessageTypeConverter()
        {
            InitializeExternalToInternalMappings();
            InitializeInternalToExternalMappings();
        }

        private void InitializeInternalToExternalMappings()
        {
            AutoMapper.Mapper.CreateMap<VideoStore.Business.Entities.Media,
                VideoStore.Services.MessageTypes.Media>().ForMember(
                    dest => dest.StockCount, opts => opts.MapFrom( src => src.Stocks.Quantity));

            AutoMapper.Mapper.CreateMap<VideoStore.Business.Entities.Order,
                VideoStore.Services.MessageTypes.Order>();

            AutoMapper.Mapper.CreateMap<VideoStore.Business.Entities.OrderItem,
                VideoStore.Services.MessageTypes.OrderItem>();

            AutoMapper.Mapper.CreateMap<VideoStore.Business.Entities.User,
                 VideoStore.Services.MessageTypes.User>();

            AutoMapper.Mapper.CreateMap<VideoStore.Business.Entities.LoginCredential,
                VideoStore.Services.MessageTypes.LoginCredential>();
        }

        public void InitializeExternalToInternalMappings()
        {
            AutoMapper.Mapper.CreateMap<VideoStore.Services.MessageTypes.Media,
                VideoStore.Business.Entities.Media>();

            AutoMapper.Mapper.CreateMap<VideoStore.Services.MessageTypes.Order,
               VideoStore.Business.Entities.Order>();

            AutoMapper.Mapper.CreateMap<VideoStore.Services.MessageTypes.OrderItem,
                VideoStore.Business.Entities.OrderItem>();

            AutoMapper.Mapper.CreateMap<VideoStore.Services.MessageTypes.User,
                 VideoStore.Business.Entities.User>();

            AutoMapper.Mapper.CreateMap<VideoStore.Services.MessageTypes.LoginCredential,
                VideoStore.Business.Entities.LoginCredential>();
        }

        public Destination Convert<Source, Destination>(Source s)
        {
            var result = AutoMapper.Mapper.Map<Source, Destination>(s);
            return result;
        }
    }
}
