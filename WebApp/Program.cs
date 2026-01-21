var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Middleware #1
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Middleware #1: Before calling next\n\r");

    await next(context);

    await context.Response.WriteAsync("Middleware #1: After calling next\n\r");
});

app.Map("/employees", (appBuilder) =>
{
    //Branch Middleware #5
    appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
    {
        await context.Response.WriteAsync("Middleware #5: Before calling next\n\r");
        await next(context);
        await context.Response.WriteAsync("Middleware #5: After calling next\n\r");
    });

    appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
    {
        await context.Response.WriteAsync("Middleware #6: Before calling next\n\r");
        await next(context);
        await context.Response.WriteAsync("Middleware #6: After calling next\n\r");
    });
});

//Middleware #2
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Middleware #2: Before calling next\n\r");

    //if I comment below line, the control will not go to next middleware and will shortcircuit here.
    await next(context);

    await context.Response.WriteAsync("Middleware #2: After calling next\n\r");
});

//app.Run helps to create Terminal Middleware #2
//app.Run(async (HttpContext context) =>
//{
//    await context.Response.WriteAsync("Terminal Middleware #2: Handling request and not calling next\n\r");
//});


//Middleware #3
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Middleware #3: Before calling next\n\r");

    await next(context);

    await context.Response.WriteAsync("Middleware #3: After calling next\n\r");
});

//Runs the Kestrel server and our webApp and then makes request to above middleware code
app.Run();


/* NOTES
 * Regular middleware and terminal(function) middleware. Middleware shortcircuiting.  
 * Use method to create middleware.
 * app.Run is used to create terminal middleware.
 * app.Map to create branching middleware.
 */