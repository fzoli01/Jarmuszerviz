using System.Web.Http;
using WebActivatorEx;
using Jarmuszerviz;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Jarmuszerviz
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                      
                        c.SingleApiVersion("v1", "Jarmuszerviz");

                        
                    })
                .EnableSwaggerUi(c =>
                    {
                       
                    });
        }
    }
}
