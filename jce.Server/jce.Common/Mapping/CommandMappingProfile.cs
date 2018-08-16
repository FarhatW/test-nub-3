using System;
using System.Linq;
using AutoMapper;
using jce.Common.Entites;
using jce.Common.Extentions;
using jce.Common.Resources;
using jce.Common.Resources.Command;
using jce.Common.Resources.UserProfile;

namespace jce.Common.Mapping
{
    class CommandMappingProfile : Profile
    {
        public CommandMappingProfile()
        {
            //Domaine to API Resource
            CreateMap<Command, CommandResource>().ForMember(cr => cr.CommandChildProduct, 
                 opt => opt.MapFrom(c => 
                 c.CommandChildProduct.Select(ccp => new CommandChildProductResource()
                 {
                     ChildId = ccp.ChildId,
                     ProductId = ccp.ProductId,
                     CommandId = ccp.CommandId,
                     OvertakeCommandChild = ccp.OvertakeCommandChild
                 })));

            //API Resource to Domaine
            CreateMap<CommandResource, Command>()
           .ForMember(c => c.Id, opt => opt.Ignore());


            CreateMap<CommandSaveResource, Command>()
            .ForMember(c => c.Id, opt => opt.Ignore())
            .ForMember(c => c.CommandChildProduct, opt => opt.Ignore())
            .AfterMap((cr, c) =>
            {
                var removedCommandResource = c.CommandChildProduct.Where(ccp => !cr.CommandChildProduct.Contains(ccp)).ToList();
                foreach (var item in removedCommandResource)
                {
                    c.CommandChildProduct.Remove(item);
                }

                var addedCommandResource = cr.CommandChildProduct.Where(ccp => !c.CommandChildProduct.Any(cp => cp.CommandId == ccp.CommandId)).Select(ccp => new CommandChildProduct { CommandId = ccp.CommandId, ChildId = ccp.ChildId, ProductId = ccp.ProductId, OvertakeCommandChild = ccp.OvertakeCommandChild }).ToList();
                foreach (var item in addedCommandResource)
                {
                    c.CommandChildProduct.Add(item);
                }
            });
        }
    }
}
