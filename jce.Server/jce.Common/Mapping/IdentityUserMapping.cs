using System.Linq;
using AutoMapper;
using jce.Common.Entites.IdentityServerDbContext;
using jce.Common.Extentions;
using jce.Common.Resources;
using jce.Common.Resources.Consent;
using jce.Common.Resources.user;
using jce.Common.Resources.userIdentity;

namespace jce.Common.Mapping
{
    public class IdentityUserMapping : Profile
    {
        public IdentityUserMapping()
        {
            CreateMap<Consent, ConsentResource>()
                .ForMember(cn => cn.AllowRememberConsent, opt => opt.MapFrom(vr => vr.Client.AllowRememberConsent))
                .ForMember(cn => cn.ClientLogoUrl, opt => opt.MapFrom(vr => vr.Client.LogoUri))
                .ForMember(cn => cn.ClientName, opt => opt.MapFrom(vr => vr.Client.ClientName))
                .ForMember(cn => cn.ClientUrl, opt => opt.MapFrom(vr => vr.Client.ClientUri))

                .ForMember(cn => cn.IdentityScopes,
                    opt => opt.MapFrom(vr => vr.Resource.IdentityResources.Select(x => new ScopeResource
                    {
                        Name = x.Name,
                        DisplayName = x.DisplayName,
                        Description = x.Description,
                        Emphasize = x.Emphasize,
                        Required = x.Required,
                        Checked = x.Required
                    }).ToList()))

                .ForMember(cn => cn.ResourceScopes,
                    opt => opt.MapFrom(vr => vr.Resource.ApiResources.SelectMany(s => s.Scopes).Select(x => new ScopeResource
                    {
                        Name = x.Name,
                        DisplayName = x.DisplayName,
                        Description = x.Description,
                        Required = x.Required,
                        Emphasize = x.Emphasize,
                        Checked = x.Required
                    }).ToList()));

            CreateMap<RoleResource, Role>()
                .ForMember(u => u.Id, opt => opt.Ignore());

            CreateMap<Role, RoleResource>();

            CreateMap<User, UserResource>()
                .ForMember(vr => vr.Roles, opt => opt.MapFrom(v => v.UserRoles.Select(vf => new Role
                {
                    Id = vf.RoleId,
                    Name = vf.Role.Name,
                    CreatedBy = vf.Role.CreatedBy,
                    CreatedOn = vf.Role.CreatedOn,
                    UpdatedBy = vf.Role.UpdatedBy,
                    UpdatedOn = vf.Role.UpdatedOn,
                    Enable = vf.Role.Enable,
                    Rank = vf.Role.Rank
                })));
            CreateMap<User, SaveUserResource>()
                .ForMember(vr => vr.Roles, opt => opt.MapFrom(v => v.UserRoles.Select(vf => vf.RoleId)));
            CreateMap<User, UpdateUserResource>()
                .ForMember(vr => vr.Roles, opt => opt.MapFrom(v => v.UserRoles.Select(vf => vf.RoleId)));
            CreateMap<UserResource, User>();

            CreateMap<UpdateUserResource, User>()

                .ForMember(u => u.Id, opt => opt.Ignore())
                .ForMember(v => v.UserRoles, opt => opt.Ignore())
                .AfterMap((ur, u) => {
                    var removedRoles = u.UserRoles.Where(f => !ur.Roles.Contains(f.RoleId)).ToList();
                    foreach (var f in removedRoles)
                    {
                        u.UserRoles.Remove(f);
                    }
                    //                    cr.Products.Where(cp => !c.Products.Any(pr => pr.ProductId == cp.ProductId))
                    var addedFeatures = ur.Roles.Where(id => u.UserRoles.All(f => f.RoleId != id)).Select(id => new UserRole { RoleId = id, UserId = ur.Id }).ToList();
                    foreach (var f in addedFeatures)
                    {
                        u.UserRoles.Add(f);
                    }

                });
            CreateMap<SaveUserResource, User>()
                .ForMember(u => u.Id, opt => opt.Ignore())
                .ForMember(v => v.UserRoles, opt => opt.Ignore())
                .AfterMap((ur, u) => {
                    var removedRoles = u.UserRoles.Where(f => !ur.Roles.Contains(f.RoleId)).ToList();
                    foreach (var f in removedRoles)
                    {
                        u.UserRoles.Remove(f);
                    }
                    //                    cr.Products.Where(cp => !c.Products.Any(pr => pr.ProductId == cp.ProductId))
                    var addedFeatures = ur.Roles.Where(id => u.UserRoles.All(f => f.RoleId != id)).Select(id => new UserRole { RoleId = id, UserId = ur.Id }).ToList();
                    foreach (var f in addedFeatures)
                    {
                        u.UserRoles.Add(f);
                    }

                });
           

           

            CreateMap(typeof(QueryResult<>), typeof(UserQueryResource));


        }
    }
}
