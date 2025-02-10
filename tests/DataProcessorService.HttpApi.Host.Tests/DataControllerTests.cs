using DataProcessorService.Application.Contracts.Data;
using DataProcessorService.HttpApi.Host.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DataProcessorService.HttpApi.Host.Tests;

public class DataControllerTests
{
    [Fact]
    public async Task SaveItems_ReturnsOk_WhenDataSavedSuccessfully()
    {
        var mockDataService = new Mock<IDataService>();
        var controller = new DataController(mockDataService.Object);

        var requestDto = new DataItemsRequestDto
        {
            Items = new Dictionary<int, string>
                {
                    { 1, "value1" },
                    { 2, "value2" }
                }
        };

        mockDataService.Setup(service => service.SaveItemsAsync(It.IsAny<DataItemsRequestDto>()))
            .Returns(Task.CompletedTask);

        var result = await controller.SaveItems(requestDto);

        var okResult = Assert.IsType<OkResult>(result);
        mockDataService.Verify(service => service.SaveItemsAsync(It.IsAny<DataItemsRequestDto>()), Times.Once);
    }

    [Fact]
    public async Task GetItems_ReturnsOk_WhenItemsExist()
    {
        var mockDataService = new Mock<IDataService>();
        var controller = new DataController(mockDataService.Object);

        var mockItems = new List<DataItemResponseDto>
            {
                new DataItemResponseDto { Id = 1 , Code = 1, Value = "value1" },
                new DataItemResponseDto { Id = 2, Code = 2, Value = "value2" }
            };

        var responseDto = new DataItemsResponseDto
        {
            Items = mockItems
        };

        mockDataService.Setup(service => service.GetItemsAsync(It.IsAny<GetDataItemsInput>()))
            .ReturnsAsync(responseDto);

        var result = await controller.GetItems(null);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsAssignableFrom<DataItemsResponseDto>(okResult.Value);
        Assert.Equal(mockItems.Count, returnValue.Items.Count);

        for (int i = 0; i < mockItems.Count; i++)
        {
            Assert.Equal(mockItems[i].Code, returnValue.Items[i].Code);
            Assert.Equal(mockItems[i].Value, returnValue.Items[i].Value);
        }
    }

    [Fact]
    public async Task GetItems_ReturnsNotFound_WhenNoItemsExist()
    {
        var mockDataService = new Mock<IDataService>();
        var controller = new DataController(mockDataService.Object);

        mockDataService.Setup(service => service.GetItemsAsync(It.IsAny<GetDataItemsInput>()))
            .ReturnsAsync((DataItemsResponseDto)null);

        var result = await controller.GetItems(null);

        Assert.IsType<NotFoundResult>(result);
    }
}