using Core.Interfaces;
using Core.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services;
using Services.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args); 

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMemoryCache(); // bộ nhớ đệm, có rồi thì lấy ra ( trong ram ), chưa có thì lấy db
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.Configure<FormOptions>(options =>
{
    options.MemoryBufferThreshold = Int32.MaxValue;
});

builder.Services.AddDbContext<E_ShopContext>(options =>
{
        options.UseSqlServer(builder.Configuration.GetConnectionString("cnnStr"));
});

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);


builder.Services.AddAuthentication(x =>
{
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("veryverysceret.....123123123123qweqweqweqwe123123123qweqweqweqwewqeqwe123123213")),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>(); // DI
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IBrandRepository, BrandRepository>();
builder.Services.AddTransient<IBrandService, BrandService>();
builder.Services.AddTransient<IImageRepository, ImageRepository>();
builder.Services.AddTransient<IImageService, ImageService>();

builder.Services.AddTransient(typeof(ICustomerRepository), typeof(CustomerRepository));
builder.Services.AddTransient(typeof(ICustomerService), typeof(CustomerService));
builder.Services.AddTransient(typeof(IOrderRepository), typeof(OrderRepository));
builder.Services.AddTransient(typeof(IOrderService), typeof(OrderService));
builder.Services.AddTransient(typeof(IOrderDetailRepository), typeof(OrderDetailReposiotry));
builder.Services.AddTransient(typeof(IOrderDetailService), typeof(OrderDetailService));

builder.Services.AddEndpointsApiExplorer(); // api
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // file import
app.UseRouting();
app.UseCors("corsapp"); // cors

app.UseAuthentication(); //
app.UseAuthorization(); //

app.MapControllers();

app.Run();
