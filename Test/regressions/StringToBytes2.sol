pragma solidity >=0.4.24 <0.6.0;

//VeriSol complains about "bytes" arg type being called with "string" type

contract Test {
contract b{
    function safe(bytes memory data) public returns (bytes memory) {
        return data;
    }
    
    function safeFrom() public  returns (bytes memory)  {
        return safe("01");
    }
}
