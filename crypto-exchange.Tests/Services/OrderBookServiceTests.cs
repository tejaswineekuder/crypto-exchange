using crypto_exchange.DTOs;
using crypto_exchange.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypto_exchange.Tests.Services
{
    public class OrderBookServiceTests
    {
        private readonly IOrderBookService _orderBookService;

        public OrderBookServiceTests()
        {
            _orderBookService = new OrderBookService();
        }

        [Test]
        public void LoadOrderBooks_ValidFolderPath_ReturnsListOfExchangeOrderBooks()
        {
            // Arrange
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Exchanges");

            // Act
            var result = _orderBookService.LoadOrderBooks(folderPath);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<ExchangeOrderBookDto>>(result);
            Assert.IsTrue(result.Count > 0);
        }

        [Test]
        public void LoadOrderBooks_NullFolderPath_ReturnsListOfExchangeOrderBooks()
        {
            // Act
            var result = _orderBookService.LoadOrderBooks(null);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<ExchangeOrderBookDto>>(result);
            Assert.IsTrue(result.Count > 0);
        }
    }
}
