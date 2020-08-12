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

            services.AddHttpContextAccessor();                  //This is for IHttpContextAcessor injection
            services.AddDistributedMemoryCache();               //This is for distributed memory cache
            services.AddSession(cfg => cfg.IdleTimeout = TimeSpan.FromMinutes(Configuration.GetValue<int>("ApplicationSetting:SessionTimeOutInMinutes")));
            //We are injection IOptions<T> 
            services.AddOptions();
            services.Configure<ApplicationSettings>(Configuration.GetSection("ApplicationSetting"));  
            //We are injection IOptions<T> 
            services.AddDbContext<Data.ApplicationContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Aplication2")));
            services.AddAutoMapper(typeof(Application2.Services.AutoMapperProfile));                  //Automapper and automapper.dependency are required.
            services.AddCors(opt => opt.AddPolicy(name : "_myAllowSpecificOrigins",                   //This is for your api's if you wanna allow that callable from anothoer urls
               builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
            ));
            
            services.AddControllersWithViews()      //Adding Newtonsoft for optional
            .AddNewtonsoftJson(s =>
            {
                s.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            
            services.AddHttpClient();             //Lets inject System.Net.Http.IHttpClientFactory Oh yeah!
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                opt =>
                {
                    opt.LoginPath = "/Account/Login";
                    opt.AccessDeniedPath = "/Account/Login";
                    opt.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    opt.LogoutPath = "/Account/Logout";
                }); 
            services.AddAuthorization();
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
            }
            app.UseStaticFiles();

            app.UseCookiePolicy();
            app.UseSession();

            app.UseRouting();

            app.UseCors("_myAllowSpecificOrigins"); //after Routing, before authentication

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
