pragma solidity ^0.4.24;

contract AbiContract {
    function abiCall(uint256 x, uint256 y) private pure returns(bytes memory)
    { 
	bytes memory encoded = abi.encodePacked(x, y); 
	return encoded; 
    }

    function foo() public 
    {
        bytes32 x = keccak256(abiCall(uint(4), uint(5)));
        bytes32 y = keccak256(abiCall(uint(4), uint(5)));
        bytes32 z = keccak256(abiCall(uint256(4), uint256(3)));
        assert (x == y);
        assert (x != z);
    }

    function bar(uint a, uint b) public 
    {
       bytes32 x = keccak256(abi.encodePacked(a, b));
       bytes32 y = keccak256(abi.encodePacked(a, b));
       assert(x == y);
       //assert(keccak256(abi.encodePacked(a, b)) == keccak256(abi.encodePacked(a, b))); //not supported yet
    }

}
