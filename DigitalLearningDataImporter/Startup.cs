using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DigitalLearningDataImporter.Startup))]
namespace DigitalLearningDataImporter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
