using WebApp.MiddleComponents;

var builder = WebApplication.CreateBuilder(args);

//Registering custom middleware as a service
builder.Services.AddTransient<MyCustomMiddleware>(); 

var app = builder.Build();

//Middleware #1
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Middleware #1: Before calling next\n\r");

    await next(context);

    await context.Response.WriteAsync("Middleware #1: After calling next\n\r");
});


app.UseMiddleware<MyCustomMiddleware>();

//app.UseWhen((context) =>
//{
//    return context.Request.Path.StartsWithSegments("/employees") &&
//            context.Request.Query.ContainsKey("id");
//},


////app.MapWhen((context) =>
////{
////    return context.Request.Path.StartsWithSegments("/employees") && context.Request.Query.ContainsKey("id");
////},
//(appBuilder) =>
//{
//    //Branch Middleware #5
//    appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
//    {
//        await context.Response.WriteAsync("Middleware #5: Before calling next\n\r");
//        await next(context);
//        await context.Response.WriteAsync("Middleware #5: After calling next\n\r");
//    });

//    appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
//    {
//        await context.Response.WriteAsync("Middleware #6: Before calling next\n\r");
//        await next(context);
//        await context.Response.WriteAsync("Middleware #6: After calling next\n\r");
//    });
//});

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
 * app.MapWhen to create conditional branching middleware.
 * app.UseWhen to create a rejoinable branch of middleware based on a condition.
 * Program.cs file is for configuring our Kestrel server, our web application and the middleware pipeline.
 * Creating custom middleware class first create the class that implements the interface and second is register the class as a service.
 */