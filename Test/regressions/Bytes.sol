pragma solidity >=0.4.24 <0.6.0;

contract Bytes {

    bytes public data = "0x3333";
    bytes public empty;

    function clearData() public {
        data = "";
    }
}
