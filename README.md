# helloNethereum

Basic web app to learn .NET. Figured I would also learn Smart Contracts and Geth. This might be a mistake. Feel free to contribute/fork/knife/take.

### Current state

 Currently this project is in a state of disarray. Test works but I was in the middle of getting a data store to work. 

### dependencies

things you need to install.

```
Geth & [Nethereum](https://github.com/Nethereum/Nethereum) 
Launched Geth using the testnet included with Nethereum
```

### Installing

I could not find the exact tutorial but I got everything from [here](https://nethereum.readthedocs.io/en/latest/). I also don't really know how to install stuff in .NET as apparently the package manager can't search and everyone in the world only uses .NET on windows.

### Running

etherPure works right now. Test makes use of `UnitTest1.cs` and run uses main in `Program.cs`. 

```
#This should run. It deploys a contract and multiplies a number by 7 and asserts
cd etherPure 
dotnet test
```

Alternatively

```
#This was a work in progress. I was trying to store a number with a smart contract
cd etherPure
dotnet run
```

Using 
 * Nethereum
 * Geth(Go Ethereum)
 * NUnit
 * XUnit
 * ASP.NET


### Troubleshooting
* [NUnit](https://github.com/Microsoft/vstest/issues/1058) testing because the docs are useless.
* [Tutorial](https://github.com/E01D/Ethereum/wiki/Nethereum-Basic-Tutorial) for Nethereum test deploy.

