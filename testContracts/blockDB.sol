pragma solidity ^0.4.0;

contract SimpleStorage {
    uint storedData;
    event dSet(uint indexed a, address indexed sender);

    function set(uint x) public {
        storedData = x;
        dSet(x, msg.sender);
        
    }

    function get() public constant returns (uint) {
        return storedData;
    }
}
