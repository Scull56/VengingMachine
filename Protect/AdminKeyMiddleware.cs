using System.Reflection;

namespace VendingMachine.Protect
{
    public class AdminKeyMiddleware
    {
        private string _adminKey;

        private readonly RequestDelegate _next;

        public AdminKeyMiddleware(RequestDelegate next, string adminKey)
        {
            _adminKey = adminKey;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var key = context.Request.Query["key"];

            var controller = context.Request.RouteValues["controller"];
            var controllerMethod = context.Request.RouteValues["action"];

            if (controller == null || controllerMethod == null)
            {
                await _next(context);

                return;
            }

            var controllerStr = controller.ToString();
            var methodStr = controllerMethod.ToString();

            Type[] typelist = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.Namespace == "VendingMachine.Controllers").ToArray();

            Type controllerClass = typelist.First((Type type) => type.Name == controllerStr + "Controller");

            if (controllerClass == null)
            {
                await _next(context);

                return;
            }

            var method = controllerClass.GetMethod(methodStr);

            if (method == null)
            {
                await _next(context);

                return;
            }

            var attribute = method.GetCustomAttribute(typeof(AdminKeyAttribute));

            if (attribute == null)
            {
                await _next(context);

                return;
            }

            if (key != _adminKey)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(@"{""message"": ""Wrong key for access"" ");

                return;
            }

            await _next(context);
        }
    }

    public static class AdminKeyMiddlewareExtensions
    {
        public static IApplicationBuilder UseAdminKey(
            this IApplicationBuilder builder, string adminKey)
        {
            return builder.UseMiddleware<AdminKeyMiddleware>(adminKey);
        }
    }
}
