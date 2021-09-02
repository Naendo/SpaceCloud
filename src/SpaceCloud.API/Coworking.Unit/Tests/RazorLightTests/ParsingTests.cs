using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using RazorLight;
using Xunit;

namespace Coworking.Unit.Tests
{
    public class ParsingTests
    {
        private readonly RazorLightEngine _engine;

        public ParsingTests()
        {
            _engine = new RazorLightEngineBuilder()
                .UseEmbeddedResourcesProject(Assembly.GetEntryAssembly())
                .UseMemoryCachingProvider()
                .Build();
        }

        [Fact]
        public async Task MailFactory_ParseForeachModule_ReturnsCorrectHtml()
        {
            var template = "<html><head></head><body><table>@foreach(var item in Model.Purchase){" +
                           "<tr>" +
                           "<td> @item.ProductName </td>" +
                           "<td class=\"centered\">@item.SinglePrice</td>" +
                           "<td class=\"centered\">@item.Amount</td>" +
                           "<td class=\"right\">@item.ProductTotal</td>" +
                           "</tr>}</table></body></html>";

            var result = await _engine.CompileRenderStringAsync("asdf", template, new TestModel
            {
                Purchase = new List<NestedModel>
                    {new NestedModel(), new NestedModel(), new NestedModel(), new NestedModel()}
            });

            Assert.NotNull(result);
        }
    }


    public class TestModel
    {
        public List<NestedModel> Purchase { get; set; }
    }

    public class NestedModel
    {
        public string SinglePrice { get; set; } = "asdf";
        public string Amount { get; set; } = "asdf";
        public string ProductTotal { get; set; } = "asdf";
        public string ProductName { get; set; } = "asdf";
    }
}