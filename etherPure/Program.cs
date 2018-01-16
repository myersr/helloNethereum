using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.Encoders;
using Nethereum.Geth;
using System.Threading;
using NUnit;
using NUnit.Framework;
using System.Numerics;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace etherPure
{
    class Program
    {

        
        public class SimpleStorageEvent
        {            
            [Parameter("uint", "x", 1, false)]
            public int Result {get; set;}

            [Parameter("address", "sender", 2, true)]
            public string Sender {get; set;}
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Please enter text and hit enter: ");
            var userIn = Console.ReadLine();
            Console.WriteLine($"User entered: {userIn}");
            int number;

            if(userIn == "deploy")
            {
                Int32 seedV = 0;
                smartDBReq(seedV).GetAwaiter().GetResult();
            }else if(!Int32.TryParse(userIn, out number))
            {
                //no, not able to parse, repeat, throw exception, use fallback value?
                Console.WriteLine($"Not able to convert {userIn} to int");
                getSmartLog().GetAwaiter().GetResult();
            }else{
                Console.WriteLine(number.GetType());
                Console.WriteLine($"The new integer is now {number}");
                liteTransaction(number).GetAwaiter().GetResult();

                getBlockValue().GetAwaiter().GetResult();
            }
            
            
        }
        

        static async Task getBlockValue()
        {
            //This abi variable is very important. After creating the variable, go to the smart contract that was created earlier and copy the entire contents of the .abi file into a word doc and replace all of the " with "".
            var abi = @"[{""constant"":false,""inputs"":[{""name"":""x"",""type"":""uint256""}],""name"":""set"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""get"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""anonymous"":false,""inputs"":[{""indexed"":true,""name"":""x"",""type"":""uint256""},{""indexed"":true,""name"":""sender"",""type"":""address""}],""name"":""DSet"",""type"":""event""}]";
            //create a new variable that will be mulitplied if the transaction is recieved.
            var contAddr = "0x87d88efdf8594f9aa73b1c0ded267084679ae7af";
            Console.WriteLine("Establishing Server connect");
            var web3Geth = new Web3Geth("http://localhost:8545");
            web3Geth.TransactionManager.DefaultGas = BigInteger.Parse("100000");
            //Send the pack
            var contract = web3Geth.Eth.GetContract(abi, contAddr);
            var getFunction = contract.GetFunction("get");

            var ting = await getFunction.CallAsync<uint>();
            Console.WriteLine($"Da ting go ba da da {ting}");
            

        }

        static async Task getSmartLog()
        {
            //This abi variable is very important. After creating the variable, go to the smart contract that was created earlier and copy the entire contents of the .abi file into a word doc and replace all of the " with "".
            var abi = @"[{""constant"":false,""inputs"":[{""name"":""x"",""type"":""uint256""}],""name"":""set"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""get"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""anonymous"":false,""inputs"":[{""indexed"":true,""name"":""x"",""type"":""uint256""},{""indexed"":true,""name"":""sender"",""type"":""address""}],""name"":""DSet"",""type"":""event""}]";
            //create a new variable that will be mulitplied if the transaction is recieved.
            var contAddr = "0x87d88efdf8594f9aa73b1c0ded267084679ae7af";
            Console.WriteLine("Establishing Server connect");
            var web3Geth = new Web3Geth("http://localhost:8545");
            web3Geth.TransactionManager.DefaultGas = BigInteger.Parse("100000");
            //Send the pack
            var contract = web3Geth.Eth.GetContract(abi, contAddr);
            var logEvent = contract.GetEvent("DSet");

            var filterAll = await logEvent.CreateFilterAsync();
            var log = await logEvent.GetFilterChanges<SimpleStorageEvent>(filterAll);
            Console.WriteLine(log[0].Event.Result);
        }

        
        static async Task liteTransaction(Int32 userInputVal)
        {
            var entryValue = userInputVal;
            var senderAddress = "0x12890d2cce102216644c59daE5baed380d84830c";
            //This abi variable is very important. After creating the variable, go to the smart contract that was created earlier and copy the entire contents of the .abi file into a word doc and replace all of the " with "".
            var abi = @"[{""constant"":false,""inputs"":[{""name"":""x"",""type"":""uint256""}],""name"":""set"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""get"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""anonymous"":false,""inputs"":[{""indexed"":true,""name"":""x"",""type"":""uint256""},{""indexed"":true,""name"":""sender"",""type"":""address""}],""name"":""DSet"",""type"":""event""}]";

            var byteCode = "0x6060604052341561000f57600080fd5b6101028061001e6000396000f30060606040526004361060485763ffffffff7c010000000000000000000000000000000000000000000000000000000060003504166360fe47b18114604d5780636d4ce63c146062575b600080fd5b3415605757600080fd5b60606004356084565b005b3415606c57600080fd5b607260d0565b60405190815260200160405180910390f35b600081905573ffffffffffffffffffffffffffffffffffffffff3316817ff417906993a6692084144afd95a5b23ecdc04788a2ec02c29be6b4a093cce76f60405160405180910390a350565b600054905600a165627a7a723058208d97f044a6ac6407312befefafcfc71ce287099c368600c36b5d1768ff5c143b0029";

            //create a new variable that will be mulitplied if the transaction is recieved.
            var contAddr = "0x87d88efdf8594f9aa73b1c0ded267084679ae7af";
            Console.WriteLine("Establishing Server connect");
            uint convertedValue = (uint)userInputVal;
            var web3Geth = new Web3Geth("http://localhost:8545");
            web3Geth.TransactionManager.DefaultGas = BigInteger.Parse("100000");

            //web3Geth.TransactionManager.DefaultGas = BigInteger.Parse("100000");

            Console.WriteLine("Unlocking account and initiating send");
            var unlockAccountResult = await web3Geth.Personal.UnlockAccount.SendRequestAsync(senderAddress, "password", 120);
            await web3Geth.Miner.Start.SendRequestAsync(6);
            //var receipt = await web3Geth.Eth.DeployContract.SendRequestAndWaitForReceiptAsync(abi, byteCode, senderAddress, new HexBigInteger(900000), null);

            //Send the pack
            var contract = web3Geth.Eth.GetContract(abi, contAddr);
            //Console.WriteLine("This is the contract: ", contract.ToString());
            //var logEvent = contract.GetEvent("dSet");

            var setFunction = contract.GetFunction("set");

            //Numbers will equal 49 if true. 
            Console.WriteLine("Preparing to Write to Block");
            var transactionHash = await setFunction.SendTransactionAsync(senderAddress, convertedValue);
            Console.WriteLine("Transaction hash?: ", transactionHash);
            var receipt = await setFunction.SendTransactionAndWaitForReceiptAsync(senderAddress, null, 8);

            var miningResult = await web3Geth.Miner.Stop.SendRequestAsync();
            Console.WriteLine("This is the ending result: ", miningResult);
        }


        static async Task smartDBReq(Int32 userInputVal)
        {
            //Create a new Variable that will act as the Sender's address
            var senderAddress = "0x12890d2cce102216644c59daE5baed380d84830c";
            //Create a variable that will be the password, for the sake of being easy, I named it password
            var password = "password";
            //This abi variable is very important. After creating the variable, go to the smart contract that was created earlier and copy the entire contents of the .abi file into a word doc and replace all of the " with "".
            var abi = @"[{""constant"":false,""inputs"":[{""name"":""x"",""type"":""uint256""}],""name"":""set"",""outputs"":[],""payable"":false,""stateMutability"":""nonpayable"",""type"":""function""},{""constant"":true,""inputs"":[],""name"":""get"",""outputs"":[{""name"":"""",""type"":""uint256""}],""payable"":false,""stateMutability"":""view"",""type"":""function""},{""anonymous"":false,""inputs"":[{""indexed"":true,""name"":""x"",""type"":""uint256""},{""indexed"":true,""name"":""sender"",""type"":""address""}],""name"":""DSet"",""type"":""event""}]";
            //The byte code can be found in the smart contract file that was created earlier that ends with .bin. Copy the entire address here and use the prefix 0x
            var byteCode = "0x6060604052341561000f57600080fd5b6101028061001e6000396000f30060606040526004361060485763ffffffff7c010000000000000000000000000000000000000000000000000000000060003504166360fe47b18114604d5780636d4ce63c146062575b600080fd5b3415605757600080fd5b60606004356084565b005b3415606c57600080fd5b607260d0565b60405190815260200160405180910390f35b600081905573ffffffffffffffffffffffffffffffffffffffff3316817ff417906993a6692084144afd95a5b23ecdc04788a2ec02c29be6b4a093cce76f60405160405180910390a350565b600054905600a165627a7a723058208d97f044a6ac6407312befefafcfc71ce287099c368600c36b5d1768ff5c143b0029";
            //create a new variable that will be mulitplied if the transaction is recieved.
            var entryValue = userInputVal;
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
            var transactionHash = await web3Geth.Eth.DeployContract.SendRequestAsync(abi,byteCode, senderAddress);
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
            Console.WriteLine("Contract Adress: ", contractAddress);
            // var contract = web3Geth.Eth.GetContract(abi, contractAddress);
            // //after the entire transaction takes place multiply the numbers
            // var setFunction = contract.GetFunction("set");

            // //Numbers will equal 49 if true. 
            // var result = await setFunction.CallAsync<int>(entryValue);

            
            
        }

        // public async Task<TransactionReceipt> MineAndGetReceiptAsync(Web3Geth web3, string transactionHash){

        //     var miningResult = await web3.Miner.Start.SendRequestAsync(6);

        //     var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);

        //     while(receipt == null){
        //         Thread.Sleep(1000);
        //         receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionHash);
        //     }

        //     miningResult = await web3.Miner.Stop.SendRequestAsync();
        //     Assert.True(miningResult);
        //     return receipt;
        // }

    }
}
