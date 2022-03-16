using FluentAssertions;
using NUnit.Framework;
using R2RTechnicalTests.Builders;
using R2RTechnicalTests.Config;
using R2RTechnicalTests.Extensions;
using R2RTechnicalTests.Models;
using R2RTechnicalTests.Ressource;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static R2RTechnicalTests.Models.Orders;

namespace R2RTechnicalTests.Tests
{
    public class OrdersUnitTest
    {
        private ConfigSettings _configSettings;
        private ValidOrdersBuilder _validOrders;

        [SetUp]
        public void BeforeTest()
        {
            _configSettings = new ConfigSettings();
            _validOrders = new ValidOrdersBuilder();
        }

        [Test]
        public async Task GivenOrderDetailsWhenIterateThroughObjectsThenReportFindIssues()
        {
            // arrange
            var filePath = _configSettings.GetDocumentsFullPath(Constant.TestOrdersFile);
            var errorsFile = _configSettings.GetDocumentsFullPath(Constant.ErrorsFile);
            var errorList = new List<string>();


            //act
            var orders = await filePath.DeserializeJson<Order>();

            orders.OrderDetails.Select(m => m.ItemNumber).Where(x => !x.All(char.IsNumber)).ToList()
               .ForEach(x => errorList.Add(ErrorMessages.InvalidItemNumber(x)));

            orders.OrderDetails.Select(m => m.Quantity).Where(x => !x.All(char.IsNumber)).ToList()
               .ForEach(x => errorList.Add(ErrorMessages.InvalidQuatity(x)));

            orders.OrderDetails.Select(m => m.CustomerNumber).Where(x => !x.All(char.IsNumber)).ToList()
               .ForEach(x => errorList.Add(ErrorMessages.InvalidCustomerNumber(x)));

            orders.OrderDetails.Select(x => x.Cost).Where(m => !m.IsDouble(m)).ToList()
                .ForEach(x => errorList.Add(ErrorMessages.InvalidCost(x)));

            orders.OrderDetails.Select(x => x.OrderDate).Where(m => !m.IsValidDate(new[] { "dd-mm-yyyy" })).ToList()
               .ForEach(x => errorList.Add(ErrorMessages.InvalidDate(x)));


            var error = new Errors
            {
                OrderNumber = orders.OrderNumber,
                ErrorMessages = errorList
            };

            errorsFile.CreateAJsonFile(error, ErrorsJsonContext.Default.Errors);



            //assert
            var errors = await filePath.DeserializeJson<Errors>();
            error.ErrorMessages.Should().NotBeNull();
            error.ErrorMessages.Should().BeEquivalentTo(errorList);
            error.OrderNumber.Should().Be("1");
        }


        [Test]
        public async Task GivenTheValidOrdersWithCorrectDataTypeWhenWriteToJsonFileThenReadFile()
        {
            //arrange
            var filePath = _configSettings.GetDocumentsFullPath(Constant.TestOrdersFile);
            var validOrderFile = _configSettings.GetDocumentsFullPath(Constant.ValidOrderFile);
            var validOrder = _validOrders.CreateValidOrdersInstance;

            // act
            validOrderFile.CreateAJsonFile(validOrder, ValidOrdersJsonContext.Default.ValidOrder);

            //assert
            var orders = await validOrderFile.DeserializeJson<ValidOrder>();

            orders.Should().NotBeNull();
            orders.OrderNumber.Should().Be(validOrder.OrderNumber);
            orders.OrderDetails.Should().BeEquivalentTo(validOrder.OrderDetails);
        }

        [TestCase(1, 24, 4171)]
        [TestCase(2, 8, 61)]
        [Test]
        public async Task GivenValidOrdersFileWhenPerformCalclationOfTotalPriceThenReturnResult(int costumerNum, int quality, double totalCoast)
        {
            //arrange
            var validOrderFile = _configSettings.GetDocumentsFullPath(Constant.ValidOrderFile);
            var validOrders = await validOrderFile.DeserializeJson<ValidOrder>();

            //act
            var customerNumItems = validOrders.OrderDetails.Where(m => m.CustomerNumber == costumerNum).Select(m => m.Quantity).Sum();
            var customerTotalCost = GetTotalCost(validOrders, costumerNum);


            //assert
            customerNumItems.Should().BePositive();
            customerTotalCost.Should().BePositive();

            customerNumItems.Should().Be(quality);
            customerTotalCost.Should().Be(totalCoast);


        }


        private double GetTotalCost(ValidOrder order, int customerNum)
        {
            var costs = (from item in order.OrderDetails
                         where item.CustomerNumber.Equals(customerNum)
                         let n = item.Quantity * item.Cost
                         select n).ToList();
            var totalCost = costs.Sum();

            return totalCost;
        }
    }
}
