using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using talenthubBE.Data;
using talenthubBE.Data.Repositories;
using talenthubBE.Data.Repositories.Organizations;
using talenthubBE.Data.Repositories.Users;
using talenthubBE.Security;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MvcDataContext>(options =>
    options.UseNpgsql(builder.Configuration["ConnectionString"] ?? throw new InvalidOperationException("Connection string 'MvcDataContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = "API",
                Version = "v1",
            });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Implicit = new OpenApiOAuthFlow
            {
                Scopes = new Dictionary<string, string>
                {
                    {"create:admin", "Create an Admin"},
                    {"create:users", "Create a User"},
                    {"edit:developers", "Edit Developers"},
                    {"edit:jobs", "Edit Jobs"},
                    {"edit:skills", "Edit Skills"},
                    {"update:user", "Update User"},
                },
                AuthorizationUrl = new Uri(builder.Configuration["Auth0:Domain"] + "authorize?audience=" + builder.Configuration["Auth0:Audience"]),
            }
        }
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Auth0:Domain"];
        options.Audience = builder.Configuration["Auth0:Audience"];
    }
);
builder.Services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
builder.Services.AddAuthorization(options => 
{
    options.AddPolicy(
        "create:admin", 
        policy => policy.Requirements.Add(
            new HasScopeRequirement("create:admin", builder.Configuration["Auth0:Domain"]!
        )
    ));
    options.AddPolicy(
        "create:users", 
        policy => policy.Requirements.Add(
            new HasScopeRequirement("create:admin", builder.Configuration["Auth0:Domain"]!
        )
    ));
    options.AddPolicy(
        "edit:developers", 
        policy => policy.Requirements.Add(
            new HasScopeRequirement("create:admin", builder.Configuration["Auth0:Domain"]!
        )
    ));
    options.AddPolicy(
        "edit:jobs", 
        policy => policy.Requirements.Add(
            new HasScopeRequirement("create:admin", builder.Configuration["Auth0:Domain"]!
        )
    ));
    options.AddPolicy(
        "edit:skills", 
        policy => policy.Requirements.Add(
            new HasScopeRequirement("create:admin", builder.Configuration["Auth0:Domain"]!
        )
    ));
    options.AddPolicy(
        "update:user", 
        policy => policy.Requirements.Add(
            new HasScopeRequirement("create:admin", builder.Configuration["Auth0:Domain"]!
        )
    ));
});

builder.Services.AddScoped<IJobsRepository, JobsRepository>();
builder.Services.AddScoped<ISkillsRepository, SkillsRepository>();
builder.Services.AddScoped<IDevelopersRepository, DevelopersRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrganizationRepository, OrganizationsRepository>();

// CORS
// var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy(name: MyAllowSpecificOrigins,
//                       policy  =>
//                       {
//                           policy.WithOrigins(builder.Configuration["Policy_url"]!)
//                             .AllowAnyHeader()
//                             .AllowAnyMethod();
//                       });
// });

var app = builder.Build();

// CORS
// app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
    c.OAuthClientId(builder.Configuration["Auth0:ClientId"]);
});
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();

public partial class Program {};