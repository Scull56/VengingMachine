using System.Reflection;

namespace VendingMachine.Protect
{
    public class AdminKeyMiddleware
    {
        private readonly RequestDelegate _next;

        public AdminKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var key = context.Request.Query["key"];

            var controller = context.Request.RouteValues["controller"];
            var controllerMethod = context.Request.RouteValues["action"];

            if (controller != null && controllerMethod != null)
            {
                var controllerStr = controller.ToString();
                var methodStr = controllerMethod.ToString();

                Type[] typelist = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "VendingMachine.Controllers").ToArray();

                Type controllerClass = typelist.First((Type type) => type.Name == controllerStr + "Controller");

                if (controllerClass != null)
                {
                    var method = controllerClass.GetMethod(methodStr);

                    if (method != null)
                    {
                        var attribute = method.GetCustomAttribute(typeof(AdminKeyAttribute));

                        if (attribute != null)
                        {
                            if (key != AdminKeyAttribute.Key)
                            {
                                context.Response.StatusCode = 403;
                                await context.Response.WriteAsync(@"{""message"": ""Wrong key for access"" ");
                            }
                        }
                    }
                }
            }

            await _next(context);
        }
    }

    public static class AdminKeyMiddlewareExtensions
    {
        public static IApplicationBuilder UseAdminKey(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AdminKeyMiddleware>();
        }
    }
}
