using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Services;
using Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IFAQService, FAQService>();
            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IReservationService, ReservationService>();
            services.AddScoped<IBlobStorage, BlobService>();
            services.AddScoped<IBlogCategoryService, BlogCategoryService>();
            services.AddScoped<IEmailTableService, EmailTableService>();
            return services;
        }
    }
}
