using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Threading.Tasks;

namespace YellowTeaming.Client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // Cookie configuration for HTTP to support cookies with SameSite=None
            // services.ConfigureSameSiteNoneCookies();

            //Cookie configuration for HTTPS

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.None;

            });

            // Add authentication services
            services.AddAuthentication(options =>
             {
                 options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                 options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                 options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;

             })
             .AddCookie()
             .AddOpenIdConnect("Auth0", options =>
             {
                 // Set the authority to your Auth0 domain
                 options.Authority = "https://yellow-teaming.eu.auth0.com";

                 // Configure the Auth0 Client ID and Client Secret
                 options.ClientId = "Sz7cxjJsDVbaNtrihydcOoSVmQytklm3";
                 options.ClientSecret = "gv8SCIXOYbqHponx0Jv1DxkxZwukJzInV736PijPxb1yzbXMO4RSWGtfmWmbF1-B";

                 // Set response type to code
                 options.ResponseType = OpenIdConnectResponseType.Code;

                 // Configure the scope
                 options.Scope.Clear();
                 options.Scope.Add("openid");
                 options.Scope.Add("profile");
                 options.Scope.Add("email");


                 options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                 {
                     NameClaimType = "name"

                 };

                 // Set the callback path, so Auth0 will call back to http://localhost:3000/callback
                 // Also ensure that you have added the URL as an Allowed Callback URL in your Auth0 dashboard
                 options.CallbackPath = new PathString("/callback");

                 // Configure the Claims Issuer to be Auth0
                 options.ClaimsIssuer = "Auth0";

                 options.SaveTokens = true;

                 options.Events = new OpenIdConnectEvents
                 {
                     // handle the logout redirection
                     OnRedirectToIdentityProviderForSignOut = (context) =>
                      {
                          var logoutUri = $"https://yellow-teaming.eu.auth0.com/v2/logout?client_id=Sz7cxjJsDVbaNtrihydcOoSVmQytklm3";

                          var postLogoutUri = context.Properties.RedirectUri;
                          if (!string.IsNullOrEmpty(postLogoutUri))
                          {
                              if (postLogoutUri.StartsWith("/"))
                              {
                                  // transform to absolute
                                  var request = context.Request;
                                  postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
                              }
                              logoutUri += $"&returnTo={ Uri.EscapeDataString(postLogoutUri)}";
                          }

                          context.Response.Redirect(logoutUri);
                          context.HandleResponse();

                          return Task.CompletedTask;
                      },
                     OnRedirectToIdentityProvider = context =>
                     {
                         // The context's ProtocolMessage can be used to pass along additional query parameters
                         // to Auth0's /authorize endpoint.
                         // 
                         // Set the audience query parameter to the API identifier to ensure the returned Access Tokens can be used
                         // to call protected endpoints on the corresponding API.
                         context.ProtocolMessage.SetParameter("audience", "https://localhost:44350/api");

                         return Task.FromResult(0);
                     }
                 };
             });





            services.AddControllersWithViews();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
