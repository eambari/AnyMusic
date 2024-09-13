using Microsoft.EntityFrameworkCore;
using AnyMusic.Domain.Identity;
using AnyMusic.Repository;
using AnyMusic.Repository.Implementation;
using AnyMusic.Repository.Interface;
using AnyMusic.Service.Interface;
using AnyMusic.Service.Implementation;
using AnyMusic.Domain.Domain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<AnyMusicUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();



builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
builder.Services.AddScoped(typeof(IAlbumRepository), typeof(AlbumRepository));
builder.Services.AddScoped(typeof(IPlaylistRepository), typeof(PlaylistRepository));
builder.Services.AddScoped(typeof(ITrackRepository), typeof(TrackRepository));
builder.Services.AddScoped(typeof(ITrackInUserPlaylistRepository), typeof(TrackInUserPlaylistRepository));


builder.Services.AddTransient<IArtistService, ArtistService>();
builder.Services.AddTransient<IAlbumService, AlbumService>();
builder.Services.AddTransient<IPlaylistService, PlaylistService>();
builder.Services.AddTransient<ITrackService, TrackService>();
builder.Services.AddTransient<ITrackInPlaylistService, TrackInPlaylistService>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
