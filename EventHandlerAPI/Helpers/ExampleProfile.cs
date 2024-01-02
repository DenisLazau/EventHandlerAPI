using AutoMapper;
using EventHandler.Data.Models;
using EventHandlerAPI.Views;

namespace EventHandlerAPI.Helpers
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
