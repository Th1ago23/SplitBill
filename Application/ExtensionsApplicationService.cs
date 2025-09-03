using Application.Interface.Service;
using Application.Service.ContextUser;
using Application.Service;
using Application.Service.JWT;
using Application.Service.Mapper.ExpenseMapper;
using Application.Service.Mapper;
using Application.Service.Mapper.GroupMapper;
using Application.Service.Mapper.UserMapper;
using Application.Utils.Validator;
using Microsoft.Extensions.DependencyInjection;
using Domain.Interface.Repository;
using Infrastructure.Repository;
using Domain.Interface.Token;
using Domain.Interface.Utils;
using Domain.Interface.Database;
using Infrastructure.Database;
using Domain.Interface.Context;
using Application.Interface.Mapper.UserMapper;
using Application.Interface.Mapper.GroupMapper;
using Application.Interface.Mapper.ExpenseMapper;
using Microsoft.AspNetCore.Builder;
using Application.Interface.Utils;


namespace Application
{
    public static class ExtensionsApplicationService
    {
        public static void AddService(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IGroupRepository, GroupRepository>();
            builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
            builder.Services.AddScoped<IUserMP, UserMP>();
            builder.Services.AddScoped<IObjectValidator, ObjectValidator>();
            builder.Services.AddScoped<IGroupMP, GroupMP>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IGroupService, GroupService>();
            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
            builder.Services.AddScoped<IExpenseService, ExpenseService>();
            builder.Services.AddScoped<IExpenseMP, ExpenseMP>();
            builder.Services.AddScoped<GroupGeneralMapper>();
            builder.Services.AddScoped<IInvitationService, InvitationService>();
        }
    }
}
