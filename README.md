# Crypto Exchange Order Matching

A .NET 8.0 console application that processes cryptocurrency order books from multiple exchanges to determine the best orders for buying or selling BTC.

## Features

* Reads and processes order books from multiple exchanges (JSON files).
* Finds the best buy/sell orders across exchanges.
* Considers exchange limits (EUR/BTC) while matching orders.

## Getting Started

### Prerequisites

* [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Installation

```bash
git clone https://github.com/tejaswineekuder/crypto-exchange.git
cd crypto-exchange
dotnet build
```

### Usage

```bash
dotnet run
```

* Enter Order Type (Buy/Sell).
* Enter the amount of BTC.

### Example

```
Enter Order Type (Buy/Sell): Buy
Enter Amount of BTC: 2
```

### Output:

```
Best Orders for Buying 2 BTC:
- Buy 1.5 BTC from Exchange 1 at 3000 EUR each.
- Buy 0.5 BTC from Exchange 2 at 3050 EUR each.
Total Cost: 6050 EUR
```

## Contact

For any questions, reach out to [tejaswineekuder](https://github.com/tejaswineekuder).
