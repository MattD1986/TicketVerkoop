using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project_FullStack.Areas.Identity.Data;
using TicketVerkoop.Data;
using TicketVerkoop.Domain.Entities;
using TicketVerkoop.Repository;
using TicketVerkoop.Repository.Interfaces;
using TicketVerkoop.Service;
using TicketVerkoop.Service.Interfaces;
using TicketVerkoop.Util.Mail;
using static TicketVerkoop.Util.Mail.EmailSender;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddTransient<IService<Club>, ClubService>();
builder.Services.AddTransient<IDAO<Club>, clubDAO>();
builder.Services.AddTransient<IService<Wedstrijd>, WedstrijdService>();
builder.Services.AddTransient<IDAO<Wedstrijd>, WedstrijdDAO>();
builder.Services.AddTransient<IService<Vak>, VakService>();
builder.Services.AddTransient<IDAO<Vak>, VakDAO>();
builder.Services.AddTransient<IService<VakOmschrijving>, VakOmschrijvingService>();
builder.Services.AddTransient<IDAO<VakOmschrijving>, VakOmschrijvingDAO>();
builder.Services.AddTransient<IService<Stadion>, StadionService>();
builder.Services.AddTransient<IDAO<Stadion>, StadionDAO>();
builder.Services.AddTransient<IService<Competitie>, CompetitieService>();
builder.Services.AddTransient<IDAO<Competitie>, CompetitieDAO>();
builder.Services.AddTransient<IService<Order>, OrderService>();
builder.Services.AddTransient<IDAO<Order>, orderDAO>();
builder.Services.AddTransient<IService<Ticket>, TicketService>();
builder.Services.AddTransient<IDAO<Ticket>, TicketDAO>();
builder.Services.AddTransient<IService<Abonnement>, AbonnementService>();
builder.Services.AddTransient<IDAO<Abonnement>, AbonnementDAO>();
builder.Services.AddTransient<IService<Plaat>, PlaatsService>();
builder.Services.AddTransient<IDAO<Plaat>, PlaatsDAO>();

builder.Services.AddControllersWithViews();

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddSingleton<IEmailSend, EmailSend>();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "be.VIVES.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(10);
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
