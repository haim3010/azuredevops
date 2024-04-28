using FluentAssertions;
using SampleWebApp.Models;
using SampleWebApp.Services;

namespace SampleWebApp.Tests
{
    public class ItemServiceTests
    {
        [Fact]
        public async Task ShouldGetAllItems()
        {
            ItemService service = new ItemService();

            var result = await service.GetAll();

            result.Should().NotBeNull();
            result.Should().HaveCount(4);
        }

        [Fact]
        public async Task ShouldGetItemsOnValidId()
        {
            ItemService service = new ItemService();

            var result = await service.Get(1);

            result.Should().NotBeNull();
            result.Should().Match<Item>(x => x.Id == 1);
        }

        [Fact]
        public async Task ShouldReturnNullOnInvalidId()
        {
            ItemService service = new ItemService();

            var result = await service.Get(5);

            result.Should().BeNull();
        }
    }
}