using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nethereum.Hex.HexTypes;
using Nethereum.Geth;
using System.Threading;
using NUnit;
using NUnit.Framework;
using System.Numerics;


namespace etherPure
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
              //Create a new Variable that will act as the Sender's address
            var senderAddress = "0x12890d2cce102216644c59daE5baed380d84830c";
            //Create a variable that will be the password, for the sake of being easy, I named it password
            var password = "password";
            //This abi variable is very important. After creating the variable, go to the smart contract that was created earlier and copy the entire contents of the .abi file into a word doc and replace all of the " with "".
            var abi = @"[{""constant"":false,""inputs"":[{""name"":""val"",""type"":""int256""}],""name"":""multiply"",""outputs"":[{""name"":""d"",""type"":""int256""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[{""name"":""multiplier"",""type"":""int256""}],""payable"":false,""stateMutability"":""nonpayable"",""type"":""constructor""}]";
            //The byte code can be found in the smart contract file that was created earlier that ends with .bin. Copy the entire address here and use the prefix 0x
            var byteCode = "0x6060604052341561000f57600080fd5b6040516020806100d0833981016040528080516000555050609b806100356000396000f300606060405260043610603e5763ffffffff7c01000000000000000000000000000000000000000000000000000000006000350416631df4f14481146043575b600080fd5b3415604d57600080fd5b60566004356068565b60405190815260200160405180910390f35b60005402905600a165627a7a723058201b96b117e951a2c6def35ae7c9393bc5949657671cc55f7e9420c2f9547299a70029";
            //create a new variable that will be mulitplied if the transaction is recieved.
            var multiplier = 7;
            //Sync up with your TestChain
            Console.WriteLine("Establishing Server connect");
            var web3Geth = new Web3Geth("http://localhost:8545");
            web3Geth.TransactionManager.DefaultGas = BigInteger.Parse("100000");
            //Send the pack
            Console.WriteLine("Unlocking account and initiating send");
            var unlockAccountResult = await web3Geth.Personal.UnlockAccount.SendRequestAsync(senderAddress, password, 120);
            Console.WriteLine("Checking account unlock");
            Assert.True(unlockAccountResult);
         
            Console.WriteLine("Hashing transaction");
            var transactionHash = await web3Geth.Eth.DeployContract.SendRequestAsync(abi,byteCode, senderAddress,multiplier);
            Console.WriteLine(senderAddress);
            //Console.WriteLine(transactionHash);
            
            
            //Mine the transaction to confirm the transaction was recieved
            Console.WriteLine("Checking request results");
            var mineResult = await web3Geth.Miner.Start.SendRequestAsync(6);
            //Assert.True(mineResult);


            
            Console.WriteLine("Generating receipt");
            var receipt = await web3Geth.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
            //If the transaction is not recieved, wait 5000 milliseconds (5 Seconds) before 
            while (receipt == null)
            {
                Thread.Sleep(5000);
                receipt = await web3Geth.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);

            }
            //Stop the mining process
            mineResult = await web3Geth.Miner.Stop.SendRequestAsync();
            Assert.True(mineResult);

            var contractAddress = receipt.ContractAddress;
        }
    }
}
