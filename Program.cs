using Car_rental.DataBase;
using Car_rental.IRepository;
using Car_rental.Repository;

namespace Car_rental
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.UseWebRoot("wwwroot");
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var connectionString = builder.Configuration.GetConnectionString("DBConnection");
            builder.Services.AddSingleton<IManagerRepository>(provider => new ManagerRepository(connectionString));
            builder.Services.AddSingleton<ICustomerRepository>(provider => new CustomerRepository(connectionString));
            builder.Services.AddSingleton<IBookingRepository>(provider => new BookingRepository(connectionString));
            builder.Services.AddSingleton<IBookingPaymentRepository>(provider => new BookingPaymentRepository(connectionString));
            builder.Services.AddSingleton<IRentalDetailRepository>(provider => new RentalDetailRepository(connectionString));
            builder.Services.AddSingleton<IReturnDetailRepository>(provider => new ReturnDetailRepository(connectionString));



            //Initialize The Database
            var Initialize = new DatabaseInitializer(connectionString: connectionString);
            Initialize.Initialize();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}
