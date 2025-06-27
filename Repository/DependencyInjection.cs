using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositoryLayer(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IFAQRepository, FAQRepository>();
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<ISettingRepository, SettingRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<IBlogCategoryRepository, BlogCategoryRepository>();
            services.AddScoped<IEmailRepository, EmailRepository>();
            services.AddScoped<IHotelImageRepository, HotelImageRepository>();
            services.AddScoped<IRoomImageRepository, RoomImageRepository>();
            return services;
        }
    }
}
