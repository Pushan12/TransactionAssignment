using Microsoft.EntityFrameworkCore;
using TransactionAssignment.Data;
using TransactionAssignment.Helper;
using TransactionAssignment.Services;

namespace TransactionAssignment
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<TxnDbContext>(opt => opt.UseInMemoryDatabase("Transaction"),ServiceLifetime.Scoped);
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<ITxnService, TxnService>();
            builder.Services.AddScoped<IFileProcesserFactory,FileProcesserFactory>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}