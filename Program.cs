var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapPost("/calculate", (CalculateRequest req) =>
{
    if (req.B == 0 && req.Op == "/")
        return Results.BadRequest(new { error = "Division by zero" });

    double result = req.Op switch
    {
        "+" => req.A + req.B,
        "-" => req.A - req.B,
        "*" => req.A * req.B,
        "/" => req.A / req.B,
        _ => throw new ArgumentException("Invalid operator")
    };

    return Results.Ok(new { result });
});

app.Run();

record CalculateRequest(double A, double B, string Op);
