pragma solidity ^0.4.0;

contract SimpleStorage {
    uint storedData;
    event DSet(uint indexed x, address indexed sender);

    function set(uint x) public {
        storedData = x;
        DSet(x, msg.sender);
        
    }

    function get() public constant returns (uint) {
        return storedData;
    }
}
