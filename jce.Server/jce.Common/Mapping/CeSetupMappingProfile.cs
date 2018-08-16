using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using jce.Common.Entites;
using jce.Common.Resources.CeSetup;
using jce.Common.Resources.CE;
using System.Linq;

namespace jce.Common.Mapping
{
    public class CeSetupMappingProfile : Profile
    {

        public CeSetupMappingProfile()
        {
            //Domaine to API Resource

//            CreateMap<CeSetup, CeSetupResource>().ForMember(cesr => cesr,
//                    opt => opt.MapFrom(ces =>
//                        ces.Mail.Select(i => new MailInCeSetup()
//                        {
//                            MailBody = i.MailBody,
//                            MailObject = i.MailObject,
//                            MailType = i.MailType
//                        })));

            CreateMap<CeSetup, CeSetupSaveResource>().ForMember(s => s.Id, opt => opt.Ignore()); ;

            //API Resource to Domaine
            CreateMap<CeSetupSaveResource, CeSetup>()
                .ForMember(s => s.Id, opt => opt.Ignore())
                    .ForMember(s => s.Mail, opt => opt.Ignore())
                    .AfterMap((cesr, ces) =>
                    {
                        var removedMailCe = ces.Mail.Where(mc => !cesr.MailCe.Any(cm => cm.CeSetupId == mc.CeSetupId)).ToList();
                        foreach (var item in removedMailCe)
                        {
                            ces.Mail.Remove(item);
                        }

                        var addedMailCe = cesr.MailCe.Where(cm => !ces.Mail.Any(mc => mc.Id == cm.Id)).Select(mc => new Mail { CeSetupId = mc.CeSetupId, Id = mc.Id}).ToList();
                        foreach (var item in addedMailCe)
                        {
                            ces.Mail.Add(item);
                        }
                    });

        }


    }
}
