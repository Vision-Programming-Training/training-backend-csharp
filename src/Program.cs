using Microsoft.EntityFrameworkCore;
using TrainingBackend.Data;
using TrainingBackend.Middleware;
using TrainingBackend.Repositories;
using TrainingBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// --- CORS ---
// フロント（training-frontend）はこのサーバーと別オリジン（別リポジトリ・別ポート）で動くため、
// ブラウザの同一オリジンポリシーで fetch がブロックされる。
// 研修で受講者がここに詰まらないよう、フロントのオリジンを許可した状態で渡す。
const string FrontendCorsPolicy = "FrontendCorsPolicy";
var allowedOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>() ?? Array.Empty<string>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(FrontendCorsPolicy, policy =>
    {
        policy.WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// --- DB（SQLite） ---
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// --- DI 登録（Repository / Service） ---
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IPricingService, PricingService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddControllers();

// --- Swagger ---
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Training Mini-EC API", Version = "v1" });
});

var app = builder.Build();

// --- DB 作成 + 初期データ投入（dotnet run 一発で seed まで完了させる） ---
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
    SeedData.Initialize(db);
}

// 例外を HTTP ステータスへ変換するミドルウェアを最前段に置く。
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(FrontendCorsPolicy);

app.MapControllers();

app.Run();

// テストプロジェクトから参照できるよう、暗黙の Program クラスを公開する。
public partial class Program { }
