using AutoMapper;
using SuperStore.Data.Models;
using SuperStoreAPI.Views;

namespace SuperStoreAPI.Helpers
{
    public class ExampleProfile: Profile
    {
        public ExampleProfile()
        {
            CreateMap<Example, ExampleView>();
            CreateMap<ExampleView, Example>();
        }
    }
}
